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
    /// Логика взаимодействия для EditFormExecutor_page.xaml
    /// </summary>
    public partial class EditFormExecutor_page : Page
    {
        private Request _currentRequest = new Request();
        private TecnoserviceEntities db = TecnoserviceEntities.GetContext();

        private int Id;
        public EditFormExecutor_page(int userId, int requestId)
        {
            InitializeComponent();

            _currentRequest = db.Request.FirstOrDefault(r => r.ID == requestId);

            if (_currentRequest == null)
            {
                MessageBox.Show("Не удалось загрузить заявку для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NavigationService.GoBack();
                return;
            }

            EditComboStatus.ItemsSource = db.Status.ToList();


            EditComboStatus.SelectedItem = db.Status.FirstOrDefault(_Status => _Status.ID == _currentRequest.Status_id);

            DataContext = _currentRequest;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад? Введенная информация не будет сохранена.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainExecutor_page(Id));
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var editingRequest = db.Request.Where(i => i.ID == _currentRequest.ID).FirstOrDefault();

            editingRequest.Status_id = ((Status)EditComboStatus.SelectedItem).ID;
            editingRequest.Comments = Comments.Text;

           
            if (editingRequest.Status_id == 3) 
            {
                editingRequest.Date_end = DateTime.Now;
            }

            db.SaveChanges();
            NavigationService.Navigate(new MainExecutor_page(Id));
        }
    }
}
