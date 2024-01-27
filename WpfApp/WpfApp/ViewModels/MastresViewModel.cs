using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp.Infrastructure.Commands;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Views;

namespace WpfApp.ViewModels
{
public class MastersViewModel : ViewModel, ICloseable
    {

        public User User => Manager.CurrentUser as User;

        private List<Feedback> _userFeedback;
        public List<Feedback> UserFeedback
        {
            get => _userFeedback;
            set => SetProperty(ref _userFeedback, value);
        }
       
        private Master _selectedMaster;

        public Master SelectedMaster
        {
            get { return _selectedMaster; }
            set { SetProperty(ref _selectedMaster, value); }
        }
        public void SearchFeedback()
        {
            if (SelectedMaster != null)
                UserFeedback = DataConnection.GetFeedbacks().Where(res => res.Master.Id == SelectedMaster.Id).ToList();
        }
        private List<Master> masterDataGrid;

        public event EventHandler CloseRequest;

        public List<Master> MasterDataGrid
        {
            get => masterDataGrid;
            set => SetProperty(ref masterDataGrid, value);

        }

        private void SearchMaster() => MasterDataGrid = DataConnection.GetMasters();
        public ICommand SketchCommand { get; set; }

        private static bool CanSketchCommandExecute(object p) => true;

        private void OnSketchCommandExecuted(object p) => OpenSketchWindow();
        private void OpenSketchWindow()
        {
            Window window;
            window = new SketchWindow();
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
        public ICommand OpenFeedbackCommand { get; set; }

        private static bool CanOpenFeedbackCommandExecute(object p) => true;

        public void OnOpenFeedbackCommandExecuted(object p) => SearchFeedback();
        private int _userRating = 5;

        public int UserRating
        {
            get { return _userRating; }
            set { SetProperty(ref _userRating, value); }
        }

        private string _userComment = string.Empty;

        public string UserComment
        {
            get { return _userComment; }
            set { SetProperty(ref _userComment, value); }
        }

        public ICommand ReviewCommand { get; set; }

        private void OnReviewCommandExecuted(object p) => Review();

        private void Review()
        {
            var db = DataConnection.GetInstance();
            db.Feedbacks.Add(new Feedback(Manager.CurrentUser as User, SelectedMaster, UserComment));
            db.SaveChanges();
            UserComment = string.Empty;
            SearchFeedback();
        }

        private static bool CanReviewCommandExecute(object p) => true;

        private string _searchText = string.Empty;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public ICommand SearchCommand { get; set; }

        private static bool CanSearchCommandExecute(object p) => true;

        private void OnSearchCommandExecuted(object p) => Search();
        private void Search() {
            MasterDataGrid = DataConnection.SearchMasters(SearchText);
        }



        public MastersViewModel() {
            
            SearchMaster();
            SketchCommand = new RelayCommand(OnSketchCommandExecuted, CanSketchCommandExecute);
            StartCommand = new RelayCommand(OnStartCommandExecuted, CanStartCommandExecute);
            ReviewCommand = new RelayCommand(OnReviewCommandExecuted, CanReviewCommandExecute);
            OpenFeedbackCommand = new RelayCommand(OnOpenFeedbackCommandExecuted, CanOpenFeedbackCommandExecute);
            SearchCommand = new RelayCommand(OnSearchCommandExecuted, CanSearchCommandExecute);


        }


    }
}
