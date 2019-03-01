using System;
using System.Collections.Generic;
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
    /// Interaction logic for KapasiteAzaltmaWnd.xaml
    /// </summary>
    public partial class KapasiteAzaltmaWnd : Window
    {
        private double Phia = 0.0;
        private double Phib = 0.0;
        private double Phic = 0.0;

        public KapasiteAzaltmaWnd()
        {
            InitializeComponent();
            DataContext = Application.Current.MainWindow.DataContext;
            Phia = ((AppFileTemplate)DataContext).PhiA;
            Phib = ((AppFileTemplate)DataContext).PhiB;
            Phic = ((AppFileTemplate)DataContext).PhiC;

            if(((AppFileTemplate)DataContext).Code == RcDesign.DesignCodes.TS500)
            {
                txtPhib.Value = 1.00;
                txtPhic.Value = 1.00;
                txtPhib.IsEnabled = false;
                txtPhic.IsEnabled = false;
            }
        }

        private void bCikis_Click(object sender, RoutedEventArgs e)
        {
            ((AppFileTemplate)DataContext).PhiA = Phia;
            ((AppFileTemplate)DataContext).PhiB = Phib;
            ((AppFileTemplate)DataContext).PhiC = Phic;
            this.Close();
        }

        private void bTamam_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
