using Notepad.Models;
using Prism.Commands;
using Notepad.Core;
using System.Windows;
using Microsoft.Win32;

namespace Notepad.Models;

class MainViewModel : ViewModel
{

    private readonly FileManager _fileManager = new();

    private DelegateCommand? _openFileDelegateCommand;

    private DelegateCommand? _saveToFileCommand;

    private DelegateCommand? _saveToFileAsCommand;

    private FileViewModel _file = new();

    public DelegateCommand OpenFileDelegateCommand => _openFileDelegateCommand ??= new DelegateCommand(ExecuteOpenFile);

    public DelegateCommand SaveToFileCommand => _saveToFileCommand ??= new DelegateCommand(ExecuteSaveToFile);

    public FileViewModel File
    {
        get => _file;
        set {
            _file = value; 
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        _fileManager.ErrorOccured += FileManagerOnErrorOccured;
    }

    private void FileManagerOnErrorOccured(object? sender, FileSavingCompletedEventArgs e)
    {
        if (Application.Current.MainWindow != null)
            MessageBox.Show(Application.Current.MainWindow, e.Message);
    }

    private void ExecuteOpenFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            InitialDirectory = "c:\\",
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() != true) return;

        File = _fileManager.GetFileFromPath(openFileDialog.FileName);
        File.Title = openFileDialog.Title;
    }

   private void ExecuteSaveToFile()
    {
        if (string.IsNullOrEmpty(_file.Path)) ExecuteSaveToFileAs();
    }

    private void ExecuteSaveToFileAs()
    {
        var saveFileDialog = new OpenFileDialog
        {
            InitialDirectory = "c:\\",
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (saveFileDialog.ShowDialog() != true) return;

        if (_fileManager.SaveFile(_file)) MessageBox.Show("Saved!");
    }
}
