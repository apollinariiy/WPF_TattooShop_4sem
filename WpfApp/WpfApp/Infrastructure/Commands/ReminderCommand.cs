using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Infrastructure.Commands
{
    public class ReminderCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Reservation reservation)
            {
                EmailMessage.SendMail("PL TATOO", reservation.User.EMail, reservation.User.FullName, "Напоминание",
                    $"Ваш сеанс {reservation.Date.ToString("dd MMMM yyyy")} {Reservation.GetHour(reservation.Time)}.\nНе забудьте нас посетить!<3");
            }
            else
                throw new InvalidOperationException();
        }
    }
}
