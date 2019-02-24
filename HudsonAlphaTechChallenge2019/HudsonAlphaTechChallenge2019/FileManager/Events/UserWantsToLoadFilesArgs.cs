using HudsonAlphaTechChallenge2019.FileManager.Model;
using System.Collections.Generic;

namespace HudsonAlphaTechChallenge2019.FileManager.Events
{
    public class UserWantsToLoadFilesArgs
    {
        public List<FileDataModel> FilePaths { get; set; }
    }
}