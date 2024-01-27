using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для SketchWindow.xaml
    /// </summary>
    public partial class SketchWindow : Window
    {
        public static SketchWindow Instance { get; private set; }
        public SketchWindow()
        {
            InitializeComponent();
            (DataContext as SketchViewModel).CloseRequest += (sender, e) => Close();
            Instance = this;    
        }

        private void Personal_Window_Click(object sender, RoutedEventArgs e)
        {
            PersonalWindow window = new PersonalWindow();
            window.Show();
        }

        private void Favorite_Window_Click(object sender, RoutedEventArgs e)
        {
            FavoriteWindow window = new FavoriteWindow();
            window.Show();
        }

        private void Start_Window_Click(object sender, RoutedEventArgs e)
        {
            StartWindow window = new StartWindow();
            window.Show();
        }

        private void Master_Window_Click(object sender, RoutedEventArgs e)
        {
            MastersWindow window = new MastersWindow();
            window.Show();
        }
    }
}
