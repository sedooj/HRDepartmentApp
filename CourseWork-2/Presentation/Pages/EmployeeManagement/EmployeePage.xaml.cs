using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;
using System.Diagnostics;
using System.Linq;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage : ContentPage
    {
        private readonly Company? _company;
        private readonly Employee? _employee;
        private readonly EmployeeManagementPageViewController? _controller;

        public EmployeePage(Company company, Employee employee, EmployeeManagementPageViewController controller)
        {
            InitializeComponent();
            _company = company;
            _employee = employee;
            _controller = controller;

            Title = _employee.UserDefaultCredentials.FirstName + " " +
                    _employee.UserDefaultCredentials.LastName + " – " + _employee.Position;
            BindingContext = this;

            try
            {
                Debug.WriteLine("Initializing EmployeePage...");
                EmploymentHistoryCollectionView.ItemsSource = _employee.EmploymentHistoryRecords
                    .Select((record, index) => new
                    {
                        Index = index + 1,
                        record.PositionAtWork,
                        WorkingPeriod = $"{record.WorkingStartDate:yyyy-MM-dd} - {(record.WorkingEndDate.HasValue ? record.WorkingEndDate.Value.ToString("yyyy-MM-dd") : " ...")}"
                    }).ToList();
                Debug.WriteLine("EmployeePage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing EmployeePage: {ex}");
            }
        }

        public string EmploymentHistorySummary => $"Number of Records: {_employee?.EmploymentHistoryRecords.Count ?? 0}";

        private async void OnRewardClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RewardPage(_employee, _controller));
                Debug.WriteLine("OnRewardClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnRewardClicked: {ex}");
            }
        }

        private void OnPunishClicked(object sender, EventArgs e)
        {
            try
            {
                // Handle punish logic here
                Debug.WriteLine("OnPunishClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnPunishClicked: {ex}");
            }
        }

        private async void OnFireClicked(object sender, EventArgs e)
        {
            try
            {
                string fireReason = await DisplayPromptAsync("Уволить сотрудника", "Пожалуйста, укажите причину увольнения сотрудника:");
                if (!string.IsNullOrEmpty(fireReason))
                {
                    _controller.FireEmployee(_company, _employee, fireReason);
                    await Navigation.PopAsync();
                    _controller.LoadData();

                    Debug.WriteLine("OnFireClicked executed successfully.");
                }
                else
                {
                    Debug.WriteLine("Fire reason was not provided.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnFireClicked: {ex}");
            }
        }

        private async void OnViewDetailsClicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                var record = button?.CommandParameter as EmploymentHistoryRecord;
                if (record != null)
                {
                    // await Navigation.PushAsync(new EmploymentHistoryDetailPage(record));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnViewDetailsClicked: {ex}");
            }
        }
    }
}