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
    internal class DeleteReservationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Reservation product)
            {
                var db = DataConnection.GetInstance();
                db.Reservations.Remove(product);
                db.SaveChanges();
                var window = PersonalWindow.Instance;
                if (window != null)
                {
                    (window.DataContext as PersonalViewModel).SearchReservation();
                }
            }
            else
                throw new NotImplementedException();
        }
    }
}
