using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using WpfApp.Services;
using WpfApp.Infrastructure.Commands;
using WpfApp.Views;


namespace WpfApp.ViewModels
{
    public class LogViewModel : ViewModel, ICloseable
    {
        public event EventHandler CloseRequest;

        private string _login = string.Empty;

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        public ICommand LoginCommand { get; set; }

        private static bool CanLoginCommandExecute(object p) => Manager.CurrentUser == null;

        private void OnLoginCommandExecuted(object p) => ProcessLogin(p);


        public LogViewModel()
        {
            LoginCommand = new RelayCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
        }

        private void ProcessLogin(object p)
        {
            string password = (p as PasswordBox).Password;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (Manager.Login(Login, password))
            {
                Window window;
                if (Manager.AccountType == Models.AccountType.User)
                    window = new StartWindow();
                else
                    window = new AdminWindow();
                Application.Current.MainWindow = window;
                CloseRequest?.Invoke(this, EventArgs.Empty);
                window.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации");
            }
        }
    }
}
