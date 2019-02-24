using CsvHelper;
using HudsonAlphaTechChallenge2019.DataAnalysis.Model;
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
        List<GeneticDataModel> filteredData = new List<GeneticDataModel>();

        public DataAnalysisView()
        {
            InitializeComponent();
            DataContext = new DataAnalysisViewModel();
            
        }

        public void LoadData(List<FileDataModel> files)
        {
            (DataContext as DataAnalysisViewModel).LoadData(files);
            var yOffset = 5;
            foreach (var file in files)
            {
                TextBlock blk = new TextBlock();
                blk.Text = file.FileName;
                Canvas.SetLeft(blk,10);
                Canvas.SetTop(blk, yOffset);
                yOffset += 75;
                canvasNames.Children.Add(blk);
            }            
            DrawData();
        }

        private void DrawData()
        {
            canvasData.Children.Clear();

            var maximumVal = (DataContext as DataAnalysisViewModel).GeneticData.Max(x => x.EndPosition);
            var scaleRatio = canvasData.Width / maximumVal;
            int yOffset = 20;
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
                line.Y1 = yOffset - 30;
                line.Y2 = yOffset - 30;
                line.Stroke = new SolidColorBrush(Colors.White);
                line.StrokeThickness = 15;
                canvasData.Children.Add(line);
            }
        }

        private void DrawData(List<List<GeneticDataModel>> dataModel)
        {
            canvasData.Children.Clear();

            var maximumVal = (DataContext as DataAnalysisViewModel).GeneticData.Max(x => x.EndPosition);
            var scaleRatio = canvasData.Width / maximumVal;
            int yOffset = 20;
            foreach (var dc in dataModel)
            {
                foreach (var data in dc)
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
                line.Y1 = yOffset - 30;
                line.Y2 = yOffset - 30;
                line.Stroke = new SolidColorBrush(Colors.White);
                line.StrokeThickness = 15;
                canvasData.Children.Add(line);
            }
        }
        
            
        

        private void BtnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            // This is where we'd structure a query
            // But For this hackathon I will not do that, intead filter the data
            var minNumMatches = int.Parse(txtbxMinNumberInteraction.Text);
            var maxNumMatches = int.Parse(txtbxMaxNumInteractions.Text);
            var minNumBasePair = int.Parse(txtbxMinBasePair.Text);
            var maxNumBasePair = int.Parse(txtbxMaxBasePair.Text);
            var collection = (DataContext as DataAnalysisViewModel).GeneticData;
            var DataCollection = (DataContext as DataAnalysisViewModel).DataCache;
            var filter = from a in collection
                         where a.NumberOfFilesFoundIn > minNumMatches
    && a.NumberOfFilesFoundIn < maxNumMatches
    && a.NumberMatchingBasePairs > minNumBasePair
    && a.NumberMatchingBasePairs < maxNumBasePair
                         select a;
            filteredData = new List<GeneticDataModel>(filter);
            List<List<GeneticDataModel>> tempList = new List<List<GeneticDataModel>>();
            tempList.Add(filteredData);
            foreach(var data in DataCollection)
            {
                tempList.Add(data);
            }
            DrawData(tempList);


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
