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
    /// Логика взаимодействия для Create_form_page.xaml
    /// </summary>
    public partial class Create_form_page : Page
    {
        private Database database;
        public Create_form_page()
        {
            InitializeComponent();
            database = new Database();
            Loaded += CreateRequestPageLoaded;
        }
        private void CreateRequestPageLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                comboBoxEquipment.ItemsSource = database.GetEquipments();
                comboBoxEquipment.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }

        private void Create_request_button_Click(object sender, RoutedEventArgs e)
        {
            public void AddRequest(
            int equipmentId,
            string fio,
            string phone,
            string email,
            string serialNumber,
            string description,
            string comment
        )
            {
                // Валидация данных.
                if (fio.Length == 0 || phone.Length == 0 || email.Length == 0 || serialNumber.Length == 0 || description.Length == 0 || comment.Length == 0)
                {
                    throw new FormatException("заполните все поля данными!");
                }

                // Строка подключения к базе данных.
                SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

                // Запрос на добаление новой заявки в базу данных.
                SqlCommand cmd = new SqlCommand("INSERT INTO [Request] (EquipmentId, StatusId, Fio, Phone, Email, SerialNumber, Description, Comment, StartDate) VALUES (@EquipmentId, 1, @Fio, @Phone, @Email, @SerialNumber, @Description, @Comment, @StartDate)", connection);
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                cmd.Parameters.AddWithValue("@Fio", fio);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@SerialNumber", serialNumber);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Comment", comment);
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                connection.Open();
                cmd.ExecuteNonQuery();
            }

            public void SaveRequest(int requestId, int statusId, string description, string comment, DateTime? endDate)
            {
                // Строка подключения к базе данных.
                SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

                // Запрос на изменение заявки в базе данных.
                SqlCommand cmd = new SqlCommand("UPDATE [Request] SET StatusId = @StatusId, Description = @Description, Comment = @Comment, EndDate = @EndDate WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@StatusId", statusId);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Comment", comment);
                if (endDate == null)
                {
                    cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                }
                cmd.Parameters.AddWithValue("@Id", requestId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {

        }

        public List<Equipment> GetEquipments()
        {
            List<Equipment> equipments = new List<Equipment>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение оборудования из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Equipment]", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение оборудования.
            while (reader.Read())
            {
                Equipment equipment = new Equipment()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                equipments.Add(equipment);
            }
            return equipments;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение оборудования по его уникальному идентификатору из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Equipment] WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", equipmentId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение оборудования.
            if (reader.Read())
            {
                Equipment equipment = new Equipment()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                return equipment;
            }
            else
            {
                throw new FormatException("Данного оборудования не существует!");
            }
        }
    }
}

//public partial class CreateRequestPage : Page
//{
//    private Database database;
//    public CreateRequestPage()
//    {
//        InitializeComponent();
//        database = new Database();
//        Loaded += CreateRequestPageLoaded;
//    }

//    private void CreateRequestPageLoaded(object sender, RoutedEventArgs e)
//    {
//        try
//        {
//            comboBoxEquipment.ItemsSource = database.GetEquipments();
//            comboBoxEquipment.SelectedIndex = 0;
//        }
//        catch (Exception ex) { }
//    }

//    private void GoBack(object sender, RoutedEventArgs e)
//    {
//        NavigationService.GoBack();
//    }

//    private void AddRequest(object sender, RoutedEventArgs e)
//    {
//        try
//        {
//            Equipment equipment = comboBoxEquipment.SelectedItem as Equipment;
//            database.AddRequest(equipment.Id, textBoxFio.Text, textBoxPhone.Text, textBoxEmail.Text, textBoxSerialNumber.Text, textBoxDescription.Text, textBoxComment.Text);
//            MessageBox.Show("Заявка успешно создана!", "Создание заявки", MessageBoxButton.OK, MessageBoxImage.Information);
//            NavigationService.GoBack();
//        }
//        catch (Exception ex)
//        {
//            MessageBox.Show(ex.Message, "Ошибка создания заявки", MessageBoxButton.OK, MessageBoxImage.Error);
//        }
//    }
//}
