﻿using Notepad.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Notepad.Models{
    public abstract class ViewModel : INotifyPropertyChanged{
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}