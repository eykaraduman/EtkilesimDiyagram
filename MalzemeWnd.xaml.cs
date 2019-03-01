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
    /// Interaction logic for MalzemeWnd.xaml
    /// </summary>
    public partial class MalzemeWnd : Window
    {
        private double fc = 0.0;
        private double fy = 0.0;

        public MalzemeWnd()
        {
            InitializeComponent();
            DataContext = Application.Current.MainWindow.DataContext;
            fc = ((AppFileTemplate)DataContext).fc;
            fy = ((AppFileTemplate)DataContext).fy;
        }

        private void bCikis_Click(object sender, RoutedEventArgs e)
        {
            ((AppFileTemplate)DataContext).fc = fc;
            ((AppFileTemplate)DataContext).fy = fy;
            this.Close();
        }

        private void bTamam_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
