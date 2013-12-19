using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TransCAM
{
    public class ModelParameters
    {
        protected XmlDocument doc;
        protected String version = "0.1";

        public ModelParameters()
        {
            doc = new XmlDocument();
            var parentNode = doc.CreateElement("TransCAM");
            doc.AppendChild(parentNode);
            var versionNode = doc.CreateElement("Version");
            versionNode.InnerText = version;
            parentNode.AppendChild(versionNode);

            var iterationNode = doc.CreateElement("Iterations");
            iterationNode.InnerText = "1000";
            parentNode.AppendChild(iterationNode);

            //TCR Related parms
            var tcrNode = doc.CreateElement("TCR_Pts");
            parentNode.AppendChild(tcrNode);
            var tcrNotesNode = doc.CreateElement("TCR_Notes");
            parentNode.AppendChild(tcrNotesNode);

            //Emissions related parms
            var emissionsNode = doc.CreateElement("CO2Emissions");
            parentNode.AppendChild(emissionsNode);
            var emitMaxNode = doc.CreateElement("Max_Pts");
            var emitMedNode = doc.CreateElement("Med_Pts");
            var emitMinNode = doc.CreateElement("Min_Pts");
            emissionsNode.AppendChild(emitMaxNode);
            emissionsNode.AppendChild(emitMedNode);
            emissionsNode.AppendChild(emitMinNode);
            var emissionsNotesNode = doc.CreateElement("Notes");
            emissionsNode.AppendChild(emissionsNotesNode);

            //Aerosol related parms
            var aerosolNode = doc.CreateElement("AnthroAeroForcing_Pts");
            parentNode.AppendChild(aerosolNode);
            var aerosolNotesNode = doc.CreateElement("AnthroAeroForcing_Notes");
            parentNode.AppendChild(aerosolNotesNode);

            //Other properties
            var propertiesNotesNode = doc.CreateElement("Properties_Notes");
            parentNode.AppendChild(propertiesNotesNode);
            var CO2LifetimeNode = doc.CreateElement("CO2Lifetime");
            parentNode.AppendChild(CO2LifetimeNode);
            CO2LifetimeNode.AppendChild(doc.CreateElement("Min")).InnerText = "80";
            CO2LifetimeNode.AppendChild(doc.CreateElement("Mode")).InnerText = "168";
            CO2LifetimeNode.AppendChild(doc.CreateElement("Max")).InnerText = "300";
        }

        /// <summary>
        /// Get/Set notes for TCR
        /// </summary>
        public String TCR_Notes
        {
            get
            {
                var node = doc.SelectSingleNode("TransCAM/TCR_Notes");
                return node.InnerText;
            }
            set
            {
                var node = doc.SelectSingleNode("TransCAM/TCR_Notes");
                node.InnerText = value;
            }
        }

        /// <summary>
        /// Number of iterations of model to perform
        /// </summary>
        public long Iterations
        {
            get
            {
                var node = this.doc.SelectSingleNode("TransCAM/Iterations");
                long iterations;
                if (node != null && long.TryParse(node.InnerText, out iterations))
                    return iterations;
                else
                    return 1000;
            }
            set
            {
                var node = this.doc.SelectSingleNode("TransCAM/Iterations");
                if (node != null)
                    node.InnerText = value.ToString();
            }
        }

        /// <summary>
        /// Get/Set the specified TCR PDF points
        /// </summary>
        public Dictionary<double, double> TCR_PDF
        {
            get
            {
                Dictionary<double, double> userPoints = new Dictionary<double, double>();
                foreach (XmlNode node in this.doc.SelectNodes("TransCAM/TCR_Pts/TCR_Pt"))
                {
                    double tcr, density;
                    if (double.TryParse(node.Attributes["tcr"].Value, out tcr) &&
                        double.TryParse(node.InnerText, out density) &&
                        !userPoints.ContainsKey(tcr))
                    {
                        userPoints.Add(tcr, density);
                    }
                }
                return userPoints;
            }
            set
            {
                var tcrNode = this.doc.SelectSingleNode("TransCAM/TCR_Pts");
                tcrNode.RemoveAll();
                foreach (KeyValuePair<double, double> pt in value)
                {
                    var node = doc.CreateElement("TCR_Pt");
                    var attr = doc.CreateAttribute("tcr");
                    attr.Value = pt.Key.ToString();
                    node.InnerText = pt.Value.ToString();
                    node.Attributes.Append(attr);
                    tcrNode.AppendChild(node);
                }
            }
        }

        /// <summary>
        /// Get/Set the specified median(index=0), min(index=1), max(index=2) CO2 emission points
        /// </summary>
        public Dictionary<double, double>[] CO2_Emissions
        {
            get
            {
                Dictionary<double, double>[] userPoints = new Dictionary<double, double>[3];
                for (int i = 0; i < 3; i++)
                {
                    userPoints[i] = new Dictionary<double, double>();
                    String nodeNames = "TransCAM/CO2Emissions/";
                    if (i == 1)
                        nodeNames += "Min_Pts/Pt";
                    else if (i == 0)
                        nodeNames += "Med_Pts/Pt";
                    else
                        nodeNames += "Max_Pts/Pt";

                    foreach (XmlNode node in this.doc.SelectNodes(nodeNames))
                    {
                        double yr, emissions;
                        if (double.TryParse(node.Attributes["Year"].Value, out yr) &&
                            double.TryParse(node.InnerText, out emissions) &&
                            !userPoints[i].ContainsKey(yr))
                        {
                            userPoints[i].Add(yr, emissions);
                        }
                    }
                }

                return userPoints;
            }
            set
            {
                for (int i = 0; i < 3; i++)
                {
                    String nodeName = "TransCAM/CO2Emissions/";
                    if (i == 1)
                        nodeName += "Min_Pts";
                    else if (i == 0)
                        nodeName += "Med_Pts";
                    else
                        nodeName += "Max_Pts";
                    
                    var emitNode = this.doc.SelectSingleNode(nodeName);
                    emitNode.RemoveAll();
                    foreach (KeyValuePair<double, double> pt in value[i])
                    {
                        var node = doc.CreateElement("Pt");
                        var attr = doc.CreateAttribute("Year");
                        attr.Value = pt.Key.ToString();
                        node.InnerText = pt.Value.ToString();
                        node.Attributes.Append(attr);
                        emitNode.AppendChild(node);
                    }   
                }
            }
        }

        /// <summary>
        /// Get/Set notes for CO2Emissions
        /// </summary>
        public String CO2Emissions_Notes
        {
            get
            {
                var node = doc.SelectSingleNode("TransCAM/CO2Emissions/Notes");
                return node.InnerText;
            }
            set
            {
                var node = doc.SelectSingleNode("TransCAM/CO2Emissions/Notes");
                node.InnerText = value;
            }
        }

        /// <summary>
        /// Get/Set notes for Anthropogenic Aerosol Forcing
        /// </summary>
        public String AnthroAeroForcing_Notes
        {
            get
            {
                var node = doc.SelectSingleNode("TransCAM/AnthroAeroForcing_Notes");
                return node.InnerText;
            }
            set
            {
                var node = doc.SelectSingleNode("TransCAM/AnthroAeroForcing_Notes");
                node.InnerText = value;
            }
        }

        /// <summary>
        /// Get/Set the specified AnthroAeroFrocing_PDF points
        /// </summary>
        public Dictionary<double, double> AnthroAeroForcing_PDF
        {
            get
            {
                Dictionary<double, double> userPoints = new Dictionary<double, double>();
                foreach (XmlNode node in this.doc.SelectNodes("TransCAM/AnthroAeroForcing_Pts/AAF_Pt"))
                {
                    double aaf, density;
                    if (double.TryParse(node.Attributes["AAF"].Value, out aaf) &&
                        double.TryParse(node.InnerText, out density) &&
                        !userPoints.ContainsKey(aaf))
                    {
                        userPoints.Add(aaf, density);
                    }
                }
                return userPoints;
            }
            set
            {
                var tcrNode = this.doc.SelectSingleNode("TransCAM/AnthroAeroForcing_Pts");
                tcrNode.RemoveAll();
                foreach (KeyValuePair<double, double> pt in value)
                {
                    var node = doc.CreateElement("AAF_Pt");
                    var attr = doc.CreateAttribute("AAF");
                    attr.Value = pt.Key.ToString();
                    node.InnerText = pt.Value.ToString();
                    node.Attributes.Append(attr);
                    tcrNode.AppendChild(node);
                }
            }
        }

        /// <summary>
        /// Get/Set notes for Properties
        /// </summary>
        public String Properties_Notes
        {
            get
            {
                var node = doc.SelectSingleNode("TransCAM/Properties_Notes");
                return node.InnerText;
            }
            set
            {
                var node = doc.SelectSingleNode("TransCAM/Properties_Notes");
                node.InnerText = value;
            }
        }

        /// <summary>
        /// Get mode/min/max of CO2lifetime (index0=mode, 1=min, 2=max)
        /// </summary>
        public double[] CO2Lifetime
        {
            get
            {
                double[] lifetime = new double[3];
                var node = doc.SelectSingleNode("TransCAM/CO2Lifetime/Mode");
                double.TryParse(node.InnerText, out lifetime[0]);
                node = doc.SelectSingleNode("TransCAM/CO2Lifetime/Min");
                double.TryParse(node.InnerText, out lifetime[1]);
                node = doc.SelectSingleNode("TransCAM/CO2Lifetime/Max");
                double.TryParse(node.InnerText, out lifetime[2]);
                return lifetime;
            }
            set
            {
                var node = doc.SelectSingleNode("TransCAM/CO2Lifetime/Mode");
                node.InnerText = value[0].ToString();
                node = doc.SelectSingleNode("TransCAM/CO2Lifetime/Min");
                node.InnerText = value[1].ToString();
                node = doc.SelectSingleNode("TransCAM/CO2Lifetime/Max");
                node.InnerText = value[2].ToString();
            }
        }

        /// <summary>
        /// Save parameters to specified file
        /// </summary>
        /// <param name="file"></param>
        public void Save(String file)
        {
            this.doc.Save(file);
        }

        /// <summary>
        /// Load parameter for the specified file
        /// </summary>
        /// <param name="file"></param>
        public void Load(String file)
        {
            this.doc.Load(file);
        }
        
    }
}
