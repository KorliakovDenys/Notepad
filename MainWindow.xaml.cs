using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Notepad.Core;
using Notepad.Models;

namespace Notepad{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        private readonly MainViewModel _mainViewModel = new();

        public MainWindow(){
            InitializeComponent();
            this.DataContext = _mainViewModel;
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e){
            if (!_mainViewModel.ShowSaveFileCheck()) e.Cancel = true;
        }

        private void MainTexBox_OnSelectionChanged(object sender, RoutedEventArgs e){
            if (sender is not TextBox textBox) return;

            var selectionStart = textBox.SelectionStart;
            var lineIndex = textBox.GetLineIndexFromCharacterIndex(selectionStart);

            _mainViewModel.Start = lineIndex;
            _mainViewModel.Length = textBox.CaretIndex - textBox.GetCharacterIndexFromLineIndex(lineIndex);
        }
    }
}