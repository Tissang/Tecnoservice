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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BazanDemo
{
    /// <summary>
    /// Логика взаимодействия для Main_page.xaml
    /// </summary>
    public partial class Main_page : Page
    {
        public Main_page()
        {
            InitializeComponent();
        }

        private void Create_order_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Create_form_page());
        }

        private void Work_order_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Work_page());
        }

        private void Reports_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Table_page());
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_page());
        }
    }
}
