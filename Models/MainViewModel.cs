using Notepad.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notepad;

namespace Notepad
{
    class MainViewModel : ViewModelBase
    {
        string fpath = Environment.CurrentDirectory;

        private string text {  get; set; }
        public string Text { get => text; set { text = value; OnPropertyChanged(); } }

        #region Commands
        private FileManager _fileManager = new();

        #endregion
        private FileManager _fileManager = new();
        private string _input = "Hello World!";
        public string Input { get { return _input; } set {  _input = value; OnPropertyChanged(); } }
        private DelegateCommand? _openFileCommand;

        public DelegateCommand OpenFileDelegateCommand => _openFileCommand ??= new DelegateCommand(ExecuteOpenFile);
            private void ExecuteOpenFile()
        { 

        }

        private DelegateCommand? _saveToFileCommand;

        public DelegateCommand SaveToFileCommand => _saveToFileCommand ??= new DelegateCommand(ExecuteSaveToFile);
        
       private void ExecuteSaveToFile()
        {
            _fileManager.SaveTextToFile(fpath, Text);
        }
    }
}
