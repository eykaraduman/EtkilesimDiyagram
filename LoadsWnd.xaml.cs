using RcDesign;
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
    /// Interaction logic for LoadsWnd.xaml
    /// </summary>
    public partial class LoadsWnd : Window
    {
        public ObservableCollection<Load> oldLoads = new ObservableCollection<Load>();
        public LoadsWnd()
        {
            InitializeComponent();
            DataContext = Application.Current.MainWindow.DataContext;
            oldLoads = new ObservableCollection<Load>(((AppFileTemplate)DataContext).Loads.Clone());
        }

        private void bCikis_Click(object sender, RoutedEventArgs e)
        {
            ((AppFileTemplate)DataContext).Loads = (ObservableCollection<Load>)oldLoads;
            this.Close();
        }

        private void bTamam_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bPasteLoadsFromClipboard_Click(object sender, RoutedEventArgs e)
        {
            List<Load> recs = PanodanBilgiAl();
            if (recs != null)
                ((AppFileTemplate)DataContext).Loads = new ObservableCollection<Load>(recs);
        }

        private List<Load> PanodanBilgiAl()
        {
            List<Load> recs = new List<Load>();
            char[] rowSplitter = { '\n', '\r' };
            char[] columnSplitter = { '\t', ',' };
            //panodan metnin alınması
            try
            {
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                if (dataInClipboard == null)
                {
                    MessageBox.Show("Pano boş.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return null;
                }
                string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

                //satırlara bölünmesi
                string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
                string LoadName = "";
                double M = 0.0, N = 0.0;
                // satırlar arasında dolaşılması ve verinin hücrelere bölünmesi
                for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
                {
                    string[] valuesInRow = rowsInClipboard[iRow].Split(columnSplitter, StringSplitOptions.RemoveEmptyEntries);
                    if (valuesInRow.Length != 2)
                    {
                        MessageBox.Show("Panodaki kolon sayısı hatalı!\nKolon sayısı 2 olmalı.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    //Hücre bilgilerinin alınması
                    try
                    {
                        LoadName = valuesInRow[0];
                        M = Convert.ToDouble(valuesInRow[0]);
                        N = Convert.ToDouble(valuesInRow[1]);
                        int sign = Math.Sign(M);             
                        if (((AppFileTemplate)DataContext).Code == DesignCodes.TS500)
                        {
                            if (N > 0.0)
                            {
                                double e = Math.Abs(M / N);
                                double emin = ((AppFileTemplate)DataContext).MetrikKesitGeometri().emin;
                                if (emin > e)
                                {
                                    M = sign * Math.Abs(N * emin);
                                }
                            }
                        }
                        recs.Add(new Load(M, N));
                    }
                    catch
                    {
                        return null;
                    }
                }
                return recs;
            }
            catch
            {
                return null;
            }

        }

        
    }
}
