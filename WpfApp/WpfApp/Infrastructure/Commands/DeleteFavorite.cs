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
    internal class DeleteFavoriteCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Favourite product)
            {
                var db = DataConnection.GetInstance();
                var currentUser = Manager.CurrentUser as User;

                    db.Favourites.Remove(product);
                db.SaveChanges();
        
                var window = FavoriteWindow.Instance;
                if (window != null)
                {
                    (window.DataContext as FavoriteViewModel).Refresh();
                }
                var window2 = SketchWindow.Instance;
                if (window2 != null)
                {
                    (window2.DataContext as SketchViewModel).SearchSketch();
                }
            }
            else
                throw new NotImplementedException();
        }
    }
}
