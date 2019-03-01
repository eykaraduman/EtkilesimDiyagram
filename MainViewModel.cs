using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RcDesign.Material;
using RcDesign;
using RcDesign.Section;
using RcDesign.InteractionDiagram;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace EtkilesimDiyagram
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            DesignCodes code = DesignCodes.ACI318;

            List<ReinforcingBar> ReinforcingBars = new List<ReinforcingBar>();
            ReinforcingBars.Add(new ReinforcingBar(15.00 * 0.01 * 0.01, 0.085));
            ReinforcingBars.Add(new ReinforcingBar(25.00 * 0.01 * 0.01, 0.29));

            //ReinforcingBars.Add(new ReinforcingBar(15.00 * 0.01 * 0.01, 0.05));
            //ReinforcingBars.Add(new ReinforcingBar(15.00 * 0.01 * 0.01, 0.30));

            EtkilesimDiyagramHazirlama diagram = new EtkilesimDiyagramHazirlama();
            diagram.ShowAllDiagram = true;
            diagram.ShowNominalDiagram = true;
            diagram.Code = code;
            diagram.SectionGeometry = new RectangularSection(1.00, 0.35, 0.06, 0.085);
            diagram.StrengthReductionFactor = new StrengthReductionFactor(0.80, 0.90, 0.65);
            //diagram.StrengthReductionFactor = new StrengthReductionFactor(1.00, 1.00, 1.00);
            diagram.Concrete = new Concrete(code, 25.0);
            diagram.Steel = new Steel(code, 420);
            diagram.ReinforcingBars = ReinforcingBars;

            //System.Diagnostics.Debug.WriteLine(rcTmp.NominalPmin());
            //System.Diagnostics.Debug.WriteLine(rcTmp.NominalMmin());


            // Axes are created automatically if they are not defined

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            this.Model = diagram.CreateModel();
        }

        public PlotModel Model { get; private set; }
    }
}
