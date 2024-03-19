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

namespace Tecnoservice
{
    /// <summary>
    /// Логика взаимодействия для CreateForm_page.xaml
    /// </summary>
    public partial class CreateForm_page : Page
    {
        private Request _currentRequest = new Request();

        private int Id;
        public CreateForm_page(int userId)
        {
            InitializeComponent();

            DataContext = _currentRequest;

            ComboThing.ItemsSource = TecnoserviceEntities.GetContext().Equipment.ToList();
            ComboType.ItemsSource = TecnoserviceEntities.GetContext().Fault_type.ToList();

            Id = userId;
        }


        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Description.Text))
                {
                    Description.BorderBrush = Brushes.Red;
                    MessageBox.Show("Пожалуйста, заполните поле 'Опишите проблему'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DB db = new DB();

                var selectedEquipmentName = (ComboThing.SelectedItem as Equipment).Name;
                var selectedFaultTypeName = (ComboType.SelectedItem as Fault_type).Name;

                if (!string.IsNullOrWhiteSpace(selectedEquipmentName) && !string.IsNullOrWhiteSpace(selectedFaultTypeName))
                {
                    var selectedEquipment = db.GetContext().Equipment.FirstOrDefault(h => h.Name == selectedEquipmentName);
                    var selectedFaultType = db.GetContext().Fault_type.FirstOrDefault(t => t.Name == selectedFaultTypeName);

                    if (selectedEquipment != null && selectedFaultType != null)
                    {
                        Request request = new Request
                        {
                            ID = db.getLastID() + 1,
                            Equipment_id = selectedEquipment.ID,
                            Fault_type_id = selectedFaultType.ID,
                            Client_id = Id,
                            Executor_id = null,
                            Comments = "",
                            Description = Description.Text,
                            Date_add = DateTime.Now,
                            Date_end = null,
                            Status_id = 1
                        };

                        db.addToTable(request);
                        NavigationService.Navigate(new MainUser_page(Id));
                    }
                    else
                    {
                        MessageBox.Show("Выбранные элементы не найдены в базе данных.");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите оборудование и тип неисправности.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                MessageBox.Show($"Exception: {ex.Message}");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Description.Text))
            {
                // Выделяем поле Описания красным
                Description.BorderBrush = Brushes.Red;

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад? Введенная информация не будет сохранена.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new MainUser_page(Id));
                }
            }
            else
            {
                NavigationService.Navigate(new MainUser_page(Id));
            }
        }

    }
}
