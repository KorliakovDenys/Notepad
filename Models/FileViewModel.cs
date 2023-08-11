namespace Notepad.Models;

public class FileViewModel : ViewModel
{
    private string _title = string.Empty;

    private string _path = string.Empty;

    private string _text = string.Empty;

    public string Title
    {
        get => _title;
        set {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Path
    {
        get => _path;
        set
        {
            _path = value;
            OnPropertyChanged();
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }
}
