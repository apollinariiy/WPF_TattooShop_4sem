using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Views;

namespace WpfApp.ViewModels
{
    public class StartViewModel : ViewModel, ICloseable
    {
        public event EventHandler CloseRequest;
        public StartViewModel()
        {
            SketchCommand = new RelayCommand(OnSketchCommandExecuted, CanSketchCommandExecute);
            MasterCommand = new RelayCommand(OnMasterCommandExecuted, CanMasterCommandExecute);
        }

        public ICommand SketchCommand { get; set; }

        private static bool CanSketchCommandExecute(object p) => true;

        private void OnSketchCommandExecuted(object p) => OpenSketchWindow();
        public ICommand MasterCommand { get; set; }

        private static bool CanMasterCommandExecute(object p) => true;

        private void OnMasterCommandExecuted(object p) => OpenMasterWindow();
        private void OpenSketchWindow()
        {
            Window window;
            window = new SketchWindow();
            Application.Current.MainWindow = window;
            CloseRequest?.Invoke(this, EventArgs.Empty);
            window.Show();
        }
        private void OpenMasterWindow()
        {
            Window window;
            window = new MastersWindow();
            Application.Current.MainWindow = window;
            CloseRequest?.Invoke(this, EventArgs.Empty);
            window.Show();
        }

    }
}
