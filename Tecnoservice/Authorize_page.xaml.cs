using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Tecnoservice
{
    /// <summary>
    /// Логика взаимодействия для Authorize_page.xaml
    /// </summary>
    public partial class Authorize_page : Page
    {

        public Authorize_page()
        {
            InitializeComponent();
        }

        public int CheckUserExists(string login)
        {
            TecnoserviceEntities db = TecnoserviceEntities.GetContext();
            User user = db.User.FirstOrDefault(_user => _user.Login.Equals(login));
            if (user != null)
            {
                return user.ID;
            }
            throw new Exception("Учетной записи с таким логином не существует");
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = loginBox.Text;
                string password = passwordBox.Password;
                int userId = CheckUserExists(login);

                TecnoserviceEntities db = TecnoserviceEntities.GetContext();
                User user = db.User.FirstOrDefault(_user => _user.Login.Equals(login));

                if (user == null)
                {
                    MessageBox.Show("Пользователя с таким логином не существует", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (user.Password != password)
                {
                    MessageBox.Show("Неверный пароль", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                switch (user.Role_id)
                {
                    case 1:
                        NavigationService.Navigate(new MainUser_page(user.Role_id));
                        break;
                    case 2:
                        NavigationService.Navigate(new MainManager_page(user.Role_id));
                        break;
                    case 3:
                        NavigationService.Navigate(new MainExecutor_page(user.Role_id));
                        break;
                }
                loginBox.Clear();
                passwordBox.Clear();

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка операции ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
