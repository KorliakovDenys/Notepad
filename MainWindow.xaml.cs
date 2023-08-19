using System.ComponentModel;
using System.Windows;
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
    }
}