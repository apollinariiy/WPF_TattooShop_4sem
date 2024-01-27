using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp.Infrastructure.Commands
{
    class DeleteUserCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Admin admin) {
                MessageBox.Show("Вы не можете удалить администратора");
            }
            if (parameter is User user)
            {
                if (MessageBox.Show("Вы действительно хотите заблокировать пользователя?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataConnection.GetInstance().Accounts.Remove(user);
                    DataConnection.GetInstance().SaveChanges();
                    var window = AdminWindow.Instance;
                    if (window != null)
                    {
                        (window.DataContext as AdminViewModel).SearchUser();
                    }
                }
            }
            else
                throw new InvalidOperationException();
        }
    }
}
