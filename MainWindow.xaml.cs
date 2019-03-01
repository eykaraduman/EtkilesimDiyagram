using CloneExtensions;
using iText=iTextSharp.text;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Wpf;
using RcDesign;
using RcDesign.InteractionDiagram;
using RcDesign.Material;
using RcDesign.Section;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnitsNet.Extensions.NumberToPressure;
using iTextSharp.text.pdf;
using System.Drawing;

namespace EtkilesimDiyagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string mFileFilter = "Karşılıklı Etkileşim Diyagram (*.xml)|*.xml";

        AppFileTemplate oldFile = null;

        string mFilePath;

        public MainWindow()
        {           
            InitializeComponent();
            DataContext = AppFileTemplate.CreateTemplate();
            oldFile = ((AppFileTemplate)DataContext).GetClone();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as AppFileTemplate).PropertyChanged += DataContextPropertyChangedEventHandler;
        }

        private void DataContextPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            List<string> prpNames = new List<string> { "ShowNominalDiagram", "ShowAllDiagram" , "ShowLeftDiagram", "ShowRightDiagram", "ShowGridLines", "ShowLoads", "ShowTitle" };
            if (prpNames.Contains(e.PropertyName))
            {
                PlotView.Model = (DataContext as AppFileTemplate).GetModel();
            }

            WriteToInfoBox();
        }

        private void mDosyaYeni_Click(object sender, RoutedEventArgs e)
        {
            string pFilePath = mFilePath;
            sbFileName.Text = mFilePath = SaveFile("", "Yeni", mFileFilter);
            if (string.IsNullOrEmpty(mFilePath))
            {
                sbFileName.Text = mFilePath = pFilePath;
                return;
            }
            else
            {
                if (File.Exists(mFilePath))
                    File.Delete(mFilePath);

                DataContext = AppFileTemplate.CreateTemplate();
                AppFileTemplate.Serialize((AppFileTemplate)DataContext, mFilePath);
                (DataContext as AppFileTemplate).PropertyChanged -= DataContextPropertyChangedEventHandler;
                (DataContext as AppFileTemplate).PropertyChanged += DataContextPropertyChangedEventHandler;
                PlotView.Model = (DataContext as AppFileTemplate).GetModel();
                WriteToInfoBox();
            }
        }

        private void mDosyaAc_Click(object sender, RoutedEventArgs e)
        {
            string pFilePath = mFilePath;
            sbFileName.Text = mFilePath = OpenFile("", "Aç", mFileFilter);
            if (string.IsNullOrEmpty(mFilePath))
            {
                sbFileName.Text = mFilePath = pFilePath;
                return;
            }
            else
            {
                DataContext = AppFileTemplate.DeSerialize(mFilePath);
                (DataContext as AppFileTemplate).PropertyChanged -= DataContextPropertyChangedEventHandler;
                (DataContext as AppFileTemplate).PropertyChanged += DataContextPropertyChangedEventHandler;
                PlotView.Model = (DataContext as AppFileTemplate).GetModel();
                WriteToInfoBox();
            }


        }

        private void mDosyaKaydet_Click(object sender, RoutedEventArgs e)
        {
            string pFilePath = mFilePath;
            if (!File.Exists(mFilePath))
            {
                sbFileName.Text = mFilePath = SaveFile("", "Kaydet", mFileFilter);
                if (string.IsNullOrEmpty(mFilePath))
                {
                    sbFileName.Text = mFilePath = pFilePath;
                    return;
                }
                else
                {
                    if (DataContext == null)
                        DataContext = AppFileTemplate.CreateTemplate();
                    oldFile = (AppFileTemplate)DataContext;

                    AppFileTemplate.Serialize((AppFileTemplate)DataContext, mFilePath);
                }
            }
            else
            {
                AppFileTemplate.Serialize((AppFileTemplate)DataContext, mFilePath);
            }
        }

        private void mDosyaFarkliKaydet_Click(object sender, RoutedEventArgs e)
        {
            string pFilePath = mFilePath;
            if (File.Exists(mFilePath))
            {
                sbFileName.Text = mFilePath = SaveFile("", "Farklı Kaydet", mFileFilter);
                if (string.IsNullOrEmpty(mFilePath))
                {
                    sbFileName.Text = mFilePath = pFilePath;
                    return;
                }
                else
                {
                    if (File.Exists(mFilePath))
                        File.Delete(mFilePath);

                    AppFileTemplate.Serialize((AppFileTemplate)DataContext, mFilePath);
                    (DataContext as AppFileTemplate).PropertyChanged -= DataContextPropertyChangedEventHandler;
                    (DataContext as AppFileTemplate).PropertyChanged += DataContextPropertyChangedEventHandler;
                }
            }
            else
            {
                MessageBox.Show("Aktif bir dosya bulunamadı!", Properties.Settings.Default.AppName, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void mCikis_Click(object sender, RoutedEventArgs e)
        {
            DataContext = oldFile;
            this.Close();
        }

        private void menuKesit_Click(object sender, RoutedEventArgs e)
        {
            SectionGeometryWnd sWnd = new SectionGeometryWnd();
            sWnd.Owner = this;
            sWnd.ShowDialog();
        }

        private void menuHesapla_Click(object sender, RoutedEventArgs e)
        {
            PlotView.Model = (DataContext as AppFileTemplate).GetModel();
            WriteToInfoBox();
        }

        private void menuShowFullDiagram_Click(object sender, RoutedEventArgs e)
        {
            if (!menuShowLeftDiagram.IsChecked && !menuShowRightDiagram.IsChecked)
            {
                menuShowFullDiagram.IsChecked = true;
                toolbarShowFullDiagram.IsChecked = true;
                return;
            }

            if(menuShowFullDiagram.IsChecked)
            {
                menuShowLeftDiagram.IsChecked = false;
                menuShowRightDiagram.IsChecked = false;

                toolbarShowLeftDiagram.IsChecked = false;
                toolbarShowRightDiagram.IsChecked = false;
            }
        }

        private void menuShowRightDiagram_Click(object sender, RoutedEventArgs e)
        {
            if (!menuShowLeftDiagram.IsChecked && !menuShowFullDiagram.IsChecked)
            {
                menuShowRightDiagram.IsChecked = true;
                toolbarShowRightDiagram.IsChecked = true;

                return;
            }

            if (menuShowRightDiagram.IsChecked)
            {
                menuShowLeftDiagram.IsChecked = false;
                menuShowFullDiagram.IsChecked = false;

                toolbarShowLeftDiagram.IsChecked = false;
                toolbarShowFullDiagram.IsChecked = false;
            }
        }

        private void menuShowLeftDiagram_Click(object sender, RoutedEventArgs e)
        {
            if (!menuShowRightDiagram.IsChecked && !menuShowFullDiagram.IsChecked)
            {
                menuShowLeftDiagram.IsChecked = true;

                toolbarShowLeftDiagram.IsChecked = true;
                return;
            }

            if (menuShowLeftDiagram.IsChecked)
            {
                menuShowRightDiagram.IsChecked = false;
                menuShowFullDiagram.IsChecked = false;

                toolbarShowRightDiagram.IsChecked = false;
                toolbarShowFullDiagram.IsChecked = false;
            }
        }

        private void toolbarShowFullDiagram_Click(object sender, RoutedEventArgs e)
        {
            if ((!toolbarShowLeftDiagram.IsChecked) == true  && (!toolbarShowRightDiagram.IsChecked) == true)
            {
                toolbarShowFullDiagram.IsChecked = true;
                menuShowFullDiagram.IsChecked = true;
                return;
            }

            if (toolbarShowFullDiagram.IsChecked == true)
            {
                toolbarShowLeftDiagram.IsChecked = false;
                toolbarShowRightDiagram.IsChecked = false;

                menuShowLeftDiagram.IsChecked = false;
                menuShowRightDiagram.IsChecked = false;
            }
        }

        private void toolbarShowLeftDiagram_Click(object sender, RoutedEventArgs e)
        {
            if (!toolbarShowRightDiagram.IsChecked == true && !toolbarShowFullDiagram.IsChecked == true)
            {
                toolbarShowLeftDiagram.IsChecked = true;

                menuShowLeftDiagram.IsChecked = true;
                return;
            }

            if (toolbarShowLeftDiagram.IsChecked == true)
            {
                toolbarShowRightDiagram.IsChecked = false;
                toolbarShowFullDiagram.IsChecked = false;

                menuShowRightDiagram.IsChecked = false;
                menuShowFullDiagram.IsChecked = false;
            }
        }

        private void toolbarShowRightDiagram_Click(object sender, RoutedEventArgs e)
        {
            if ((!toolbarShowLeftDiagram.IsChecked) == true && (!toolbarShowFullDiagram.IsChecked) == true)
            {
                toolbarShowRightDiagram.IsChecked = true;
                menuShowRightDiagram.IsChecked = true;

                return;
            }

            if (toolbarShowRightDiagram.IsChecked == true)
            {
                toolbarShowLeftDiagram.IsChecked = false;
                toolbarShowFullDiagram.IsChecked = false;

                menuShowLeftDiagram.IsChecked = false;
                menuShowFullDiagram.IsChecked = false;
            }
        }

        private void menuDonati_Click(object sender, RoutedEventArgs e)
        {
            ReinforcingWnd sWnd = new ReinforcingWnd();
            sWnd.Owner = this;
            sWnd.ShowDialog();
        }

        private void menuYukler_Click(object sender, RoutedEventArgs e)
        {
            LoadsWnd sWnd = new LoadsWnd();
            sWnd.Owner = this;
            sWnd.ShowDialog();
        }

        private void menuGenelBilgi_Click(object sender, RoutedEventArgs e)
        {
            GenelBilgiWnd sWnd = new GenelBilgiWnd();
            sWnd.Owner = this;
            sWnd.ShowDialog();
        }

        private void menuMalzeme_Click(object sender, RoutedEventArgs e)
        {
            MalzemeWnd sWnd = new MalzemeWnd();
            sWnd.Owner = this;
            sWnd.ShowDialog();
        }

        private void menuKapasiteAzaltma_Click(object sender, RoutedEventArgs e)
        {
            KapasiteAzaltmaWnd sWnd = new KapasiteAzaltmaWnd();
            sWnd.Owner = this;
            sWnd.ShowDialog();
        }

        public void WriteToInfoBox()
        {
            AppFileTemplate f = (AppFileTemplate)DataContext;
            FlowDocument flowDoc = new FlowDocument();
            Paragraph para = new Paragraph();

            para.Inlines.Add(new Bold(new Run("MALZEME:\n")));
            para.Inlines.Add(new Run(string.Format("fc={0:0} MPa\n", f.fc)));
            para.Inlines.Add(new Run(string.Format("fcd={0:0.00} MPa\n", f.Beton().fcd.TonnesForcePerSquareMeter().Megapascals)));
            para.Inlines.Add(new Run(string.Format("k1={0:0.000} MPa\n", f.Beton().k1)));
            para.Inlines.Add(new Run(string.Format("Ec={0} MPa\n", f.Beton().E.TonnesForcePerSquareMeter().Megapascals)));
            para.Inlines.Add(new Run(string.Format("fy={0:0.00} MPa\n", f.fy)));
            para.Inlines.Add(new Run(string.Format("fyd={0:0.00} MPa\n", f.DonatiCeligi().fyd.TonnesForcePerSquareMeter().Megapascals)));
            para.Inlines.Add(new Run(string.Format("Es={0} MPa\n\n", f.DonatiCeligi().E.TonnesForcePerSquareMeter().Megapascals)));

            para.Inlines.Add(new Bold(new Run("KESİT:\n")));
            para.Inlines.Add(new Run(string.Format("b={0} cm\n", f.b)));
            para.Inlines.Add(new Run(string.Format("h={0} cm\n", f.h)));
            para.Inlines.Add(new Run(string.Format("Alan={0} cm²\n\n", f.b * f.h)));

            para.Inlines.Add(new Bold(new Run("DONATI:\n")));

            int j = 1;
            foreach (var rb in f.ReinforcingBars)
            {
                para.Inlines.Add(new Run(string.Format("As{2}={0:0.00} cm² d{2}={1:0.00} cm\n", rb.As, rb.di, j)));
                j++;
            }
            para.Inlines.Add(new Run(string.Format("r=%{0:0.00}\n", 100.0 * f.ReinforcingBars.Sum(i => i.As) / (f.b * f.h))));

            flowDoc.Blocks.Add(para);
            infoBox.Document = flowDoc;
        }

        public static string OpenFile(string directoryPath, string title, string filter)
        {
            OpenFileDialog opd = new OpenFileDialog();
            //opd.InitialDirectory = directoryPath;
            opd.Filter = filter;
            opd.Title = title;
            opd.Multiselect = false;
            bool? rs = opd.ShowDialog();
            if (rs == true)
            {
                return opd.FileName;
            }
            else
                return null;
        }

        public static string SaveFile(string directoryPath, string title, string filter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;
            sfd.Title = title;
            sfd.OverwritePrompt = true;
            bool? rs = sfd.ShowDialog();
            if (rs == true)
            {
                return sfd.FileName;
            }
            else
                return null;
        }

        private void menuCreateMNReport_Click(object sender, RoutedEventArgs e)
        {
            string fileName = SaveFile("", "Kaydet", "Metin dsoyası (*.txt)|*.txt");
            if (!string.IsNullOrEmpty(fileName))
            {
                AppFileTemplate f = (AppFileTemplate)DataContext;
                InteractionDiagramItem.WriteToFile(f.GetItems(), fileName);
            }
        }

        private void menuCopyClipboard_Click(object sender, RoutedEventArgs e)
        {
            var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
            var bitmap = pngExporter.ExportToBitmap(PlotView.Model);
            Clipboard.SetImage(bitmap);
        }

        private void menuSaveAsPng_Click(object sender, RoutedEventArgs e)
        {
            string fileName = SaveFile("", "Kaydet", "İmaj dosyası (*.png)|*.png");
            if (!string.IsNullOrEmpty(fileName))
            {
                var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
                pngExporter.ExportToFile(PlotView.Model, fileName);
            }
           
        }

        private void menuSaveAsPdf_Click(object sender, RoutedEventArgs e)
        {
            string fileName = SaveFile("", "Kaydet", "Pdf dosyası (*.pdf)|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                using (var stream = File.Create(fileName))
                {
                    var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                
                    pdfExporter.Export(PlotView.Model, stream);
                }
            }
        }

        private void menuCreatePdfReport_Click(object sender, RoutedEventArgs e)
        {
            string fileName = SaveFile("", "Kaydet", "Pdf dsoyası (*.pdf)|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    CreateReport(fileName);
                }
                catch (PdfException de)
                {
                    MessageBox.Show(de.Message);
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(ioe.Message);
                }
            }
        }

        private void CreateReport(string filePath)
        {
            AppFileTemplate f = (AppFileTemplate)DataContext;

            // margins
            float left = 30;
            float right = 10;
            float top = 10;
            float bottom = 10;
            float headH = 20;
            float indent = 5;
     
            //string fontName = "C:\\WINDOWS\\FONTS\\CALIBRI.TTF";
            string fontName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "CALIBRI.TTF");
            iText.Font NormalSatirFont = iText.FontFactory.GetFont(fontName, "CP1254", 10, iText.Font.NORMAL);
            iText.Font BoldSatirFont = iText.FontFactory.GetFont(fontName, "CP1254", 10, iText.Font.BOLD);
            iText.Font NormalRiseFont = iText.FontFactory.GetFont(fontName, "CP1254", 8, iText.Font.NORMAL);
            iText.Font NormalSymbolFont = iText.FontFactory.GetFont(iText.FontFactory.SYMBOL, 10, iText.Font.NORMAL);
            iText.Document doc = new iText.Document(iText.PageSize.A4, left, right, top, bottom);

            float xLL = doc.GetLeft(left);
            float yLL = doc.GetBottom(bottom);
            float xUR = doc.GetRight(right);
            float yUR = doc.GetTop(top);
            float w = xUR - xLL;
            float h = yUR - yLL;
            float xUL = xLL;
            float yUL = yUR;
            float xLR = xUR;
            float yLR = yLL;
            //float graphW = w - 10;
            //float graphH = graphW * 2 / 3;

           
            float graphH = 3 * h / 5;
            float graphW = w - 10;


            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            // direct content
            PdfContentByte cb = writer.DirectContent;
            // çizgiler
            DrawLine(cb, xUR - w, yUR, xUR, yUR);
            DrawLine(cb, xUR - w, yUR, xLL, yLL);
            DrawLine(cb, xLL, yLL, xLL + w, yLL);
            DrawLine(cb, xLL + w, yLL, xUR, yUR);
            DrawLine(cb, xUL, yUL - headH, xUR, yUR - headH);
            DrawLine(cb, xUL, yUL - headH - graphH, xUR, yUR - headH - graphH);

            // başlık
            ColumnText ct = new ColumnText(cb);
            ct.Indent = indent;
            iText.Phrase txt = new iText.Phrase();
            txt.Add(new iText.Chunk(f.ReportTitle, NormalSatirFont));
            ct.SetSimpleColumn(txt, xUL, yUL - headH, xUR, yUR, headH / 1.5f, iText.Element.ALIGN_LEFT | iText.Element.ALIGN_MIDDLE);
            ct.Go();

            var stream = new MemoryStream();
            var pngExporter = new PngExporter { Width = (int)graphW, Height = (int)graphH, Background = OxyColors.White };
            pngExporter.Export(PlotView.Model, stream);

            iText.Image png = iText.Image.GetInstance(stream.GetBuffer());
            png.Alignment = iText.Element.ALIGN_CENTER | iText.Element.ALIGN_MIDDLE;
            png.SetAbsolutePosition(xUL, yUL - headH - graphH);
            doc.Add(png);

            float kstW = w/2;
            float kstH = (h - headH - graphH) / 1.5f;
            ColumnText ctKesit = new ColumnText(cb);
            ctKesit.Indent = indent;
            txt = new iText.Phrase();
            txt.Add(new iText.Chunk("Kesit\n", BoldSatirFont));
            txt.Add(new iText.Chunk(String.Format("Genişlik, b = {0:0} cm\n", f.b), NormalSatirFont));
            txt.Add(new iText.Chunk(String.Format("Yükseklik, h = {0:0} cm\n", f.h), NormalSatirFont));
            txt.Add(new iText.Chunk(String.Format("Alan, bxh = {0:0} cm²\n", f.b * f.h), NormalSatirFont));
            txt.Add(new iText.Chunk("\nMalzeme\n", BoldSatirFont));
            txt.Add(new iText.Chunk("f", NormalSatirFont));
            txt.Add(new iText.Chunk("c", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0} MPa   ", f.Beton().fc.TonnesForcePerSquareMeter().Megapascals), NormalSatirFont));
            txt.Add(new iText.Chunk("f", NormalSatirFont));
            txt.Add(new iText.Chunk("y", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0} MPa\n", f.DonatiCeligi().fy.TonnesForcePerSquareMeter().Megapascals), NormalSatirFont));

            txt.Add(new iText.Chunk("f", NormalSatirFont));
            txt.Add(new iText.Chunk("cd", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0} MPa   ", f.Beton().fcd.TonnesForcePerSquareMeter().Megapascals), NormalSatirFont));

            txt.Add(new iText.Chunk("f", NormalSatirFont));
            txt.Add(new iText.Chunk("yd", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0} MPa\n", f.DonatiCeligi().fyd.TonnesForcePerSquareMeter().Megapascals), NormalSatirFont));

            txt.Add(new iText.Chunk("E", NormalSatirFont));
            txt.Add(new iText.Chunk("c", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0} MPa   ", f.Beton().E.TonnesForcePerSquareMeter().Megapascals), NormalSatirFont));
            txt.Add(new iText.Chunk("E", NormalSatirFont));
            txt.Add(new iText.Chunk("s", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0} MPa\n", f.DonatiCeligi().E.TonnesForcePerSquareMeter().Megapascals), NormalSatirFont));
            txt.Add(new iText.Chunk("e", NormalSymbolFont));
            txt.Add(new iText.Chunk("cu", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0.0000} m/m   ", f.Beton().Ecu), NormalSatirFont));
            txt.Add(new iText.Chunk("e", NormalSymbolFont));
            txt.Add(new iText.Chunk("yd", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0.00000} m/m\n", f.DonatiCeligi().Eyd), NormalSatirFont));
            txt.Add(new iText.Chunk("k", NormalSatirFont));
            txt.Add(new iText.Chunk("1", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(String.Format(" = {0:0.000}\n", f.Beton().k1), NormalSatirFont));
            
            ctKesit.SetSimpleColumn(txt, xUL, yUL - headH - graphH - kstH, xUL + kstW, yUL - headH - graphH, 15, iText.Element.ALIGN_LEFT);
            ctKesit.Go();

            ColumnText ctDonati = new ColumnText(cb);
            txt = new iText.Phrase();
            txt.Add(new iText.Chunk("Donatı\n", BoldSatirFont));
            int j = 1;
            foreach (var rb in f.ReinforcingBars)
            {
                txt.Add(new iText.Chunk("A", NormalSatirFont));
                txt.Add(new iText.Chunk(string.Format("s{0}", j), NormalRiseFont).SetTextRise(-1.0f));
                txt.Add(new iText.Chunk(string.Format("={0:0.00} cm², ", rb.As), NormalSatirFont));
                txt.Add(new iText.Chunk("d", NormalSatirFont));
                txt.Add(new iText.Chunk(string.Format("{0}", j), NormalRiseFont).SetTextRise(-1.0f));
                txt.Add(new iText.Chunk(string.Format("={0:0.00} cm\n", rb.di), NormalSatirFont));
                j++;
            }
            txt.Add(new iText.Chunk("r", NormalSymbolFont));
            txt.Add(new iText.Chunk(string.Format("=%{0:0.00}\n", 100.0 * f.ReinforcingBars.Sum(i => i.As) / (f.b * f.h)), NormalSatirFont));
            txt.Add(new iText.Chunk("(d", NormalSatirFont));
            txt.Add(new iText.Chunk("i", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(":Kesit basınç yüzeyinin donatı eksenine uzaklığı)\n", NormalSatirFont));            
            txt.Add(new iText.Chunk("\nDayanım Azaltma Katsayıları\n", BoldSatirFont));
            txt.Add(new iText.Chunk("f", NormalSymbolFont));
            txt.Add(new iText.Chunk("a", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(string.Format(" = {0:0.00}, ", f.PhiA), NormalSatirFont));
            txt.Add(new iText.Chunk("f", NormalSymbolFont));
            txt.Add(new iText.Chunk("b", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(string.Format(" = {0:0.00}, ", f.PhiB), NormalSatirFont));
            txt.Add(new iText.Chunk("f", NormalSymbolFont));
            txt.Add(new iText.Chunk("c", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(string.Format(" = {0:0.00}", f.PhiC), NormalSatirFont));
            //txt.Add(new iText.Chunk("\n\nTasarım:\n", BoldSatirFont));
            //txt.Add(new iText.Chunk(f.Code.ToString(), NormalSatirFont));
            ctDonati.SetSimpleColumn(txt, xUL + kstW, yUL - headH - graphH - kstH, xUL + 2 * kstW, yUL - headH - graphH, 15, iText.Element.ALIGN_LEFT);
            ctDonati.Go();

            ColumnText ctTesir = new ColumnText(cb);
            ctTesir.Indent = indent;
            txt = new iText.Phrase();
            txt.Add(new iText.Chunk("Maksimum eksenel basınç, N", NormalSatirFont));
            txt.Add(new iText.Chunk("max", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(string.Format(" = {0:0.00} t\n", f.Pmax), NormalSatirFont));
            txt.Add(new iText.Chunk("Maksimum eksenel çekme, N", NormalSatirFont));
            txt.Add(new iText.Chunk("min", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(string.Format(" = {0:0.00} t\n\n", f.Pmin), NormalSatirFont));

            txt.Add(new iText.Chunk("Yönetmelik maksimum eksenel basınç sınırı, N", NormalSatirFont));
            txt.Add(new iText.Chunk("max", NormalRiseFont).SetTextRise(-1.0f));
            txt.Add(new iText.Chunk(string.Format(" = {0:0.00} t", f.ActualPmax), NormalSatirFont));
            ctTesir.SetSimpleColumn(txt, xUL, yUL - headH - graphH - 1.5f * kstH, xUL + kstW, yUL - headH - graphH - kstH, 15, iText.Element.ALIGN_LEFT);
            ctTesir.Go();

            doc.Close();
        }

        private void DrawLine(PdfContentByte cb, float x1, float y1, float x2, float y2)
        {
            cb.MoveTo(x1, y1);
            cb.LineTo(x2, y2);
            cb.Stroke();
        }

        private void DrawText(PdfContentByte cb, float x, float y, BaseFont font, float fonSize, string txt)
        {
            cb.BeginText();
            cb.SetTextMatrix(x, y);
            cb.SetFontAndSize(font, fonSize);
            cb.ShowText(txt);
            cb.EndText();
        }

       
    }
}
