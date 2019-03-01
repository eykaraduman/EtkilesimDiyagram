using RcDesign.InteractionDiagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EtkilesimDiyagram
{
    /// <summary>
    /// Interaction logic for ReinforcingWnd.xaml
    /// </summary>
    public partial class ReinforcingWnd : Window
    {
        public ObservableCollection<ReinforcingBar> oldReinforcingBars = new ObservableCollection<ReinforcingBar>();
        public ReinforcingWnd()
        {
            InitializeComponent();
            DataContext = Application.Current.MainWindow.DataContext;
            oldReinforcingBars = new ObservableCollection<ReinforcingBar>(((AppFileTemplate)DataContext).ReinforcingBars.Clone());
        }

        private void bCikis_Click(object sender, RoutedEventArgs e)
        {
            ((AppFileTemplate)DataContext).ReinforcingBars = (ObservableCollection<ReinforcingBar>)oldReinforcingBars;
            this.Close();
        }

        private void bTamam_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).WriteToInfoBox();
            Close();
        }
    }
}
