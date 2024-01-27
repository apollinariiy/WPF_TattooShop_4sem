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
    public class CancelReservationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Reservation reservation)
            {
                EmailMessage.SendMail("PL TATOO", reservation.User.EMail, reservation.User.FullName, "Ваша запись отменена",
                    $"Извините, ваш сеанс {reservation.Date.ToString("dd MMMM yyyy")} {Reservation.GetHour(reservation.Time)} по техническим причинам отменен.\nВы можете записаться на другой сеанс.");
                var db = DataConnection.GetInstance();
                db.Reservations.Remove(reservation);
                db.SaveChanges();
                var window = AdminWindow.Instance;
                if (window != null)
                {
                    (window.DataContext as AdminViewModel).SearchReservation();
                }
            }
            else
                throw new InvalidOperationException();
        }
    }
}
