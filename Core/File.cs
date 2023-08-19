namespace Notepad.Core;

public class File{
    private string _text = string.Empty;

    public string Title{ get; set; } = string.Empty;

    public string Path{ get; set; } = string.Empty;

    public string Text{
        get => _text;
        set{
            _text = value;
            IsFileSaved = false;
        }
    }

    public bool IsFileSaved{ get; set; } = true;
}