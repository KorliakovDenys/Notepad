using System;
using System.IO;
using Notepad.Models;
namespace Notepad.Core;

public class FileManager {
        private const string DefaultFileName = "New text file";

        private const string DefaultFileExtension = ".txt";

    private static readonly string ApplicationPath = Environment.CurrentDirectory;

    public event EventHandler<FileSavingCompletedEventArgs>? ErrorOccured;

    private static bool IsFileExist(string filePath)
    {
        return File.Exists(filePath);
    }

    public FileViewModel GetFileFromPath (string filePath)
    {
        if (IsFileExist(filePath))
        {
            return new FileViewModel { Text = File.ReadAllText(filePath), Path = filePath };
        }

        var newFile = new FileViewModel();
        OnError(newFile, "File does not exist.");
        return newFile;
    }
        public bool SaveFile(FileViewModel fileViewModel){
            try{
                if (string.IsNullOrEmpty(fileViewModel.Path)){
                var newFileName = DefaultFileName; 

                for (var i = 2; i < short.MaxValue; i++){
                    if (!IsFileExist(newFileName)) break;
                    newFileName = DefaultFileName + $"({i})";
                }

                var title = newFileName + DefaultFileExtension;

                fileViewModel.Title = title;
                fileViewModel.Path = ApplicationPath + title;
                }
            File.WriteAllText(fileViewModel.Path, fileViewModel.Text);
            return true;
            }
            catch (Exception exception) 
            {
                 OnError(fileViewModel, exception.Message);
            }
            return false;
        }

       private void OnError(FileViewModel file, string message)
    {
        ErrorOccured?.Invoke(this, new FileSavingCompletedEventArgs(file, message))
    }
        

       
    }

