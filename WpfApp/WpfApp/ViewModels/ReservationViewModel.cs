using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class ReservationViewModel : ViewModel
    {
        public static Master SelectedMaster { get; set; }
        public Master SelectMaster { get; }
        public List<Master> MastersList => DataConnection.GetInstance().Masters.ToList();

        public List<string> UserTimeList => Enum.GetValues(typeof(UserTime))
                                          .Cast<UserTime>()
                                          .Select(Reservation.GetHour)
                                          .ToList();
        private Master _master;

        public Master Master
        {
            get { return _master; }
            set { SetProperty(ref _master, value); }
        }
        private string _selectedTime;

        public string SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (UserTimeList.Contains(value))
                {
                    SetProperty(ref _selectedTime, value);
                }
                else
                {
                    MessageBox.Show("Упс, такого времени не существует");
                }
            }
        }
        private DateTime _date;

        public DateTime UserDate
        {
            get { return _date; }
            set
            {
                if (value > DateTime.Today)
                {
                    SetProperty(ref _date, value);
                }
                else
                {
                    MessageBox.Show("Упс, даты не существует");
                }
            }
        }
        public int selectedIndex { get; set; }
        public ICommand ReservationCommand { get; set; }

        private static bool CanReservationCommandExecute(object p) => true;

        private void OnReservationCommandExecuted(object p) => SaveReservation();

        private void SaveReservation()
        {
            if (Master != null && !string.IsNullOrEmpty(SelectedTime) && UserDate != null && UserDate > DateTime.Today)
            {
                UserTime userTime = Reservation.ParseHour(SelectedTime);
                var db = DataConnection.GetInstance();
                bool isDuplicateExists = db.Reservations.Any(r =>
                r.Master != null &&
                r.Master.Id == Master.Id &&
                r.Date != null &&
                r.Date == UserDate &&
                r.Time != null &&
                r.Time == userTime

            );
                if (isDuplicateExists)
                {
                    MessageBox.Show("Извините, данный сеанс уже занят(");
                }
                else
                {
                    db.Reservations.Add(new Reservation(Manager.CurrentUser as User, Master, UserDate, Reservation.ParseHour(SelectedTime)));
                    db.SaveChanges();
                    MessageBox.Show("Вы записались!");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.");
            }

        }

        public ReservationViewModel() {
            UserDate = DateTime.Today.AddDays(1);

            if (SelectedMaster != null) {
               selectedIndex = MastersList.FindIndex(m => m.Id == SelectedMaster.Id);
            }
            ReservationCommand = new RelayCommand(OnReservationCommandExecuted, CanReservationCommandExecute);
        }

    }
}
