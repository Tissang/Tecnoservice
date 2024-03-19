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
using System.Windows.Threading;

namespace Tecnoservice
{
    /// <summary>
    /// Логика взаимодействия для Load_page.xaml
    /// </summary>
    public partial class Load_page : Page
    {
        private DispatcherTimer timer;
        public Load_page()
        {
            InitializeComponent();

            // Создаем таймер с задержкой в 2 секунды
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);

            // Добавляем обработчик события для таймера
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            NavigationService navService = NavigationService.GetNavigationService(this);
            NavigationService.Navigate(new Authorize_page());
        }
    }
}
