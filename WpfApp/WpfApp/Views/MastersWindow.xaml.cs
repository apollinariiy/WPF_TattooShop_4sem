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
using WpfApp.Models;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MastersWindow.xaml
    /// </summary>
    public partial class MastersWindow : Window
    {
        public MastersWindow()
        {
            InitializeComponent();
            (DataContext as MastersViewModel).CloseRequest += (sender, e) => Close();

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
           
                // Устанавливаем свойство Visibility кнопки в Visible
                MyButton.Visibility = Visibility.Visible;
            NewReview_StackPanel.Visibility = Visibility.Visible;
            CheckReview_StackPanel.Visibility = Visibility.Visible;
            (DataContext as MastersViewModel).SearchFeedback();
        }
 

        private void ListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Start_Window_Click(object sender, RoutedEventArgs e)
        {
            StartWindow window = new StartWindow();
            window.Show();
        }

        private void Sketch_Window_Click(object sender, RoutedEventArgs e)
        {
            SketchWindow window = new SketchWindow();
            window.Show();
        }
        private void Reservation_Window_Click(object sender, RoutedEventArgs e)
        {
            ReservationWindow window = new ReservationWindow();
            window.Show();
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

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            NewReview_StackPanel.Visibility=Visibility.Hidden;
            CheckReview_StackPanel.Visibility = Visibility.Visible;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            NewReview_StackPanel.Visibility = Visibility.Visible;
            CheckReview_StackPanel.Visibility = Visibility.Hidden;
        }
    }
}
