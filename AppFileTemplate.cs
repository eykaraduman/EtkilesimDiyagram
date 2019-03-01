using Polenter.Serialization;
using RcDesign;
using RcDesign.InteractionDiagram;
using RcDesign.Material;
using RcDesign.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet.Extensions.NumberToLength;
using UnitsNet.Extensions.NumberToArea;
using System.ComponentModel;
using OxyPlot;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EtkilesimDiyagram
{
    public static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
    [Serializable]
    public class AppFileTemplate : INotifyPropertyChanged
    {

        DesignCodes code;
        public DesignCodes Code
        {
            get { return code; }
            set
            {
                if (code == value)
                    return;
                code = value;
                OnPropertyChanged("Code");
            }
        }

        double _fc;
        public double fc
        {
            get { return _fc; }
            set
            {
                if (_fc == value)
                    return;
                _fc = value;
                OnPropertyChanged("fc");
            }
        }

        double _fy;
        public double fy
        {
            get { return _fy; }
            set
            {
                if (_fy == value)
                    return;
                _fy = value;
                OnPropertyChanged("fy");
            }
        }

        double _h;
        public double h
        {
            get { return _h; }
            set
            {
                if (_h == value)
                    return;
                _h = value;
                OnPropertyChanged("h");
            }
        }

        double _b;
        public double b
        {
            get { return _b; }
            set
            {
                if (_b == value)
                    return;
                _b = value;
                OnPropertyChanged("b");
            }
        }

        double phiA;
        public double PhiA
        {
            get { return phiA; }
            set
            {
                if (phiA == value)
                    return;
                phiA = value;
                OnPropertyChanged("PhiA");
            }
        }

        double phiB;
        public double PhiB
        {
            get { return phiB; }
            set
            {
                if (phiB == value)
                    return;
                phiB = value;
                OnPropertyChanged("PhiB");
            }
        }

        double phiC;
        public double PhiC
        {
            get { return phiC; }
            set
            {
                if (phiC == value)
                    return;
                phiC = value;
                OnPropertyChanged("PhiC");
            }
        }

        ObservableCollection<ReinforcingBar> reinforcingBars;
        public ObservableCollection<ReinforcingBar> ReinforcingBars
        {
            get { return reinforcingBars; }
            set
            {
                if (reinforcingBars == value)
                    return;
                reinforcingBars = value;
                OnPropertyChanged("ReinforcingBars");
            }
        }


        ObservableCollection<Load> loads;
        public ObservableCollection<Load> Loads
        {
            get { return loads; }
            set
            {
                if (loads == value)
                    return;
                loads = value;
                OnPropertyChanged("Loads");
            }
        }

        string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged("Title");
            }
        }

        string reportTitle;
        public string ReportTitle
        {
            get { return reportTitle; }
            set
            {
                if (reportTitle == value)
                    return;
                reportTitle = value;
                OnPropertyChanged("ReportTitle");
            }
        }

        string subTitle;
        public string SubTitle
        {
            get { return subTitle; }
            set
            {
                if (subTitle == value)
                    return;
                subTitle = value;
                OnPropertyChanged("SubTitle");
            }
        }

        bool showAllDiagram;
        public bool ShowAllDiagram
        {
            get { return showAllDiagram; }
            set
            {
                if (showAllDiagram == value)
                    return;
                showAllDiagram = value;
                OnPropertyChanged("ShowAllDiagram");
            }
        }

        bool showRightDiagram;
        public bool ShowRightDiagram
        {
            get { return showRightDiagram; }
            set
            {
                if (showRightDiagram == value)
                    return;
                showRightDiagram = value;
                OnPropertyChanged("ShowRightDiagram");
            }
        }

        bool showLeftDiagram;
        public bool ShowLeftDiagram
        {
            get { return showLeftDiagram; }
            set
            {
                if (showLeftDiagram == value)
                    return;
                showLeftDiagram = value;
                OnPropertyChanged("ShowLeftDiagram");
            }
        }

        bool showNominalDiagram;
        public bool ShowNominalDiagram
        {
            get { return showNominalDiagram; }
            set
            {
                if (showNominalDiagram == value)
                    return;
                showNominalDiagram = value;
                OnPropertyChanged("ShowNominalDiagram");
            }
        }

        bool showGridLines;
        public bool ShowGridLines
        {
            get { return showGridLines; }
            set
            {
                if (showGridLines == value)
                    return;
                showGridLines = value;
                OnPropertyChanged("ShowGridLines");
            }
        }

        bool showLoads;
        public bool ShowLoads
        {
            get { return showLoads; }
            set
            {
                if (showLoads == value)
                    return;
                showLoads = value;
                OnPropertyChanged("ShowLoads");
            }
        }

        bool showTitle;
        public bool ShowTitle
        {
            get { return showTitle; }
            set
            {
                if (showTitle == value)
                    return;
                showTitle = value;
                OnPropertyChanged("ShowTitle");
            }
        }

        public double Pmax { get; set; }
        public double Pmin { get; set; }
        public double ActualPmax { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static AppFileTemplate CreateTemplate()
        {
            AppFileTemplate dsy = new AppFileTemplate();

            dsy.Code = DesignCodes.TS500;
            dsy.fc = 25.00;
            dsy.fy = 420.0;
            dsy.b = 100.0;
            dsy.h = 35.0;

            dsy.ReinforcingBars = new ObservableCollection<ReinforcingBar>
            {
                    new ReinforcingBar(15.00, 8.5),
                    new ReinforcingBar(25.00, 28.5)
            };

            dsy.Loads = new ObservableCollection<Load>
            {
                new Load(20.0, 200.0),
                new Load(-20.0, 200.0)
            };

            dsy.PhiA = dsy.PhiB = dsy.PhiC = 1.0;

            dsy.Title = "Etkileşim Diyagram";
            dsy.SubTitle = "(h=35 cm)";
            dsy.ReportTitle = "";
            dsy.ShowNominalDiagram = true;
            dsy.ShowLeftDiagram = false;
            dsy.ShowRightDiagram = false;
            dsy.ShowAllDiagram = true;
            dsy.ShowGridLines = true;
            dsy.ShowLoads = true;
            dsy.ShowTitle = true;

            return dsy;
        }

        public static void Serialize(AppFileTemplate obj, string fileName)
        {
            SharpSerializer serializer = new SharpSerializer();
            serializer.Serialize(obj, fileName);
        }

        public static AppFileTemplate DeSerialize(string fileName)
        {
            SharpSerializer serializer = new SharpSerializer();
            return
                (AppFileTemplate)(serializer.Deserialize(fileName));
        }

        public Concrete Beton()
        {
            return new Concrete(Code, fc);
        }

        public Steel DonatiCeligi()
        {
            return new Steel(Code, fy);
        }

        public RectangularSection MetrikKesitGeometri()
        {
            return new RectangularSection(b.Centimeters().Meters, h.Centimeters().Meters,
                8.5.Centimeters().Meters, 6.5.Centimeters().Meters);
        }

        public List<ReinforcingBar> MetrikDonatiKatmanBilgi()
        {
            List<ReinforcingBar> ReinforcingBarsMetric = ReinforcingBars.Clone() as List<ReinforcingBar>;
            ReinforcingBarsMetric.ForEach(item => item.As = item.As.SquareCentimeters().SquareMeters);
            ReinforcingBarsMetric.ForEach(item => item.di = item.di.Centimeters().Meters);

            return ReinforcingBarsMetric;
        }

        public PlotModel GetModel()
        {
            EtkilesimDiyagramHazirlama diagram = new EtkilesimDiyagramHazirlama();
            diagram.ShowAllDiagram = ShowAllDiagram;
            diagram.ShowRightDiagram = ShowRightDiagram;
            diagram.ShowLeftDiagram = ShowLeftDiagram;
            diagram.ShowNominalDiagram = ShowNominalDiagram;
            diagram.ShowGridLines = ShowGridLines;
            diagram.ShowLoads = ShowLoads;
            diagram.Code = Code;
            diagram.Title = Title;
            diagram.SubTitle = SubTitle;
            diagram.ShowTitle = ShowTitle;          
            diagram.SectionGeometry = MetrikKesitGeometri();
            diagram.StrengthReductionFactor = new StrengthReductionFactor(PhiA, PhiB, PhiC);
            diagram.Concrete = Beton();
            diagram.Steel = DonatiCeligi();
            diagram.ReinforcingBars = MetrikDonatiKatmanBilgi();
            diagram.Loads = (List<Load>)Loads.Clone();

            PlotModel model = diagram.CreateModel();

            Pmax = diagram.Pmax;
            Pmin = diagram.Pmin;
            ActualPmax = diagram.ActualPmax;
       
            return model;
        }

        public List<InteractionDiagramItem> GetItems()
        {
            EtkilesimDiyagramHazirlama diagram = new EtkilesimDiyagramHazirlama();
            diagram.Code = Code;
            diagram.SectionGeometry = MetrikKesitGeometri();
            diagram.StrengthReductionFactor = new StrengthReductionFactor(PhiA, PhiB, PhiC);
            diagram.Concrete = Beton();
            diagram.Steel = DonatiCeligi();
            diagram.ReinforcingBars = MetrikDonatiKatmanBilgi();
            return diagram.CreateInteractionDiagram();
        }
    }
}
