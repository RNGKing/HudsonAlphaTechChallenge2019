using HudsonAlphaTechChallenge2019.FileManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HudsonAlphaTechChallenge2019.FileManager.ViewModel
{
    class FileManagerViewModel
    {
        public ObservableCollection<FileDataModel> FileDataCollection { get; set; } = new ObservableCollection<FileDataModel>(); 
    }
}
