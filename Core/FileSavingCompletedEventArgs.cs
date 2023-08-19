using System;
using Notepad.Models;

namespace Notepad.Core;

public class FileSavingCompletedEventArgs : EventArgs{
    public readonly File? File;

    public readonly string? Message;

    public FileSavingCompletedEventArgs(File file, string message){
        File = file;
        Message = message;
    }
}