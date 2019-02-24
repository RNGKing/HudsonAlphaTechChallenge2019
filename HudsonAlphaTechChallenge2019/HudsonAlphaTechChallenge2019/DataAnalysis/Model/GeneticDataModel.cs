using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HudsonAlphaTechChallenge2019.DataAnalysis.Model
{
    public class GeneticDataModel
    {
        public string ChromosomeName { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public string FileName { get; set; }
        public int NumberOfFilesFoundIn { get; set; }
        public int NumberMatchingBasePairs { get; set; }
        
    }
}
