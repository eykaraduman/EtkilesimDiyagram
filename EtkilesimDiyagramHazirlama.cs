using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using RcDesign;
using RcDesign.InteractionDiagram;
using RcDesign.Material;
using RcDesign.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace EtkilesimDiyagram
{
    public class EtkilesimDiyagramHazirlama
    {
        public DesignCodes Code { get; set; }
        public Concrete Concrete { get; set; }
        public Steel Steel { get; set; }
        public RectangularSection SectionGeometry { get; set; }
        public List<ReinforcingBar> ReinforcingBars { get; set; }
        public List<Load> Loads { get; set; }
        public StrengthReductionFactor StrengthReductionFactor { get; set; }
        public bool ShowNominalDiagram { get; set; }
        public bool ShowLeftDiagram { get; set; }
        public bool ShowRightDiagram { get; set; }
        public bool ShowAllDiagram { get; set; }
        public bool ShowGridLines { get; set; }
        public bool ShowLoads { get; set; }
        public bool ShowTitle { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public double ActualPmax { get; set; }
        public double Pmax { get; set; }
        public double Pmin { get; set; }

        public EtkilesimDiyagramHazirlama()
        {
            ShowNominalDiagram = false;
            ShowGridLines = false;
            ShowAllDiagram = true;
            ShowRightDiagram = false;
            ShowLeftDiagram = false;
            ShowLoads = true;
        }

        public List<InteractionDiagramItem> InteractionDiagramItems()
        {
            RcRectangularSection RcSection = new RcRectangularSection(SectionGeometry, Concrete, Steel, ReinforcingBars);
            List<InteractionDiagramItem> items = new List<InteractionDiagramItem>();

            double interval = 0.005;
            int NumberOfPoints = (int)Math.Truncate(1.20 * SectionGeometry.h / interval);
            double cMax = ReinforcingBars.Max(bar => bar.di);
            for (int i = 0; i < NumberOfPoints; i++)
            {
                double c = i * interval;
                List<double> ReinforcingStrains = new List<double>();

                for (int j = 0; j < ReinforcingBars.Count; j++)
                {
                    ReinforcingBar reBar = ReinforcingBars[j];
                    double SteelStrain = RcSection.ReinforcingStrain(c, reBar.di);
                    ReinforcingStrains.Add(SteelStrain);
                }

                double actualPhi = StrengthReductionFactor.CalculateActualPhi(Code, RcSection.MaxRebarStrain(ReinforcingStrains));

                if(RcSection.AxialCapacity(c) < RcSection.NominalPmax())
                    items.Add(new InteractionDiagramItem(RcSection.MomentCapacity(c), RcSection.AxialCapacity(c), actualPhi, Concrete.k1 * c));
            }

            items.Insert(0, new InteractionDiagramItem(RcSection.NominalMmin(), RcSection.NominalPmin(), StrengthReductionFactor.phiB, 0.00));

            items.Add(new InteractionDiagramItem(RcSection.NominalMmax(), RcSection.NominalPmax(), StrengthReductionFactor.phiC, Concrete.k1 * cMax));

            ActualPmax = StrengthReductionFactor.phiA * StrengthReductionFactor.phiC * RcSection.NominalPmax();

            return items;            
        }


        public List<InteractionDiagramItem> CreateInteractionDiagram()
        {
            List<InteractionDiagramItem> allItems = new List<InteractionDiagramItem>();
            List<InteractionDiagramItem> rightSideItems = InteractionDiagramItems();

            ReinforcingBars.ForEach(rb => rb.di = SectionGeometry.h - rb.di);
            ReinforcingBars.Reverse();

            List<InteractionDiagramItem> leftSideItems = InteractionDiagramItems();

            for (int i = 0; i < rightSideItems.Count; i++)
            {
                allItems.Add(rightSideItems[i]);
            }

            for (int i = leftSideItems.Count - 1; i >= 0; i--)
            {
                leftSideItems[i].Mnominal *= -1;
                leftSideItems[i].Mactual *= -1;
                allItems.Add(leftSideItems[i]);
            }
            return allItems;
        }

        public PlotModel CreateModel()
        {
            List<InteractionDiagramItem> items = CreateInteractionDiagram();
            Tuple<List<DataPoint>, List<DataPoint>> actualItems = DivideDiagramByPmax(items, ActualPmax);

            double iPmax = Pmax = items.Max(i => i.Pactual);
            double iPmin = Pmin = items.Min(i => i.Pactual);

            double iMmax = items.Max(i => i.Mactual);
            double iMmin = items.Min(i => i.Mactual);

            double PminPmaxLength = (iMmax - iMmin) / 3.5;

            LineStyle horizontalGridLines = LineStyle.Solid;
            LineStyle verticalGridLines = LineStyle.Solid;

            if(!ShowGridLines)
            {
                horizontalGridLines = LineStyle.None;
                verticalGridLines = LineStyle.None;
            }

            var model = new PlotModel();

            if (ShowTitle)
            {
                if (!string.IsNullOrEmpty(Title))
                {
                    model.Title = Title;
                }

                if (!string.IsNullOrEmpty(SubTitle))
                {
                    model.Subtitle = SubTitle;
                }
            }

            model.Axes.Add(new LinearAxis {
                IsZoomEnabled = false,
                IsPanEnabled = false,
                Title = "M (t.m)",
                Position = AxisPosition.Bottom,                
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1.00,
                ExtraGridlineStyle= LineStyle.LongDashDot,          
                MajorGridlineStyle = verticalGridLines,
                MinorGridlineStyle = verticalGridLines == LineStyle.Solid ? LineStyle.Dot : LineStyle.None,
            });

            model.Axes.Add(new LinearAxis {
                IsZoomEnabled = false,
                IsPanEnabled = false,
                Title = "N (t)",
                Position = AxisPosition.Left,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1.00,
                ExtraGridlineStyle = LineStyle.LongDashDot,
                MajorGridlineStyle = horizontalGridLines,
                MinorGridlineStyle = horizontalGridLines == LineStyle.Solid ? LineStyle.Dot : LineStyle.None,
                Maximum = ShowNominalDiagram ? items.Max(i => i.Pnominal) + 50.0 : items.Max(i => i.Pactual) + 50.0,
                Minimum = ShowNominalDiagram ? items.Min(i => i.Pnominal) - 50.0: items.Min(i => i.Pactual) - 50.0
            });

            if (ShowRightDiagram) model.Axes[0].Minimum = 0.0;

            if (ShowLeftDiagram) model.Axes[0].Maximum = 0.0;

            if (ShowAllDiagram)
            {
                model.Axes[0].Minimum = double.NaN;
                model.Axes[0].Maximum = double.NaN;
            }

            var loadsSerie = new LineSeries { MarkerType = MarkerType.Plus, LineStyle = LineStyle.None, MarkerStroke=OxyColors.Red };

            if (Loads.Count > 0)
            {
                foreach (var item in Loads)
                {
                    loadsSerie.Points.Add(new DataPoint(item.M, item.N));
                }
            }

            // Create series
            var nominalMPSerie = new LineSeries { MarkerType = MarkerType.None, StrokeThickness = 1, Color = OxyColors.Black, LineStyle = LineStyle.DashDot };
            var actualUpperMPSerie = new LineSeries { MarkerType = MarkerType.None, StrokeThickness = 1, Color = OxyColors.Black, LineStyle = LineStyle.Dash, Dashes = new double[] { 3.0 } };
            var actualLowerMPSerie = new LineSeries { MarkerType = MarkerType.None, StrokeThickness = 1, Color = OxyColors.Black };

            var PmaxSerie = new LineSeries { MarkerType = MarkerType.None, StrokeThickness = 1, Color = OxyColors.Black, LineStyle = LineStyle.Dash, Dashes = new double[] { 3.0 } };
            var PminSerie = new LineSeries { MarkerType = MarkerType.None, StrokeThickness = 1, Color = OxyColors.Black, LineStyle = LineStyle.Dash, Dashes = new double[] { 3.0 } };            

            var PmaxtextAnnotationRight = new TextAnnotation
            {
                Text = "N_{max}",
                TextPosition = new DataPoint(PminPmaxLength, iPmax),
                StrokeThickness = 0.0,
                FontSize = 16.0,
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Top
            };

            var PmaxtextAnnotationLeft = new TextAnnotation
            {
                Text = "N_{max}",
                TextPosition = new DataPoint(-PminPmaxLength, iPmax),
                StrokeThickness = 0.0,
                FontSize = 16.0,
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Right,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Top
            };

            var PmintextAnnotationRight = new TextAnnotation
            {
                Text = "N_{min}",
                TextPosition = new DataPoint(PminPmaxLength, iPmin),
                StrokeThickness = 0.0,
                FontSize = 16.0,
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Bottom
            };

            var PmintextAnnotationLeft = new TextAnnotation
            {
                Text = "N_{min}",
                TextPosition = new DataPoint(-PminPmaxLength, iPmin),
                StrokeThickness = 0.0,
                FontSize = 16.0,
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Right,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Bottom
            };

            foreach (var item in items)
            {
                nominalMPSerie.Points.Add(new DataPoint(item.Mnominal, item.Pnominal));               
            }

            actualUpperMPSerie.Points.AddRange(actualItems.Item1);
            actualLowerMPSerie.Points.AddRange(actualItems.Item2);

            PmaxSerie.Points.Add(new DataPoint(-PminPmaxLength, iPmax));
            PmaxSerie.Points.Add(new DataPoint(PminPmaxLength, iPmax));

            PminSerie.Points.Add(new DataPoint(-PminPmaxLength, iPmin));
            PminSerie.Points.Add(new DataPoint(PminPmaxLength, iPmin));

            // Serileri modele ekle
            if (ShowNominalDiagram)
                model.Series.Add(nominalMPSerie);

            model.Series.Add(actualUpperMPSerie);
            model.Series.Add(actualLowerMPSerie);

            model.Series.Add(PmaxSerie);
            model.Series.Add(PminSerie);

            if (ShowLoads)
                model.Series.Add(loadsSerie);


            model.Annotations.Add(PmaxtextAnnotationRight);
            model.Annotations.Add(PmaxtextAnnotationLeft);

            model.Annotations.Add(PmintextAnnotationRight);
            model.Annotations.Add(PmintextAnnotationLeft);

            return model;
        }

        private Tuple<List<DataPoint>, List<DataPoint>>  DivideDiagramByPmax(List<InteractionDiagramItem> items, double Pmax)
        {
            Line maxLine = new Line();
            maxLine.X1 = items.Max(i => i.Mactual);
            maxLine.X2 = items.Min(i => i.Mactual);
            maxLine.Y1 = maxLine.Y2 = Pmax;

            List<DataPoint> upperItems = new List<DataPoint>();
            List<DataPoint> lowerItems = new List<DataPoint>();
            Point intPnt = new Point(0.0, 0.0);
            bool addToUpper = false;

            for (int i = 0; i < items.Count-1; i++)
            {
                Line tmpLine = new Line();
                tmpLine.X1 = items[i].Mactual;
                tmpLine.Y1 = items[i].Pactual;
                tmpLine.X2 = items[i+1].Mactual;
                tmpLine.Y2 = items[i+1].Pactual;

                if (DoLinesIntersect(maxLine, tmpLine, ref intPnt))
                {
                    addToUpper = !addToUpper;

                    if(items[i].Pactual <= intPnt.Y)
                    {
                        upperItems.Add(new DataPoint(intPnt.X, intPnt.Y));
                        upperItems.Add(new DataPoint(items[i + 1].Mactual, items[i + 1].Pactual));

                        lowerItems.Add(new DataPoint(items[i].Mactual, items[i].Pactual));
                        lowerItems.Add(new DataPoint(intPnt.X, intPnt.Y));
                    }
                    else
                    {                        
                        upperItems.Add(new DataPoint(items[i].Mactual, items[i].Pactual));
                        upperItems.Add(new DataPoint(intPnt.X, intPnt.Y));

                        lowerItems.Add(new DataPoint(intPnt.X, intPnt.Y));
                        lowerItems.Add(new DataPoint(items[i+1].Mactual, items[i+1].Pactual));

                    }
                }
                else
                {
                    if(addToUpper)
                        upperItems.Add(new DataPoint(items[i].Mactual, items[i].Pactual));
                    else
                        lowerItems.Add(new DataPoint(items[i].Mactual, items[i].Pactual));
                }
            }

            if(!lowerItems.Last().Equals(new DataPoint(items.Last().Mactual, items.Last().Pactual)))
            {
                lowerItems.Add(new DataPoint(items.Last().Mactual, items.Last().Pactual));
            }

            return new Tuple<List<DataPoint>, List<DataPoint>>(upperItems, lowerItems);
 
        }

        private static bool DoLinesIntersect(Line L1, Line L2, ref Point ptIntersection)
        {
            // Denominator for ua and ub are the same, so store this calculation
            double d =
               (L2.Y2 - L2.Y1) * (L1.X2 - L1.X1)
               -
               (L2.X2 - L2.X1) * (L1.Y2 - L1.Y1);

            //n_a and n_b are calculated as seperate values for readability
            double n_a =
               (L2.X2 - L2.X1) * (L1.Y1 - L2.Y1)
               -
               (L2.Y2 - L2.Y1) * (L1.X1 - L2.X1);

            double n_b =
               (L1.X2 - L1.X1) * (L1.Y1 - L2.Y1)
               -
               (L1.Y2 - L1.Y1) * (L1.X1 - L2.X1);

            // Make sure there is not a division by zero - this also indicates that
            // the lines are parallel.  
            // If n_a and n_b were both equal to zero the lines would be on top of each 
            // other (coincidental).  This check is not done because it is not 
            // necessary for this implementation (the parallel check accounts for this).
            if (d == 0)
                return false;

            // Calculate the intermediate fractional point that the lines potentially intersect.
            double ua = n_a / d;
            double ub = n_b / d;

            // The fractional point will be between 0 and 1 inclusive if the lines
            // intersect.  If the fractional calculation is larger than 1 or smaller
            // than 0 the lines would need to be longer to intersect.
            if (ua >= 0d && ua <= 1d && ub >= 0d && ub <= 1d)
            {
                ptIntersection.X = L1.X1 + (ua * (L1.X2 - L1.X1));
                ptIntersection.Y = L1.Y1 + (ua * (L1.Y2 - L1.Y1));
                return true;
            }
            return false;
        }
    }
}
