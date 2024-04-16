using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace BazanDemo
{
    /// <summary>
    /// Логика взаимодействия для Splash_page.xaml
    /// </summary>
    public partial class Splash_page : Page
    {
        private DispatcherTimer timer;
        public Splash_page()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval=TimeSpan.FromSeconds(5);
            timer.Tick += Timer_tick;
            timer.Start();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            NavigationService.Navigate(new Main_page());
        }
    }
}
