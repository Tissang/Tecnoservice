using BazanDemo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Login_page.xaml
    /// </summary>
    public partial class Login_page : Page
    {
        public Login_page()
        {
            InitializeComponent();
        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {
           
            string stroka_conn = "Data Source=\"10.118.95.29, 1433\";Initial Catalog=User_02_02;User ID=User_02_02;Password=#3e2w1q";
            SqlConnection conn = new SqlConnection(stroka_conn);
            string str_query = "SELECT * FROM [User]";
            SqlCommand sqlCommand = new SqlCommand(str_query, conn);
            sqlCommand.Connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                if (reader[2].ToString() == Login_box.Text && reader[3].ToString() == Password_box.Text) 
                { 
                    if (int.Parse(reader[1].ToString()) == 1)
                    {
                        NavigationService.Navigate(new Main_page());
                    }
                    else
                    {
                        NavigationService.Navigate(new Vipoln_page());
                    }
                }
            }

        }
    }
}
//public partial class SignInPage : Page
//{
//    private Database database;
//    public SignInPage()
//    {
//        InitializeComponent();
//        database = new Database();
//    }

//    private void SignIn(object sender, RoutedEventArgs e)
//    {
//        try
//        {
//            int roleId = database.SignIn(textBoxLogin.Text, passwordBoxPassword.Password);
//            if (roleId == 2)
//            {
//                Contractor contractor = database.GetContractorByLogin(textBoxLogin.Text);
//                NavigationService.Navigate(new HandleJobPage(contractor.Id));
//            }
//            else
//            {
//                NavigationService.Navigate(new ControlPage(roleId));
//            }
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message, "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
//        }
//    }
//}