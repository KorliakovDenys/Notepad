using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepad.Models
{
    class MainViewModel : ViewModel
    {
        private string _input;
        public string Input { get { return _input; } set {  _input = value; OnPropertyChanged(); } }
    }
}
