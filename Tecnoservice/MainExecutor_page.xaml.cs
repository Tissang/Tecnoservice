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
using System.Windows.Shapes;

namespace Tecnoservice
{
    /// <summary>
    /// Логика взаимодействия для MainExecutor_page.xaml
    /// </summary>
    public partial class MainExecutor_page : Page
    {

        private int Id;
        public MainExecutor_page(int userId)
        {
            InitializeComponent();
            DGridMainExecutor_page.ItemsSource = TecnoserviceEntities.GetContext().Request.ToList();
            Id = userId;
        }
        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchText = searchTextBox.Text.ToLower();
                bool hasMatches = false;

                // Сначала сбросим все стили перед новым поиском
                foreach (var item in DGridMainExecutor_page.Items)
                {
                    DataGridRow row = (DataGridRow)DGridMainExecutor_page.ItemContainerGenerator.ContainerFromItem(item);

                    if (row != null)
                    {
                        foreach (DataGridColumn column in DGridMainExecutor_page.Columns)
                        {
                            if (column is DataGridTextColumn)
                            {
                                var cellContent = column.GetCellContent(row);

                                if (cellContent != null)
                                {
                                    // Удалить стили из ячейки
                                    ((TextBlock)cellContent).ClearValue(TextBlock.BackgroundProperty);

                                    string cellText = ((TextBlock)cellContent).Text.ToLower();

                                    if (cellText.Contains(searchText))
                                    {
                                        // Подсветить текст совпадения (изменить цвет текста)
                                        ((TextBlock)cellContent).Background = Brushes.Yellow;
                                        hasMatches = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (!hasMatches)
                {
                    MessageBox.Show("Совпадений не найдено.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TecnoserviceEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridMainExecutor_page.ItemsSource = TecnoserviceEntities.GetContext().Request.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранной заявки
            Request selectedRequest = DGridMainExecutor_page.SelectedItem as Request;

            if (selectedRequest != null)
            {
                // Переход на экран редактирования с передачей Id заявки
                NavigationService.Navigate(new EditFormExecutor_page(Id, selectedRequest.ID));
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
