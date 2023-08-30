using System;
using System.Diagnostics;
using Prism.Commands;
using Notepad.Core;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Navigation;

namespace Notepad.Models;

internal class MainViewModel : ViewModel{
    private readonly FileManager _fileManager = new();

    private File _file = new();

    private DelegateCommand? _newFileDelegateCommand;

    private DelegateCommand? _openFileDelegateCommand;

    private DelegateCommand? _saveToFileCommand;

    private DelegateCommand? _saveToFileAsCommand;

    private int _row = 1;

    private int _column = 1;

    public DelegateCommand OpenFileDelegateCommand => _openFileDelegateCommand ??= new DelegateCommand(ExecuteOpenFile);

    public DelegateCommand NewFileDelegateCommand => _newFileDelegateCommand ??= new DelegateCommand(ExecuteNewFile);

    public DelegateCommand SaveToFileCommand => _saveToFileCommand ??= new DelegateCommand(ExecuteSaveToFile);

    public DelegateCommand SaveToFileAsCommand => _saveToFileAsCommand ??= new DelegateCommand(ExecuteSaveToFileAs);

    public int Row{
        get => _row;
        set{
            _row = value;
            OnPropertyChanged();
        }
    }

    public int Column{
        get => _column;
        set{
            _column = value;
            OnPropertyChanged();
        }
    }

    public File File{
        get => _file;
        private set{
            _file = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel(){
        _fileManager.ErrorOccured += FileManagerOnErrorOccured;
    }

    private void FileManagerOnErrorOccured(object? sender, FileSavingCompletedEventArgs e){
        if (Application.Current.MainWindow != null)
            MessageBox.Show(Application.Current.MainWindow, e.Message, e.File!.Title??="", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
    }

    private void ExecuteNewFile(){
        if (!ShowSaveFileCheck()) return;

        File = new File();
    }

    private void ExecuteOpenFile(){
        var openFileDialog = new OpenFileDialog{
            InitialDirectory = "c:\\",
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() != true) return;

        if (!ShowSaveFileCheck()) return;

        _file = _fileManager.GetFileFromPath(openFileDialog.FileName);
        _file.Title = openFileDialog.SafeFileName;
        File = _file;
    }

    private void ExecuteSaveToFile(){
        if (string.IsNullOrEmpty(_file.Path)) ExecuteSaveToFileAs();
        else{
            _fileManager.SaveFile(_file);
        }
    }

    private void ExecuteSaveToFileAs(){
        var saveFileDialog = new SaveFileDialog{
            InitialDirectory = "c:\\",
            Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (saveFileDialog.ShowDialog() != true) return;

        _file.Path = saveFileDialog.FileName;
        _file.Title = saveFileDialog.SafeFileName;

        _fileManager.SaveFile(_file);
        
        File = _file;
    }

    public bool ShowSaveFileCheck(){
        if (!_file.IsFileSaved){
            switch (MessageBox.Show(Application.Current.MainWindow!, "Do you want to save the file?", File.Title,
                        MessageBoxButton.YesNoCancel, MessageBoxImage.Question)){
                case MessageBoxResult.None:
                    return false;
                case MessageBoxResult.Yes:
                    ExecuteSaveToFile();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    return false;
                default:
                    break;
            }
        }

        return true;
    }
}