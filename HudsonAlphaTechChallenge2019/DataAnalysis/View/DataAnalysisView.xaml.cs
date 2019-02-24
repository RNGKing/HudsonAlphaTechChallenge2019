using CsvHelper;
using HudsonAlphaTechChallenge2019.DataAnalysis.ViewModels;
using HudsonAlphaTechChallenge2019.FileManager.Model;
using System;
using System.Collections.Generic;
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

namespace HudsonAlphaTechChallenge2019.DataAnalysis.View
{
    /// <summary>
    /// Interaction logic for DataAnalysisView.xaml
    /// </summary>
    public partial class DataAnalysisView : UserControl
    {
        public DataAnalysisView()
        {
            InitializeComponent();
            DataContext = new DataAnalysisViewModel();
        }

        public void LoadData(List<FileDataModel> files)
        {
            (DataContext as DataAnalysisViewModel).LoadData(files);
        }

        public void DrawInformation()
        {
            var model = (DataContext as DataAnalysisViewModel).GeneticData;
            var maxVal = model.Max(x => x.EndPosition);
            var scalingRatio = RenderSurface.Width / maxVal;
            foreach (var g in model)
            {
                var rect = new Rectangle();
                rect.Width = (scalingRatio * g.EndPosition) - (scalingRatio * g.StartPosition);
                rect.Height = 45;
                rect.Fill = new SolidColorBrush(Colors.Red);
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.StrokeThickness = 2;
                Canvas.SetLeft(rect, (scalingRatio * g.StartPosition));
                
                RenderSurface.Children.Add(rect);
            }
        }

        private void BtnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            // This is where we'd structure a query
        }

        private void BtnSaveToCSV_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if(dlg.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(dlg.FileName);
                using (var writer = new StreamWriter(fileInfo.FullName))
                {
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords((DataContext as DataAnalysisViewModel).GeneticData);
                    }
                }
            }
        }

        private void BtnSaveToJSON_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(dlg.FileName);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject((DataContext as DataAnalysisViewModel).GeneticData);
                using (var writer = new StreamWriter(fileInfo.FullName))
                {
                    writer.Write(jsonData);
                }
            }
        }
    }
}
