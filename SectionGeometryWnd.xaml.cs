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
    /// Interaction logic for SectionGeometryWnd.xaml
    /// </summary>
    public partial class SectionGeometryWnd : Window
    {
        private double b = 0.0;
        private double h = 0.0;
        public SectionGeometryWnd()
        {
            InitializeComponent();
            DataContext = Application.Current.MainWindow.DataContext;
            b = ((AppFileTemplate)DataContext).b;
            h = ((AppFileTemplate)DataContext).h;

            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void bCikis_Click(object sender, RoutedEventArgs e)
        {
            ((AppFileTemplate)DataContext).b = b;
            ((AppFileTemplate)DataContext).h = h;
            this.Close();
        }

        private void bTamam_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
