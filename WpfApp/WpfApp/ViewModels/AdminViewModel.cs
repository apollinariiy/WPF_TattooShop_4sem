using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Views;
using WpfApp.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using WpfApp.Infrastructure.Commands;
using WpfApp.Infrastructure;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace WpfApp.ViewModels
{
    public class AdminViewModel : ViewModel
    {

        #region User
        private string _query = string.Empty;

        public string Query
        {
            get { return _query; }
            set { SetProperty(ref _query, value); }
        }
        private List<User> userDataGrid;

        public List<User> UserDataGrid
        {
            get => userDataGrid;
            set => SetProperty(ref userDataGrid, value);
        }
        public ICommand SearchCommand { get; set; }

        private void OnSearchCommandExecuted(object p) => SearchUser();

        private static bool CanSearchCommandExecute(object p) => true;
        public void SearchUser() => UserDataGrid = DataConnection.SearchUsers(Query);
        #endregion User
        #region Sketch
        private List<Sketch> sketchDataGrid;

        public List<Sketch> SketchDataGrid
        {
            get => sketchDataGrid;
            set => SetProperty(ref sketchDataGrid, value);
        }
        public void SearchSketch() => SketchDataGrid = DataConnection.GetSketches();

        public List<TatooTypes> TatooTypesList => Enum.GetValues(typeof(TatooTypes))
            .Cast<TatooTypes>().Where(type => type != TatooTypes.None).ToList();

        /// <summary>
        /// ////
        /// </summary>
    	public int Id { get; }
        private byte[] _image;

        public byte[] Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private TatooTypes _type;

        public TatooTypes Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        public ICommand ImageCommand { get; set; }

        private static bool CanLoadImgCommandExecute(object p) => true;

        private void OnLoadImgCommandExecuted(object p) => LoadImg();
        public ICommand SaveCommand { get; set; }

        private static bool CanSaveCommandExecute(object p) => true;

        private void OnSaveCommandExecuted(object p) => Save();
        private void Save()
        {
            var db = DataConnection.GetInstance();
            if (Image != null && Image.Length > 0 && Type != TatooTypes.None)
            {
                db.Sketch.Add(new Sketch(Image, Type));
                db.SaveChanges();
                SearchSketch();
                MessageBox.Show("Сохранено");
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
            }
        }
        private void LoadImg()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Картинки| *.jpg;*.jpeg;*.png;*.bmp"
            };

            if (dialog.ShowDialog() == true)
            {
                var (status, result) = ImageProvider.ImageToByte(dialog.FileName);
                if (status)
                    Image = result;
                else
                    MessageBox.Show("Не удалось загрузить изображение");
            }
        }
        #endregion Sketch
        #region Master
        private List<Master> masterDataGrid;

        public List<Master> MasterDataGrid
        {
            get => masterDataGrid;
            set => SetProperty(ref masterDataGrid, value);
        }

        public void SearchMaster() => MasterDataGrid = DataConnection.GetMasters();

        public int Id_Master { get; }
        private string _surname = string.Empty;

        public string Surname
        {
            get { return _surname; }
            set { SetProperty(ref _surname, value); }
        }
        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _middlename = string.Empty;

        public string MiddleName
        {
            get { return _middlename; }
            set { SetProperty(ref _middlename, value); }
        }

        private string _experience = string.Empty;

        public string Experience
        {
            get { return _experience; }
            set { SetProperty(ref _experience, value); }
        }

        private byte[] _imageMaster;

        public byte[] ImageMaster
        {
            get { return _imageMaster; }
            set { SetProperty(ref _imageMaster, value); }
        }

        private TatooTypes _typeMaster;

        public TatooTypes TypeMaster
        {
            get { return _typeMaster; }
            set { SetProperty(ref _typeMaster, value); }
        }
        public ICommand ImageMasterCommand { get; set; }

        private static bool CanLoadImgMasterCommandExecute(object p) => true;

        private void OnLoadImgMasterCommandExecuted(object p) => LoadImgMaster();
        private void LoadImgMaster()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Картинки| *.jpg;*.jpeg;*.png;*.bmp"
            };

            if (dialog.ShowDialog() == true)
            {
                var (status, result) = ImageProvider.ImageToByte(dialog.FileName);
                if (status)
                    ImageMaster = result;
                else
                    MessageBox.Show("Не удалось загрузить изображение");
            }
        }
        public ICommand SaveMasterCommand { get; set; }

        private static bool CanSaveMasterCommandExecute(object p) => true;

        private void OnSaveMasterCommandExecuted(object p) => SaveMaster();
        private void SaveMaster()
        {
            var db = DataConnection.GetInstance();

            if (!string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(MiddleName) && TypeMaster != TatooTypes.None && !string.IsNullOrEmpty(Experience) && ImageMaster != null && ImageMaster.Length > 0)
            {
                db.Masters.Add(new Master(Id_Master, ImageMaster, Surname, Name, MiddleName, TypeMaster, Convert.ToInt32(Experience)));
                db.SaveChanges();
                SearchSketch();
                MessageBox.Show("Сохранено");
                SearchMaster();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
            }
         
        }

        #endregion Master
        #region Reservation
        private List<Reservation> reservationDataGrid;

        public List<Reservation> ReservationDataGrid
        {
                get => reservationDataGrid;
                set => SetProperty(ref reservationDataGrid, value);
        }

        public void SearchReservation() => ReservationDataGrid = DataConnection.GetReservations();
        #endregion Reservation
        public List<TatooTypes> TatooTypesFilterList => Enum.GetValues(typeof(TatooTypes))
         .Cast<TatooTypes>().ToList();
        private TatooTypes _filterType;

        public TatooTypes FilterType
        {
            get { return _filterType; }
            set { SetProperty(ref _filterType, value); }
        }
        private List<Sketch> originalSketchDataGrid;
        public ICommand SearchChangeCommand { get; set; }

        private void OnSearchChangeCommandExecuted(object p) => SearchChange();

        private static bool CanSearchChangeCommandExecute(object p) => true;
        public void SearchChange() {
            if (originalSketchDataGrid == null)
            {
                originalSketchDataGrid = SketchDataGrid.ToList();
            }
            if (FilterType == TatooTypes.None)
            {
                SketchDataGrid = originalSketchDataGrid;
            }
            else
            {
                SketchDataGrid = originalSketchDataGrid.Where(sketch => sketch.Type == FilterType).ToList();
            }
        }


        public AdminViewModel()
        {
            SearchUser();
            SearchSketch();
            SearchMaster();
            SearchReservation();
            Image = ImageProvider.GetDefault();
            ImageMaster = ImageProvider.GetDefault();
            ImageCommand = new RelayCommand(OnLoadImgCommandExecuted, CanLoadImgCommandExecute);
            ImageMasterCommand = new RelayCommand(OnLoadImgMasterCommandExecuted, CanLoadImgMasterCommandExecute);
            SaveCommand = new RelayCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            SaveMasterCommand = new RelayCommand(OnSaveMasterCommandExecuted, CanSaveMasterCommandExecute);
            SearchCommand = new RelayCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
            SearchChangeCommand = new RelayCommand(OnSearchChangeCommandExecuted, CanSearchChangeCommandExecute);

        }
    }
}
