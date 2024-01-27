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
    class DeleteSketchCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (parameter is Sketch sketch)
            {
                    DataConnection.GetInstance().Sketch.Remove(sketch);
                    DataConnection.GetInstance().SaveChanges();
                    var window = AdminWindow.Instance;
                    if (window != null)
                    {
                        (window.DataContext as AdminViewModel).SearchSketch();
                    }
       
            }
            else
                throw new InvalidOperationException();
        }
    }
}
