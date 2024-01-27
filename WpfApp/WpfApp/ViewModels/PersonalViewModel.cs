using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Views;

namespace WpfApp.ViewModels
{
    public class PersonalViewModel : ViewModel
    {
        public User User => Manager.CurrentUser as User;

        private ObservableCollection<Reservation> _userReservation;
        public ObservableCollection<Reservation> UserReservation
        {
            get => _userReservation;
            set => SetProperty(ref _userReservation, value);
        }

        public void SearchReservation()
        {
            var filteredReservations = new ObservableCollection<Reservation>(
                DataConnection.GetReservations().Where(res => res.User != null && res.User.Id == Manager.CurrentUser.Id)
            );
            UserReservation = filteredReservations;
        }



        public PersonalViewModel() {
            SearchReservation();
            UserReservation.CollectionChanged += (sender, e) => SearchReservation();
        }
      
    }
}
