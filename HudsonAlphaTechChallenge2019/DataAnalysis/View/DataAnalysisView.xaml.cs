using CsvHelper;
using HudsonAlphaTechChallenge2019.DataAnalysis.ViewModels;
using HudsonAlphaTechChallenge2019.FileManager.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            DrawData();
        }

        private void DrawData()
        {
            var maximumVal = (DataContext as DataAnalysisViewModel).GeneticData.Max(x => x.EndPosition);
            var scaleRatio = canvasData.Width / maximumVal;
            int yOffset = 0;
            foreach (var dc in (DataContext as DataAnalysisViewModel).DataCache)
            {
                foreach(var data in dc)
                {
                    Rectangle rect = new Rectangle();
                    var width = (data.EndPosition - data.StartPosition) * scaleRatio * 1000;
                    var height = 35;
                    rect.Width = width;
                    rect.Height = height;
                    rect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#cedc38"));


                    Canvas.SetLeft(rect, data.StartPosition * scaleRatio * 1000);
                    Canvas.SetTop(rect, yOffset);
                    
                    canvasData.Children.Add(rect);
                }
                yOffset += 75;
                Line line = new Line();
                line.X1 = 0;
                line.X2 = canvasData.Width;
                line.Y1 = yOffset - 20;
                line.Y2 = yOffset - 20;
                line.Stroke = new SolidColorBrush(Colors.White);
                line.StrokeThickness = 15;
                canvasData.Children.Add(line);
            }
        }

       

        private void BtnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            // This is where we'd structure a query
            // But For this hackathon I will not do that, intead filter the data

            

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
