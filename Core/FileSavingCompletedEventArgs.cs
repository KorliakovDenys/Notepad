using System;
using Notepad.Models;

namespace Notepad.Core;

public class FileSavingCompletedEventArgs : EventArgs
{
    public readonly FileViewModel? File;

    public readonly string? Message;

    public FileSavingCompletedEventArgs(FileViewModel file, string message )
    {
        File = file;
        Message = message;
    }
}
