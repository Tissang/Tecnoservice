using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Tecnoservice
{
    public partial class EditFormManager_page : Page
    {
        private Request _currentRequest = new Request();
        private TecnoserviceEntities db = TecnoserviceEntities.GetContext();
        private int Id;

        public EditFormManager_page(int userId, int requestId)
        {
            InitializeComponent();

            _currentRequest = db.Request.FirstOrDefault(r => r.ID == requestId);

            if (_currentRequest == null)
            {
                MessageBox.Show("Не удалось загрузить заявку для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NavigationService.GoBack();
                return;
            }

            EditComboExecutor.ItemsSource = db.User.Where(_user => _user.Role.Name == "Исполнитель").ToList();
            EditComboType.ItemsSource = db.Fault_type.ToList();
            EditComboStatus.ItemsSource = db.Status.ToList();

            DatePicker1.SelectedDate = _currentRequest.Date_end;

            EditComboExecutor.SelectedItem = _currentRequest.User;
            EditComboType.SelectedItem = db.Fault_type.FirstOrDefault(_faultType => _faultType.ID == _currentRequest.Fault_type_id);
            EditComboStatus.SelectedItem = db.Status.FirstOrDefault(_Status => _Status.ID == _currentRequest.Status_id);

            
            DataContext = _currentRequest;
            EditComboStatus.SelectionChanged += EditComboStatus_SelectionChanged;
        }

        private void EditComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (EditComboStatus.SelectedItem is Status selectedStatus && selectedStatus.Name == "Выполнено")
            {
               
                DatePicker1.SelectedDate = DateTime.Now;
            }
            else
            {
               
                DatePicker1.SelectedDate = null;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад? Введенная информация не будет сохранена.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainUser_page(Id));
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var editingRequest = db.Request.FirstOrDefault(i => i.ID == _currentRequest.ID);

            if (editingRequest != null)
            {
                editingRequest.Executor_id = ((User)EditComboExecutor.SelectedItem).ID;
                editingRequest.Fault_type_id = ((Fault_type)EditComboType.SelectedItem).ID;
                editingRequest.Status_id = ((Status)EditComboStatus.SelectedItem).ID;
                editingRequest.Description = EditDescription.Text;
                editingRequest.Date_end = DatePicker1.SelectedDate;

                db.SaveChanges();
                NavigationService.Navigate(new MainManager_page(Id));
            }
        }
    }
}
