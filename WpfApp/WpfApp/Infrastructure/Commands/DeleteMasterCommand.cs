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
    class DeleteMasterCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Master master)
            {
                DataConnection.GetInstance().Masters.Remove(master);
                DataConnection.GetInstance().SaveChanges();
                var window = AdminWindow.Instance;
                if (window != null)
                {
                    (window.DataContext as AdminViewModel).SearchMaster();
                }

            }
            else
                throw new InvalidOperationException();
        }
    }
}
