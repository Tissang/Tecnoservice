using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    public partial class Statistic_page : Page
    {
        private int Id;

        public Statistic_page()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                using (var context = new TecnoserviceEntities())
                {
                    int completedRequestsCount = context.Request.Count(req => req.Status_id == 3);
                    labelCompletedRequestsCount.Content = $"Количество выполненных заявок: {completedRequestsCount}";

                    double averageCompletionTime = CalculateAverageCompletionTime(context.Request);
                    labelAverageExecutionTime.Content = $"Среднее время выполнения: {averageCompletionTime} часов";

                    CalculateTopIssueTypes(context.Request);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке статистики: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateAverageCompletionTime(IQueryable<Request> requestList)
        {
            var completedRequests = requestList
                .Where(req => req.Status_id == 3 && req.Date_add != null && req.Date_end != null)
                .ToList();

            if (completedRequests.Any())
            {
                double totalMinutes = completedRequests
                    .Sum(req => (req.Date_end - req.Date_add).Value.TotalMinutes);
                double averageMinutes = totalMinutes / completedRequests.Count;
                return Math.Round(averageMinutes / 60.0, 2); 
            }
            else
            {
                return 0;
            }
        }

        private void CalculateTopIssueTypes(IQueryable<Request> requestList)
        {
            var issueTypeCounts = requestList.GroupBy(req => req.Fault_type_id)
                .Select(group => new { IssueTypeId = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            if (issueTypeCounts != null)
            {
                var topIssueType = requestList.FirstOrDefault(req => req.Fault_type_id == issueTypeCounts.IssueTypeId)?.Fault_type;
                if (topIssueType != null)
                {
                    labelTop.Content = $"Самая частая неисправность: {topIssueType.Name} ({issueTypeCounts.Count} шт.)";
                }
                else
                {
                    labelTop.Content = "Ошибка!";
                }
            }
            else
            {
                labelTop.Content = "Нет выполненных заявок с неисправностями";
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainManager_page(Id));
        }
    }
}
