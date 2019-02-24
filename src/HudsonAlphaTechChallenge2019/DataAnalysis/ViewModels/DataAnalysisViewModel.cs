using HudsonAlphaTechChallenge2019.DataAnalysis.Model;
using HudsonAlphaTechChallenge2019.FileManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HudsonAlphaTechChallenge2019.DataAnalysis.ViewModels
{
    class DataAnalysisViewModel
    {
        public ObservableCollection<GeneticDataModel> GeneticData { get; set; } = new ObservableCollection<GeneticDataModel>();

        List<List<GeneticDataModel>> DataCache = new List<List<GeneticDataModel>>();

        public void LoadData(List<FileDataModel> files)
        {
            foreach(var f in files)
            {
                DataCache.Add(BuildModel(f));
            }
            
            foreach (var r in DataCache[0])
            {
                GeneticData.Add(r);
            }
        }

        private void DrawData(List<GeneticDataModel> list)
        {
            var largestPoint = list.Max(x => x.EndPosition);
            


        }

        public List<GeneticDataModel> BuildModel(FileDataModel model)
        {
            string[] lines = System.IO.File.ReadAllLines(model.FilePath);
            var list = new List<GeneticDataModel>();
            foreach(var line in lines)
            {
                var data = new GeneticDataModel();
                string[] cols = line.Split('\t');
                data.ChromosomeName = cols[0];
                data.StartPosition = int.Parse(cols[1]);
                data.EndPosition = int.Parse(cols[2]);
                data.FileName = cols[3];
                data.NumberOfFilesFoundIn = int.Parse(cols[4]);
                data.NumberMatchingBasePairs = int.Parse(cols[5]);
                list.Add(data);
            }
            return list;
        }

    }

    

}
