using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    internal class FavoriteViewModel : ViewModel
    {
        public User User => Manager.CurrentUser as User;

        private ObservableCollection<Favourite> _userFavorite;
        public ObservableCollection<Favourite> UserFavorite
        {
            get => _userFavorite;
            set => SetProperty(ref _userFavorite, value);
        }


        public void Refresh()
        {
            UserFavorite = new ObservableCollection<Favourite>(DataConnection.GetFavourites().Where(f=> f.User.Id == Manager.CurrentUser.Id));
        }
        public FavoriteViewModel() {
            Refresh();
            UserFavorite.CollectionChanged += (sender, e) => Refresh();
        }

    }
}
