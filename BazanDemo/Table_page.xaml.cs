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
    /// Логика взаимодействия для Table_page.xaml
    /// </summary>
    public partial class Table_page : Page
    {
        private Database database;

        public Table_page()
        {
            InitializeComponent();
            database = new Database();
            Loaded += RequestListPageLoaded;
        }

        private void RequestListPageLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                dataGridRequest.ItemsSource = database.GetRequests();
            }
            catch (Exception ex) { }
        }

        private void GoBack(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public List<Request> GetRequests()
        {
            List<Request> requests = new List<Request>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение заявок из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Request]", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение заявок.
            while (reader.Read())
            {
                DateTime? endDate;
                if (reader[10].ToString().Length == 0)
                {
                    endDate = null;
                }
                else
                {
                    endDate = DateTime.Parse(reader[10].ToString());
                }

                Request request = new Request()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Equipment = GetEquipmentById(int.Parse(reader[1].ToString())),
                    Status = GetStatusById(int.Parse(reader[2].ToString())),
                    Fio = reader[3].ToString(),
                    Phone = reader[4].ToString(),
                    Email = reader[5].ToString(),
                    SerialNumber = reader[6].ToString(),
                    Description = reader[7].ToString(),
                    Comment = reader[8].ToString(),
                    StartDate = DateTime.Parse(reader[9].ToString()),
                    EndDate = endDate
                };
                requests.Add(request);
            }
            return requests;
        }

        public Request GetRequestById(int requestId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение заявки по её уникальному идентификатору из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Request] WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", requestId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение заявки.
            if (reader.Read())
            {
                DateTime? endDate;
                if (reader[10].ToString().Length == 0)
                {
                    endDate = null;
                }
                else
                {
                    endDate = DateTime.Parse(reader[10].ToString());
                }

                Request request = new Request()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Equipment = GetEquipmentById(int.Parse(reader[1].ToString())),
                    Status = GetStatusById(int.Parse(reader[2].ToString())),
                    Fio = reader[3].ToString(),
                    Phone = reader[4].ToString(),
                    Email = reader[5].ToString(),
                    SerialNumber = reader[6].ToString(),
                    Description = reader[7].ToString(),
                    Comment = reader[8].ToString(),
                    StartDate = DateTime.Parse(reader[9].ToString()),
                    EndDate = endDate
                };
                return request;
            }
            else
            {
                throw new FormatException("Данной заявки не существует!");
            }
        }
    }
}
//public partial class RequestListPage : Page
//{
//    private Database database;

//    public RequestListPage()
//    {
//        InitializeComponent();
//        database = new Database();
//        Loaded += RequestListPageLoaded;
//    }

//    private void RequestListPageLoaded(object sender, System.Windows.RoutedEventArgs e)
//    {
//        try
//        {
//            dataGridRequest.ItemsSource = database.GetRequests();
//        }
//        catch (Exception ex) { }
//    }

//    private void GoBack(object sender, System.Windows.RoutedEventArgs e)
//    {
//        NavigationService.GoBack();
//    }
//}
