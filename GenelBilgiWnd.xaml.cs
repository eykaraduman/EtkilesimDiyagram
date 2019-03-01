using RcDesign;
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
    /// Interaction logic for GenelBilgiWnd.xaml
    /// </summary>
    public partial class GenelBilgiWnd : Window
    {
        private string ReportTitle = "";
        private string DiagramTitle = "";
        private string DiagramSubTitle = "";
        private DesignCodes Code;

        public GenelBilgiWnd()
        {
            InitializeComponent();
            DataContext = Application.Current.MainWindow.DataContext;
            ReportTitle = ((AppFileTemplate)DataContext).ReportTitle;
            DiagramTitle = ((AppFileTemplate)DataContext).Title;
            DiagramSubTitle = ((AppFileTemplate)DataContext).SubTitle;
            Code = ((AppFileTemplate)DataContext).Code;
        }

        private void bCikis_Click(object sender, RoutedEventArgs e)
        {
            ((AppFileTemplate)DataContext).ReportTitle = ReportTitle;
            ((AppFileTemplate)DataContext).Title = DiagramTitle;
            ((AppFileTemplate)DataContext).SubTitle = DiagramSubTitle;
            ((AppFileTemplate)DataContext).Code = Code;
            this.Close();
        }

        private void bTamam_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
