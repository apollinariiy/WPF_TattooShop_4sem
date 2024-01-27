using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Views
{
    internal interface ICloseable
    {
        event EventHandler CloseRequest;
    }
}
