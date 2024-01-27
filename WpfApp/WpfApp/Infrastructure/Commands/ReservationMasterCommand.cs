using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp.Infrastructure.Commands
{
    class ReservMasterCommand : Command
    {
        public override bool CanExecute(object parameter) => Manager.AccountType == AccountType.User;

        public override void Execute(object parameter)
        {
            if (parameter is Master product)
            {
                ReservationViewModel.SelectedMaster = product;
                ReservationWindow window = new ReservationWindow();
                window.Show();
            }
            else
                throw new InvalidOperationException();
        }
    }
}
