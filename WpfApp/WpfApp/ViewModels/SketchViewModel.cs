using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Views;
using WpfApp.Infrastructure.Commands;
using System.Reflection.Metadata;
using System.Data.Common;

namespace WpfApp.ViewModels
{
    public class SketchViewModel : ViewModel, ICloseable
    {
        
        public List<TatooTypes> TatooTypesList => Enum.GetValues(typeof(TatooTypes))
 .Cast<TatooTypes>().ToList();
        private TatooTypes _type;

        public TatooTypes Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value);
            }
        }
/*        public ICommand SelectTatooTypeCommand { get; set; }

        private static bool CanSelectTatooTypeCommandExecute(object p) => true;

        private void OnSelectTatooTypeCommandExecuted(object p) => SortResult();

        private void SortResult()
        {
            MessageBox.Show("aaaaaaaaaaaaaaaa");
        }*/

        private List<Sketch> _searchResult;

        public event EventHandler CloseRequest;

        public List<Sketch> SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }
        public void SearchSketch() => SearchResult = DataConnection.GetSketches();
        public ICommand MasterCommand { get; set; }

        private static bool CanMasterCommandExecute(object p) => true;

        private void OnMasterCommandExecuted(object p) => OpenMasterWindow();

        private void OpenMasterWindow()
        {
            Window window;
            window = new MastersWindow();
            Application.Current.MainWindow = window;
            CloseRequest?.Invoke(this, EventArgs.Empty);
            window.Show();
        }
        public ICommand StartCommand { get; set; }

        private static bool CanStartCommandExecute(object p) => true;

        private void OnStartCommandExecuted(object p) => OpenStartWindow();

        private void OpenStartWindow()
        {
            Window window;
            window = new StartWindow();
            Application.Current.MainWindow = window;
            CloseRequest?.Invoke(this, EventArgs.Empty);
            window.Show();
        }
        private List<Sketch> originalSketchDataGrid;
        public ICommand SearchChangeCommand { get; set; }

        private static bool CanSearchChangeCommandExecute(object p) => true;

        private void OnSearchChangeCommandExecuted(object p) => Sort(p);

        private void Sort(object p) {
            if (p is TatooTypes product)
            {
                TatooTypes Type = product;
                if (originalSketchDataGrid == null)
                {
                    originalSketchDataGrid = SearchResult.ToList();
                }
                if (Type == TatooTypes.None)
                {
                    SearchResult = originalSketchDataGrid;
                }
                else {
                    SearchResult = originalSketchDataGrid.Where(sketch => sketch.Type == Type).ToList();
                }
               
            }
        }
        public SketchViewModel() {
            SearchSketch();
            StartCommand = new RelayCommand(OnStartCommandExecuted, CanStartCommandExecute);
            MasterCommand = new RelayCommand(OnMasterCommandExecuted, CanMasterCommandExecute);
            SearchChangeCommand = new RelayCommand(OnSearchChangeCommandExecuted, CanSearchChangeCommandExecute);

        }
    }
}
