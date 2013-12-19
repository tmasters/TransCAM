using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransCAM
{
    /// <summary>
    /// Line chart control that can allows manual editing of the charts via clicking
    /// </summary>
    public partial class LineEditChart : UserControl
    {
        /// <summary>
        /// This represents a point within the chart (x & y values are chart-specific)
        /// </summary>
        public class Pt
        {
            public double X {get;set;}
            public double Y { get; set; }
            public bool Selected = false;
        }

        /// <summary>
        /// Represents a series in the chart
        /// </summary>
        public class Series
        {
            public List<Pt> points { get; set; }
            /// <summary>
            /// Editable means you can add and delete points via clicks
            /// </summary>
            public bool editable {get; set;}
            public Color pointColor {get; set;}
            public Color lineColor { get; set; }
            //public Color highlightColor { get; set; }
            public Series()
            {
                points = new List<Pt>();
                pointColor = lineColor = System.Drawing.Color.Blue;
                editable = false;
                //highlightColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Represents an axis object
        /// </summary>
        public class Axis
        {
            public double Min {get;set;}
            public double Max { get; set; }
            public String Title { get; set; }
            public List<double> Ticks { get; set; }
        }

        /// <summary>
        /// X-Axis chart definition
        /// </summary>
        public Axis xAxis = new Axis()
        {
            Min = 0,
            Max = 3,
            Title = "Transient Climate Response",
            Ticks = new List<double>() { 0, 0.5, 1, 1.5, 2, 2.5, 3}
        };

        /// <summary>
        /// Y-Axis chart definition
        /// </summary>
        public Axis yAxis = new Axis()
        {
            Min = 0,
            Max = 1,
            Title = "Relative Density",
            Ticks = new List<double>() {0.0, 0.5, 1.0}
        };

        //List of series for this graph
        public List<Series> SeriesList = new List<Series>() { new Series(){editable = true}};
        
        //Setting for the diameter of 
        public int PointWidth = 10;

        protected Pt dragPoint = null;
        protected int marginBottom = 45, marginTop = 20, marginLeft = 45, marginRight = 20;

        public LineEditChart()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When mouse moves, to enable dragging
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (dragPoint != null)
            {
                Pt pt = ScreenToChart(new Point(e.X, e.Y));
                dragPoint.X = pt.X;
                dragPoint.Y = pt.Y;
                this.Refresh();
            }
        }

        /// <summary>
        /// When user clicks down on the mouse
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            foreach (Series series in this.SeriesList)
            {
                if (series.editable) //Only one series should be editable
                {
                    //First see if we are clicking on any existing points
                    bool found = false;
                    foreach (Pt pt in series.points)
                    {
                        Point screenCenter = ChartToScreen(pt);
                        if (Math.Pow(screenCenter.X - e.X, 2) + Math.Pow(screenCenter.Y - e.Y, 2) < Math.Pow(PointWidth / 2, 2.0))
                        {
                            pt.Selected = true;
                            dragPoint = pt;
                            found = true;
                        }
                        else //Deselect all other points on that mouse-down
                        {
                            pt.Selected = false;
                        }
                    }
                    if (!found)
                    {
                        Pt chartPoint = ScreenToChart(new Point(e.X, e.Y));
                        chartPoint.Selected = true;
                        dragPoint = chartPoint;
                        series.points.Add(chartPoint);
                    }
                }
            }

            this.Refresh();
        }

        /// <summary>
        /// When mouse goes up, stop dragging
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.dragPoint = null;
        }

        /// <summary>
        /// Handle DEL button for removing points
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Delete))
            {
                foreach (Series series in this.SeriesList)
                {
                    foreach (Pt pt in series.points)
                    {
                        if (pt.Selected)
                        {
                            series.points.Remove(pt);
                            this.Refresh();
                            break;
                        }
                    }
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Convert from coordinates in the chart to screen (control) coordinates
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected Point ChartToScreen(Pt pt)
        {
            int axisBottom = this.Height - marginBottom;
            int axisTop = marginTop;
            int axisLeft = marginLeft;
            int axisRight = this.Width - marginRight;

            int X = (int) Math.Round((pt.X-xAxis.Min) / (xAxis.Max - xAxis.Min) * (axisRight - axisLeft) + axisLeft);
            int Y = (int)Math.Round(axisBottom - (pt.Y - yAxis.Min) / (yAxis.Max - yAxis.Min) * (axisBottom - axisTop));
            return new Point(X, Y);
        }

        /// <summary>
        /// Convert from screen (control) coordinates to coordinates in the chart 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected Pt ScreenToChart(Point point)
        {
            //X and Y axis lines
            int axisBottom = this.Height - marginBottom;
            int axisTop = marginTop;
            int axisLeft = marginLeft;
            int axisRight = this.Width - marginRight;

            double point_x = ((double)point.X - axisLeft) / (axisRight - axisLeft) * (xAxis.Max - xAxis.Min) +xAxis.Min;
            double point_y = (axisBottom - (double)point.Y) * (yAxis.Max - yAxis.Min) / (axisBottom - axisTop) + yAxis.Min;
            return new Pt() { X = point_x, Y = point_y };
        }

        /// <summary>
        /// Redrawing the graph
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //X and Y axis lines
            int axisBottom =   this.Height-marginBottom;
            int axisTop = marginTop;
            int axisLeft = marginLeft;
            int axisRight = this.Width - marginRight;
  
            var solidBrush = new SolidBrush(Color.Black);

            //DRAW X-AXIS---------------

            e.Graphics.DrawLine(new Pen(Color.Black), new Point(axisLeft, axisBottom), new Point(axisRight, axisBottom));
            //Min point
            /*float ptWidth = e.Graphics.MeasureString(xAxis.Min.ToString(), new Font(FontFamily.GenericSansSerif, 10)).Width;
            e.Graphics.DrawString(xAxis.Min.ToString(), new Font(FontFamily.GenericSansSerif, 10),
                solidBrush, (float)axisLeft - .5 * ptWidth, (float)axisBottom + 5); 

            //Max point
            e.Graphics.DrawString(xAxis.Max.ToString(), new Font(FontFamily.GenericSansSerif, 10),
                solidBrush, (float)axisRight, (float)axisBottom + 5); */

            if (xAxis.Ticks != null)
            {
                foreach (double tick in xAxis.Ticks)
                {
                    int x = (int) ((tick-xAxis.Min) / (xAxis.Max - xAxis.Min) * (axisRight - axisLeft) + axisLeft);
                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(x, axisBottom-3), new Point(x, axisBottom+3));
                    float width = e.Graphics.MeasureString(tick.ToString(), new Font(FontFamily.GenericSansSerif, 10)).Width;
                    e.Graphics.DrawString(tick.ToString(), new Font(FontFamily.GenericSansSerif, 10),
                        solidBrush, (float)(x-.5*width), (float)axisBottom + 5);
                }
            }

            //Axis title
            float titleWidth = e.Graphics.MeasureString(xAxis.Title, new Font(FontFamily.GenericSansSerif, 12)).Width;
            e.Graphics.DrawString(xAxis.Title, new Font(FontFamily.GenericSansSerif, 12),
                       solidBrush, (float)(.5*(axisRight-axisLeft)+axisLeft - .5 * titleWidth), (float)axisBottom + 22);

            //DRAW Y-AXIS---------------
            e.Graphics.DrawLine(new Pen(Color.Black), new Point(axisLeft, axisBottom), new Point(axisLeft, marginTop));
            
            if (yAxis.Ticks != null)
            {
                foreach (double tick in yAxis.Ticks)
                {
                    int y = (int)(axisBottom - (tick -yAxis.Min) / (yAxis.Max - yAxis.Min) * (axisBottom - axisTop));
                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(axisLeft-3, y), new Point(axisLeft+3, y));
                    var size = e.Graphics.MeasureString(tick.ToString(), new Font(FontFamily.GenericSansSerif, 10));
                    e.Graphics.DrawString(tick.ToString(), new Font(FontFamily.GenericSansSerif, 10),
                        solidBrush, (float)axisLeft-5-size.Width, (float) (y - .5*size.Height));
                }

            } 

            //Axis title rotated
            e.Graphics.RotateTransform(90);
            var titleSize = e.Graphics.MeasureString(yAxis.Title, new Font(FontFamily.GenericSansSerif, 12));
            e.Graphics.DrawString(yAxis.Title, new Font(FontFamily.GenericSansSerif, 12),
                solidBrush, (float)axisLeft - 22 - titleSize.Height, (float)(.5*(axisBottom-axisTop)+axisTop - .5 * titleSize.Width));
            e.Graphics.ResetTransform();

            //Draw all points
            foreach (Series series in this.SeriesList)
            {
                series.points.Sort(PointComprarer);
                for (int i = 0; i < series.points.Count; i++)
                {
                    if (i < series.points.Count - 1) //Draw line between this and next point
                    {
                        e.Graphics.DrawLine(new Pen(series.lineColor), ChartToScreen(series.points[i]),
                            ChartToScreen(series.points[i + 1]));
                    }

                    if (series.editable) //Only draw big points if this is editable
                    {
                        var screenPos = ChartToScreen(series.points[i]);
                        var brush = new SolidBrush(series.pointColor);
                        e.Graphics.FillEllipse(brush, (int)(screenPos.X - PointWidth / 2), (int)(screenPos.Y - PointWidth / 2), PointWidth, PointWidth);
                        if (series.points[i].Selected)
                        {
                            e.Graphics.DrawEllipse(new Pen(Color.Black), (int)(screenPos.X - PointWidth / 2), (int)(screenPos.Y - PointWidth / 2), PointWidth, PointWidth);
                            String ptString = "Point: [" + series.points[i].X.ToString("N2") + "," + series.points[i].Y.ToString("N2") + "]";
                            e.Graphics.DrawString(ptString, new Font(FontFamily.GenericSansSerif, 8),
                                brush, 0, 0);
                        }
                    }
                }
            }
        }

        public static int PointComprarer(Pt pt1, Pt pt2)
        {
            return pt1.X.CompareTo(pt2.X);
        }
    }
}
