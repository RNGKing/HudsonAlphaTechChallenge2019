using HudsonAlphaTechChallenge2019.DataAnalysis.Model;
using HudsonAlphaTechChallenge2019.FileManager.Model;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Linq;

namespace HudsonAlphaTechChallenge2019.DataAnalysis.ViewModels
{
    class DataAnalysisViewModel
    {
        public ObservableCollection<GeneticDataModel> GeneticData { get; set; } = new ObservableCollection<GeneticDataModel>();
        public ObservableCollection<GeneticDataModel> BackingData { get; set; } = new ObservableCollection<GeneticDataModel>();

        public ICollectionView collectionView { get
            {
                return CollectionViewSource.GetDefaultView(GeneticData);
            } }

        public List<List<GeneticDataModel>> DataCache = new List<List<GeneticDataModel>>();

        public void LoadData(List<FileDataModel> files)
        {
            
            foreach(var f in files)
            {
                Console.WriteLine(f.FileName);
                DataCache.Add(BuildModel(f));
                
            }
            
            foreach (var r in DataCache[0])
            {
                GeneticData.Add(r);

                var avgPosX = (r.StartPosition + r.EndPosition) / 2;

            }

            BackingData = GeneticData;

            Console.WriteLine($"Genetic Data: {GeneticData.Count}");
        }

        

        private void DrawData(List<GeneticDataModel> list)
        {
            var largestPoint = list.Max(x => x.EndPosition);
            
        }

        public void FilterData(int minMatches, int maxMatches, int minBase, int maxBase)
        {
            var query = from a in GeneticData
                        where a.NumberOfFilesFoundIn > minMatches &&
                          a.NumberOfFilesFoundIn < maxMatches && a.NumberMatchingBasePairs > minBase &&
                          a.NumberMatchingBasePairs < maxBase
                        select a;
            BackingData = new ObservableCollection<GeneticDataModel>(query);
        }

        public List<GeneticDataModel> BuildModel(FileDataModel model)
        {
            string[] lines = System.IO.File.ReadAllLines(model.FilePath);
            var list = new List<GeneticDataModel>();
            foreach(var line in lines)
            {
                var data = new GeneticDataModel();
                string[] cols = line.Split('\t');
                if(cols[5] == "+" || cols[5] == "-")
                {
                    data.ChromosomeName = cols[0];
                    data.StartPosition = int.Parse(cols[1]);
                    data.EndPosition = int.Parse(cols[2]);
                }
                else
                {
                    data.ChromosomeName = cols[0];
                    data.StartPosition = int.Parse(cols[1]);
                    data.EndPosition = int.Parse(cols[2]);
                    data.FileName = cols[3];
                    data.NumberOfFilesFoundIn = int.Parse(cols[4]);
                    data.NumberMatchingBasePairs = int.Parse(cols[5]);
                }


                list.Add(data);
            }
            return list;
        }

    }

    

}
