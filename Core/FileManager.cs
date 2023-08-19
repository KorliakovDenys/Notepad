using System;
using System.IO;
using Notepad.Models;
using File = Notepad.Core.File;

namespace Notepad.Core;

public class FileManager{
    private const string DefaultFileName = "New text file";

    private const string DefaultFileExtension = ".txt";

    private static readonly string ApplicationPath = Environment.CurrentDirectory;

    public event EventHandler<FileSavingCompletedEventArgs>? ErrorOccured;

    private static bool IsFileExist(string filePath){
        return System.IO.File.Exists(filePath);
    }

    public File GetFileFromPath(string filePath){
        if (IsFileExist(filePath)){
            return new File{ Text = System.IO.File.ReadAllText(filePath), Path = filePath, IsFileSaved = true};
        }

        var newFile = new File();
        OnError(newFile, "File does not exist.");

        return newFile;
    }

    public bool SaveFile(File file){
        try{
            if (string.IsNullOrEmpty(file.Path)){
                var newFileName = DefaultFileName;

                for (var i = 2; i < short.MaxValue; i++){
                    if (!IsFileExist(newFileName)) break;
                    newFileName = DefaultFileName + $"({i})";
                }

                var title = newFileName + DefaultFileExtension;

                file.Title = title;
                file.Path = ApplicationPath + title;
            }

            System.IO.File.WriteAllText(file.Path, file.Text);

            file.IsFileSaved = true;
        }
        catch (Exception exception){
            OnError(file, exception.Message);
        }

        return file.IsFileSaved;
    }

    private void OnError(File file, string message){
        ErrorOccured?.Invoke(this, new FileSavingCompletedEventArgs(file, message));
    }
}