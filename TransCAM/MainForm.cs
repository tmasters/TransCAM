using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransCAM
{
    public partial class MainForm : Form
    {
        ModelParameters ModelParms = new ModelParameters();
        String Filename = "";

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Change which is the currently "active" line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbMedianEmit_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                lineEditChartEmissions.SeriesList[i].editable = false;
            int curSeriesIndex = 0;
            if (rbMaxEmit.Checked)
                curSeriesIndex = 2;
            else if (rbMinEmit.Checked)
                curSeriesIndex = 1;
            lineEditChartEmissions.SeriesList[curSeriesIndex].editable = true;
            lineEditChartEmissions.Refresh();
        }

        /// <summary>
        /// On initial load of this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //----Set up our emissions chart
            lineEditChartEmissions.yAxis = new LineEditChart.Axis()
            {
                Min = 0,
                Max = 40,
                Title = "Emissions (GtC)",
                Ticks = new List<double>() { 0, 5, 10, 15, 20, 25, 30, 35, 40 }
            };
            lineEditChartEmissions.xAxis = new LineEditChart.Axis()
            {
                Min = 2013,
                Max = 2100,
                Title = "Year",
                Ticks = new List<double>() { 2015, 2030, 2045, 2060, 2075, 2090, 2100 }
            };
            //Min line
            lineEditChartEmissions.SeriesList.Add(new LineEditChart.Series()
            {
                editable = false,
                lineColor = Color.Green,
                pointColor = Color.Green
            });
            //Max line
            lineEditChartEmissions.SeriesList.Add(new LineEditChart.Series()
            {
                editable = false,
                lineColor = Color.Red,
                pointColor = Color.Red
            });

            //----Set up our aerosols chart
            lineEditChartAerosols.yAxis = new LineEditChart.Axis()
            {
                Min = 0,
                Max = 1,
                Title = "Density",
                Ticks = new List<double>() { 0, 0.5, 100 }
            };
            lineEditChartAerosols.xAxis = new LineEditChart.Axis()
            {
                Min = -2.5,
                Max = 0.5,
                Title = "Aerosol Forcing (W/m^2)",
                Ticks = new List<double>() { -2.5, -2.0, -1.5, -1.0, -0.5, 0.0, 0.5 }
            };
            

            //----Set up our damages chart
            this.lineEditChartDamages.xAxis = new LineEditChart.Axis()
            {
                Min = 0,
                Max = 7,
                Title = "Warming (K)",
                Ticks = new List<double>() { 0, 1, 2, 3, 4, 5, 6, 7 }
            };

            this.lineEditChartDamages.yAxis = new LineEditChart.Axis()
            {
                Min = 0,
                Max = 40,
                Title = "% of Consumption/Ouput",
                Ticks = new List<double>() { 0, 5, 10, 15, 20, 25, 30, 35, 40 }
            };
            //Min line
            lineEditChartDamages.SeriesList.Add(new LineEditChart.Series()
            {
                editable = false,
                lineColor = Color.Green,
                pointColor = Color.Green
            });
            //Max line
            lineEditChartDamages.SeriesList.Add(new LineEditChart.Series()
            {
                editable = false,
                lineColor = Color.Red,
                pointColor = Color.Red
            });

            //Set-up the datagridview entry for other parameters
            dgProperties.Rows.Add(new object[] { "Discount Rate", 1, 4, 10 });
            dgProperties.Rows.Add(new object[] { "CO2 Effective Lifetime", 80, 168, 300 });
            //dgProperties.Rows.Add(new object[] { "CO2 Atmo Residence Fraction", 1, 4, 10 });
        }

        /// <summary>
        /// Edit notes for TCR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotesTCR_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Transient Climate Response", ModelParms.TCR_Notes);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ModelParms.TCR_Notes = form.Notes;
        }

        /// <summary>
        /// Manual entry of TCR PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualEntryTCR_Click(object sender, EventArgs e)
        {
            ModelParms.TCR_PDF = lineEditChartTCR.SeriesList[0].points.ToDictionary(K => K.X, K => K.Y);
            ManualEntryForm form = new ManualEntryForm("TCR", "TCR", "Density", ModelParms.TCR_PDF);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ModelParms.TCR_PDF = form.Values;
                lineEditChartTCR.SeriesList[0].points = new List<LineEditChart.Pt>();
                foreach (var point in ModelParms.TCR_PDF)
                {
                    lineEditChartTCR.SeriesList[0].points.Add(new LineEditChart.Pt()
                    {
                        Selected = false,
                        X = point.Key,
                        Y = point.Value
                    });
                    lineEditChartTCR.Refresh();
                }
            }
        }

        /// <summary>
        /// Edit notes for CO2 emission trajectories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotesEmissions_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("CO2 Emissions", ModelParms.CO2Emissions_Notes);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ModelParms.CO2Emissions_Notes = form.Notes;
        }

        /// <summary>
        /// Edit notes for aerosol forcing PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotesAerosols_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Anthropogenic Aerosol Forcing", ModelParms.AnthroAeroForcing_Notes);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ModelParms.AnthroAeroForcing_Notes = form.Notes;
        }

        /// <summary>
        /// Manual enter projected CO2 emission trajectories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualEmissions_Click(object sender, EventArgs e)
        {
            ModelParms.CO2_Emissions = new Dictionary<double, double>[3] 
            {
                lineEditChartEmissions.SeriesList[0].points.ToDictionary(K => K.X, K => K.Y),
                lineEditChartEmissions.SeriesList[1].points.ToDictionary(K => K.X, K => K.Y),
                lineEditChartEmissions.SeriesList[2].points.ToDictionary(K => K.X, K => K.Y)
            };
            String type = "(likely)";
            int curSeriesIndex = 0;
            if (rbMaxEmit.Checked)
            {
                curSeriesIndex = 2;
                type = "(max)";
            }
            else if (rbMinEmit.Checked)
            {
                type = "(min)";
                curSeriesIndex = 1;
            }

            ManualEntryForm form = new ManualEntryForm("Manual Emissions " + type, "Year", "Emissions" + type, ModelParms.CO2_Emissions[curSeriesIndex]);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Set the value back in our doc
                var emissions = ModelParms.CO2_Emissions;
                emissions[curSeriesIndex] = form.Values;
                ModelParms.CO2_Emissions = emissions;

                //Now update this on the graph
                lineEditChartEmissions.SeriesList[curSeriesIndex].points = new List<LineEditChart.Pt>();
                foreach (var point in emissions[curSeriesIndex])
                {
                    lineEditChartEmissions.SeriesList[curSeriesIndex].points.Add(new LineEditChart.Pt()
                    {
                        Selected = false,
                        X = point.Key,
                        Y = point.Value
                    });
                    lineEditChartEmissions.Refresh();
                }
            }
        }

        /// <summary>
        /// Manual enter current aerosol forcing PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManualAerosols_Click(object sender, EventArgs e)
        {
            ModelParms.AnthroAeroForcing_PDF = lineEditChartAerosols.SeriesList[0].points.ToDictionary(K => K.X, K => K.Y);
            ManualEntryForm form = new ManualEntryForm("Anthropogenic Aerosol Forcing", "AerosolForcing(W/m^2)", "Density", ModelParms.AnthroAeroForcing_PDF);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ModelParms.AnthroAeroForcing_PDF = form.Values;
                lineEditChartAerosols.SeriesList[0].points = new List<LineEditChart.Pt>();
                foreach (var point in ModelParms.AnthroAeroForcing_PDF)
                {
                    lineEditChartAerosols.SeriesList[0].points.Add(new LineEditChart.Pt()
                    {
                        Selected = false,
                        X = point.Key,
                        Y = point.Value
                    });
                    lineEditChartAerosols.Refresh();
                }
            }
        }

        /// <summary>
        /// Store the parameters entered in the UI back into the ModelParms object
        /// </summary>
        protected void StoreParametersFromUI()
        {
            //CO2 emissions trajectory
            ModelParms.CO2_Emissions = new Dictionary<double, double>[3] 
            {
                lineEditChartEmissions.SeriesList[0].points.ToDictionary(K => K.X, K => K.Y),
                lineEditChartEmissions.SeriesList[1].points.ToDictionary(K => K.X, K => K.Y),
                lineEditChartEmissions.SeriesList[2].points.ToDictionary(K => K.X, K => K.Y)
            };
            //Anthro aerosol PDF
            ModelParms.AnthroAeroForcing_PDF = lineEditChartAerosols.SeriesList[0].points.ToDictionary(K => K.X, K => K.Y);
            
            //TCR PDF
            ModelParms.TCR_PDF = lineEditChartTCR.SeriesList[0].points.ToDictionary(K => K.X, K => K.Y);

            //CO2 lifetime
            double[] lifetime = new double[3];
            double.TryParse(dgProperties.Rows[1].Cells[1].Value.ToString(), out lifetime[1]); //Min
            double.TryParse(dgProperties.Rows[1].Cells[2].Value.ToString(), out lifetime[0]); //Mode
            double.TryParse(dgProperties.Rows[1].Cells[0].Value.ToString(), out lifetime[2]); //Max           
            ModelParms.CO2Lifetime = lifetime;
        }

        /// <summary>
        /// Update the UI based on parameters in the DOM
        /// </summary>
        protected void LoadParametersIntoUI()
        {
            //CO2 emissions trajectory
            //Set the value back in our doc
            var emissions = ModelParms.CO2_Emissions;
            for (int i = 0; i < 3; i++)
            {
                lineEditChartEmissions.SeriesList[i].points = new List<LineEditChart.Pt>();
                foreach (var point in emissions[i])
                {
                    lineEditChartEmissions.SeriesList[i].points.Add(new LineEditChart.Pt()
                    {
                        Selected = false,
                        X = point.Key,
                        Y = point.Value
                    });
                    lineEditChartEmissions.Refresh();
                }
            }
            //Anthro aerosol PDF
            lineEditChartAerosols.SeriesList[0].points = new List<LineEditChart.Pt>();
            foreach (var point in ModelParms.AnthroAeroForcing_PDF)
            {
                lineEditChartAerosols.SeriesList[0].points.Add(new LineEditChart.Pt()
                {
                    Selected = false,
                    X = point.Key,
                    Y = point.Value
                });
                lineEditChartAerosols.Refresh();
            }

            //TCR PDF
            lineEditChartTCR.SeriesList[0].points = new List<LineEditChart.Pt>();
            foreach (var point in ModelParms.TCR_PDF)
            {
                lineEditChartTCR.SeriesList[0].points.Add(new LineEditChart.Pt()
                {
                    Selected = false,
                    X = point.Key,
                    Y = point.Value
                });
                lineEditChartTCR.Refresh();
            }

            //CO2 lifetime
            var lifetime = ModelParms.CO2Lifetime;
            dgProperties.Rows[1].Cells[1].Value = lifetime[1]; //Min
            dgProperties.Rows[1].Cells[2].Value = lifetime[0]; //Mode
            dgProperties.Rows[1].Cells[0].Value = lifetime[2]; //Max            
        }

        /// <summary>
        /// Show the properties notes dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPropertiesNotes_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Other Properties", ModelParms.Properties_Notes);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ModelParms.Properties_Notes = form.Notes;
        }

        /// <summary>
        /// Save As model parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StoreParametersFromUI();
            SaveFileDialog svd = new SaveFileDialog();
            svd.DefaultExt = "tcm";
            svd.Filter = "TransCAM Files (*.tcm)|*.tcm";
            if (svd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.ModelParms.Save(svd.FileName);
                this.Filename = svd.FileName;
            }
        }

        /// <summary>
        /// Simple save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Filename.Length == 0)
                saveAsToolStripMenuItem_Click(null, new EventArgs());
            else
                this.ModelParms.Save(this.Filename);
        }

        /// <summary>
        /// Open files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "tcm";
            ofd.Filter = "TransCAM Files (*.tcm)|*.tcm";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.ModelParms.Load(ofd.FileName);
                this.Filename = ofd.FileName;
                LoadParametersIntoUI();
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StoreParametersFromUI();
            var model = new Model();
            model.Run(this.ModelParms);
        }
    }
}
