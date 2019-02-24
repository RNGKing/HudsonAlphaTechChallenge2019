using HudsonAlphaTechChallenge2019.FileManager.Events;
using HudsonAlphaTechChallenge2019.FileManager.Model;
using HudsonAlphaTechChallenge2019.FileManager.ViewModel;
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

namespace HudsonAlphaTechChallenge2019.FileManager.View
{
    /// <summary>
    /// Interaction logic for FileLoader.xaml
    /// </summary>
    public partial class FileLoader : UserControl
    {
        public event UserWantsToLoadFiles OnUserWantsToLoadFiles;

        public FileLoader()
        {
            InitializeComponent();
            DataContext = new FileManagerViewModel();
        }

        private void BtnLoadFile_Click(object sender, RoutedEventArgs e)
        {

            // shows the open file dialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if(dlg.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(dlg.FileName);
                var fileData = new FileDataModel { FilePath = fileInfo.FullName, FileName = fileInfo.Name };
                (DataContext as FileManagerViewModel).FileDataCollection.Add(fileData);
            }
        }

        private void BtnAnalyzeData_Click(object sender, RoutedEventArgs e)
        {
            var tempList = new List<FileDataModel>();
            foreach(var file in (DataContext as FileManagerViewModel).FileDataCollection)
            {
                tempList.Add(file);
            }
            OnUserWantsToLoadFiles?.Invoke(this, new UserWantsToLoadFilesArgs { FilePaths = tempList });
        }
    }
}
