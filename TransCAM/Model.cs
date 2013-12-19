using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransCAM
{
    public class Model
    {
        //Hard-coded parameters
        const int START_YEAR = 2013;
        const int END_YEAR = 2100;
        const double CH4_Lifetime = 12.0;
        const double CH4_Prein_PPB = 790;
        const double CH4_Cur_PPB = 1800;
        const double CO2_Prein_PPM = 288;
        const double CO2_Cur_PPM = 392.5;
        //Default parameters for two-box model
        const double layer1Depth = 90;
        const double layer2Depth = 1000;
        

        protected List<double[]> m_temperatureRuns = new List<double[]>();

        /// <summary>
        /// Run through the iterations of the model
        /// </summary>
        /// <param name="parms"></param>
        public void Run(ModelParameters parms)
        {
            Random rand = new Random(0);


            double[] CO2EmissionsHist = ProcessTimeSeries(Properties.Resources.CO2_Emissions_Hist);

            //First smooth our parameter points into logical discrete units
            double[] emissionsMinInterpolate = Interpolate.InterpolateForYears(CO2EmissionsHist[CO2EmissionsHist.Length-1], START_YEAR, END_YEAR - START_YEAR + 1, parms.CO2_Emissions[1]);
            double[] emissionsMaxInterpolate = Interpolate.InterpolateForYears(CO2EmissionsHist[CO2EmissionsHist.Length - 1], START_YEAR, END_YEAR - START_YEAR + 1, parms.CO2_Emissions[2]);
            double[] emissionsLikelyInterpolate = Interpolate.InterpolateForYears(CO2EmissionsHist[CO2EmissionsHist.Length - 1], START_YEAR, END_YEAR - START_YEAR + 1, parms.CO2_Emissions[0]);
            var aerosolsInterpolate = Interpolate.InterpolateForPDF(-25, 5, parms.AnthroAeroForcing_PDF);
            var tcrInterpolate = Interpolate.InterpolateForPDF(0, 30, parms.TCR_PDF);

            for (int run = 0; run < parms.Iterations; run++)
            {
                //Sample from different parameters
                //First determine future CO2 emissions trajectory
                double[] emissionsFuture = new double[END_YEAR - START_YEAR + 1];
                double emissionsRand = rand.NextDouble();
                for (int i = 0; i < emissionsFuture.Length; i++)
                    emissionsFuture[i] = TriangleSample(emissionsMinInterpolate[i], emissionsMaxInterpolate[i], emissionsLikelyInterpolate[i], emissionsRand);
                //Now determine aerosols
                double curAerosol = GetSampleFromPDF(aerosolsInterpolate, rand.NextDouble()) / 10.0;
                //Now TCR
                int tcr_sample = GetSampleFromPDF(tcrInterpolate, rand.NextDouble());
                double tcr = tcr_sample / 10.0;
                //Based on TCR, we will derive parameters for lambda, c1Depth, c2Depth, and layerTransfer for 2-box model
                double lambda = 3.7 / (tcr / 0.6); //Our TCR will be about 60% of our effective sensitivity
                double layerTransfer = findBestLayerTranser(tcr, lambda);

                //Now sample for our CO2lifetime
                double lifetime = TriangleSample(parms.CO2Lifetime[1], parms.CO2Lifetime[2], parms.CO2Lifetime[0], rand.NextDouble());

                //Now calculate the temperatures
                double[] temperatures = CalculateTemperatures(emissionsFuture, curAerosol, lifetime, lambda, layer1Depth, layer2Depth, layerTransfer);
  
            }
        }

        /// <summary>
        /// Get a sample from a discrete distribution
        /// </summary>
        /// <param name="pdf">int component represents value to 1 decimal place * 10, double component is relative density</param>
        /// <param name="U">pre-selected value from uniform dist (0,1)</param>
        /// <returns>int value selected</returns>
        protected int GetSampleFromPDF(Dictionary<int, double> pdf, double U)
        {
            //First we will normalize this 
            double totalValue = 0.0;
            foreach (var value in pdf.Values)
                totalValue += value;
            double rand = U * totalValue;

            //Now find relevant sample
            double cumTotal = 0.0;
            foreach (var sample in pdf)
            {
                cumTotal += sample.Value;
                if (rand <= cumTotal)
                    return sample.Key;
            }
            //Should never reach here
            return int.MinValue;
        }

        /// <summary>
        /// Select the sample from a triangle distribution based on the U value selected
        /// </summary>
        /// <param name="a">min</param>
        /// <param name="b">max</param>
        /// <param name="c">mode</param>
        /// <param name="U">pre-selected value from uniform dist (0,1)</param>
        /// <returns></returns>
        protected double TriangleSample(double a, double b, double c, double U)
        {
            if (U < c)
                return a + Math.Sqrt(U * (b - a) * (c - a));
            else
                return b - Math.Sqrt((1.0 - U) * (b - a) * (b - c));
        }

        /// <summary>
        /// Determine the air-born fraction based on the specified lifetime and historic emissions / concentrations
        /// </summary>
        /// <param name="emissions_history"></param>
        /// <param name="concentration_history"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        protected double CalculateAirBornFraction(double[] emissions_history, double lifetime, double startVal, double endVal)
        {
            //Determine curvature from lifetime
            var conc_x = concentration(emissions_history, lifetime, 1.0, 0.0);
            return (conc_x[conc_x.Length - 1] - conc_x[0]) / (endVal - startVal);
            //Now set up our X variable for regression
            /*var X = new double[emissions_history.Length, 2];
            for (int i=0; i < conc_x.Length; i++)
            {
                X[i,0] = 1.0;
                X[i,1] = conc_x[i];
            }
            double[] betas = Regression.DoRegression(X, concentration_history);
            return betas[1]; */
        }

        /// <summary>
        /// Get time series array from resource file
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected static double[] ProcessTimeSeries(String input)
        {
            List<double> vals = new List<double>();
            String[] lines = input.Replace("\r\n", "\n").Split('\n');
            for (int i=1; i < lines.Length; i++)
            {
                String[] cells = lines[i].Split('\t');
                double val;
                if (cells.Length > 1 && double.TryParse(cells[1], out val))
                    vals.Add(val);
            }
            return vals.ToArray();
        }

        /// <summary>
        /// Calculate temperatures for each year based on the specified parameters
        /// </summary>
        /// <param name="CO2EmissionsFuture"></param>
        /// <param name="curAerosol"></param>
        /// <param name="CO2Lifetime"></param>
        /// <param name="lamda"></param>
        /// <param name="c1Depth"></param>
        /// <param name="c2Depth"></param>
        /// <param name="layerTransfer"></param>
        /// <returns></returns>
        protected double[] CalculateTemperatures(double[] CO2EmissionsFuture, double curAerosol, double CO2Lifetime, double lamda, double c1Depth, double c2Depth, double layerTransfer)
        {
            //Calculate CO2 (+ nitrous oxide + CFCs) forcing evolution
            double[] CO2EmissionsHist = ProcessTimeSeries(Properties.Resources.CO2_Emissions_Hist);
            double atmoFraction = CalculateAirBornFraction(CO2EmissionsHist, CO2Lifetime, CO2_Prein_PPM, CO2_Cur_PPM); 
            double[] CO2EmissionsAll = new double[2100-1850+1];
            for (int yr = 1850; yr <= 2100; yr++)
            {
                if (yr < START_YEAR)
                    CO2EmissionsAll[yr - 1850] = CO2EmissionsHist[yr - 1850];
                else
                    CO2EmissionsAll[yr - 1850] = CO2EmissionsFuture[yr - START_YEAR];
            }
            double[] CO2Concentration = concentration(CO2EmissionsAll, CO2Lifetime, atmoFraction, CO2_Prein_PPM);
            //Now calculate forcing
            double[] CO2Forcing = new double[2100 - 1850 + 1];
            for (int i = 0; i < CO2Concentration.Length; i++)
                CO2Forcing[i] = 5.35 * Math.Log(CO2Concentration[i] / 280.0);
            
            //Calculate CH4 forcing evolution
            double[] CH4EmissionsHist = ProcessTimeSeries(Properties.Resources.CH4_Emissions_Hist);
            double CH4AtmoFraction = CalculateAirBornFraction(CH4EmissionsHist, CH4_Lifetime, CH4_Prein_PPB, CH4_Cur_PPB); 
            //Base estimate of future CH4 emissions off of CO2 emissions (conversion from GtC to TgCH4)
            double[] CH4EmissionFuture = new double[END_YEAR - START_YEAR + 1];
            for (int i = 0; i < CO2EmissionsFuture.Length; i++)
                CH4EmissionFuture[i] = 36 * CO2EmissionsFuture[i] + 130;
           
            double[] CH4EmissionsAll = new double[2100 - 1850 + 1];
            for (int yr = 1850; yr <= 2100; yr++)
            {
                if (yr < START_YEAR)
                    CH4EmissionsAll[yr - 1850] = CH4EmissionsHist[yr - 1850];
                else
                    CH4EmissionsAll[yr - 1850] = CH4EmissionFuture[yr - START_YEAR];
            }
            double[] CH4Concentration = concentration(CH4EmissionsAll, CH4_Lifetime, CH4AtmoFraction, CH4_Prein_PPB);
            double[] CH4Forcing = new double[END_YEAR - 1850 + 1];
            for (int i = 0; i < CH4Concentration.Length; i++)
                CH4Forcing[i] = .03 * Math.Sqrt(CH4Concentration[i]) - 0.84;

            //O3 and aerosol forcing drop-off
            double[] AerosolForcing = GetAerosolForcing(curAerosol);
            double[] O3Forcing = GetO3Forcing();
            

            double[] TotalForcing = new double[END_YEAR - 1850 + 1];
            for (int yr = 0; yr < TotalForcing.Length; yr++)
            {
                //Include effects of NO2 and CFCs with CO2
                TotalForcing[yr] = CO2Forcing[yr] * 1.2 + CH4Forcing[yr] + AerosolForcing[yr] + O3Forcing[yr];
            }

            return RunTwoBox(TotalForcing, lamda, c1Depth, c2Depth, layerTransfer);
        }

        /// <summary>
        /// Get the aerosol forcing scaled based on the specified forcing
        /// </summary>
        /// <param name="curAeroForcing"></param>
        /// <returns></returns>
        public double[] GetAerosolForcing(double curAeroForcing)
        {
            double[] aerosolForcing = new double[END_YEAR - 1850 + 1];
            //First get historical forcing
            String[] rows = Properties.Resources.GISS_Forcings.Replace("\r\n", "\n").Split('\n');
            for (int yr=1850; yr < 1880; yr++)
                aerosolForcing[yr-1850] = 0.0;
            for (int yr=1880; yr < 2012; yr++)
            {
                aerosolForcing[yr - 1850] = 0;
                String[] cells = rows[yr-1880+1].Split('\t');
                for (int j = 4; j <= 7; j++)
                {
                    aerosolForcing[yr - 1850] += Convert.ToDouble(cells[j]);
                }
            }
            double lastYearForcing = aerosolForcing[2011-1850];
            //Taper off until 0 in 2100
            for (int yr = 2012; yr <= END_YEAR; yr++)
            {
                aerosolForcing[yr - 1850] = lastYearForcing - (yr - 2012.0) / (END_YEAR - 2012.0) * lastYearForcing;
            }

            //Now scale this aerosol forcing to the input
            for (int yr = 1850; yr < END_YEAR; yr++)
                aerosolForcing[yr - 1850] *= curAeroForcing / lastYearForcing;

            return aerosolForcing;
        }

        /// <summary>
        /// Get the O3 forcing
        /// </summary>
        /// <param name="curAeroForcing"></param>
        /// <returns></returns>
        public double[] GetO3Forcing()
        {
            double[] O3Forcing = new double[END_YEAR - 1850 + 1];
            //First get historical forcing
            String[] rows = Properties.Resources.GISS_Forcings.Replace("\r\n", "\n").Split('\n');
            for (int yr = 1850; yr < 1880; yr++)
                O3Forcing[yr - 1850] = 0.0;
            for (int yr = 1880; yr < 2012; yr++)
            {
                String[] cells = rows[yr - 1880 + 1].Split('\t');
                O3Forcing[yr - 1850] = Convert.ToDouble(cells[2]);
            }
            double lastYearForcing = O3Forcing[2011 - 1850];
            //Taper off until 0 in 2100
            for (int yr = 2012; yr <= END_YEAR; yr++)
            {
                O3Forcing[yr - 1850] = lastYearForcing - (yr - 2012.0) / (END_YEAR - 2012.0) * lastYearForcing;
            }

            return O3Forcing;
        }

        /// <summary>
        /// Run the two-box model with the specified parameters
        /// </summary>
        /// <param name="forcing"></param>
        /// <param name="lamda"></param>
        /// <param name="c1Depth"></param>
        /// <param name="c2Depth"></param>
        /// <param name="layerTransfer"></param>
        /// <returns>array of surface temperatures (same length as forcing)</returns>
        protected static double[] RunTwoBox(double[] forcing, double lamda, double c1Depth, double c2Depth, double layerTransfer)
        {
            //Get heat capacity of both layers
            double c1 = 4.0 * 1000 * 1000 * c1Depth;
            double c2 = 4.0 * 1000 * 1000 * c2Depth;

            double dt = 365.25 * 24 * 60 * 60; //Timestep
            int modelLength = forcing.Length;

            double[] T = new double[modelLength]; //Surface temperature anomaly
            double[] T2 = new double[modelLength]; //2nd layer temperature anomaly
            for (int yr = 0; yr < modelLength; yr++)
            {
                if (yr == 0)
                {
                    T[yr] = 0.0;
                    T2[yr] = 0.0;
                }
                else
                {
                    T[yr] = T[yr - 1] + dt * (forcing[yr] - lamda * T[yr - 1] - layerTransfer * (T[yr - 1] - T2[yr - 1])) / (c1);
                    T2[yr] = T2[yr - 1] + dt * (layerTransfer * (T[yr - 1] - T2[yr - 1])) / (c2);
                }
            }
            return T;
        }

        /// <summary>
        /// Get the concentration of a particular species based on the emission trajectory and
        /// parameters
        /// </summary>
        /// <param name="emissions"></param>
        /// <param name="lifetime"></param>
        /// <param name="atmoFactor"></param>
        /// <param name="intercept"></param>
        /// <returns></returns>
        protected static double[] concentration(double[] emissions, double lifetime, double atmoFactor, double intercept)
        {
            double[] conc = new double[emissions.Length];
            for (int i = 0; i < emissions.Length; i++) conc[i] = intercept;
            
            for (int yr = 0; yr < emissions.Length; yr++)
            {
                for (int resid = 0; resid <= yr; resid++)
                {
                    conc[yr] += emissions[resid] * atmoFactor * Math.Exp(-(yr - resid) / lifetime);
                }
            }

            return conc;
        }

        /// <summary>
        /// Calculate the transient response to 2x doubling of CO2 over 70 years based on specified parameters
        /// </summary>
        /// <typeparam name="?"></typeparam>
        /// <param name="?"></param>
        /// <returns>the TCR</returns>
        protected double calculateTCR(double lambda, double layerTransfer)
        {
	        double[] concentration = new double[70];
            double[] forcing = new double[70];
	        concentration[0] = 280;
            forcing[0] = 5.35 * Math.Log(concentration[0]/280.0);
	        for (int yr=1; yr < concentration.Length; yr++)
            {
		        concentration[yr] = 1.01*concentration[yr-1];
                forcing[yr] = 5.35 * Math.Log(concentration[yr]/280.0);
            }
            return RunTwoBox(forcing, lambda, layer1Depth, layer2Depth, layerTransfer)[69];
        }

        /// <summary>
        /// Find the value for layer heat transfer rate in the two-box model that get closest to the TCR
        /// specified (using lambda)
        /// </summary>
        /// <param name="TCR"></param>
        /// <param name="lambda"></param>
        /// <returns></returns>
        protected double findBestLayerTranser(double TCR, double lambda)
        {
            double minDiff = double.MaxValue;
            double bestLayerTransfer = 0;
            for (double lt = 0.01; lt <= 5.0; lt += 0.01)
            {
                double tcr_1 = calculateTCR(lambda, lt);
                if (Math.Abs(tcr_1 - TCR) < minDiff)
                {
                    minDiff = Math.Abs(tcr_1 - TCR);
                    bestLayerTransfer = lt;
                }
            }
            return bestLayerTransfer;
        }

    }
}
