using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;
using System.Diagnostics;
using System.Linq;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage
    {
        private Company _company;
        private Human _employee;
        private readonly EmployeeManagementPageViewController _controller;

        public EmployeePage(Company company, Human employee, EmployeeManagementPageViewController controller)
        {
            _company = company;
            _employee = employee;
            _controller = controller;
            InitializeComponent();

            Title = _employee.UserDefaultCredentials.FirstName + " " +
                    _employee.UserDefaultCredentials.LastName + " – " + _employee.EmploymentHistoryRecords.Last().PositionAtWork;
            BindingContext = this;

            
            try
            {
                Debug.WriteLine("Initializing EmployeePage...");
                EmploymentHistoryCollectionView.ItemsSource = _employee.EmploymentHistoryRecords
                    .Select((record, index) => new
                    {
                        Index = index + 1,
                        record.PositionAtWork,
                        WorkingPeriod = $"{record.WorkingStartDate:dd-MM-yyyy} - {(record.WorkingEndDate.HasValue ? record.WorkingEndDate.Value.ToString("dd.MM.yyyy") : " Нынешнее время")}"
                    }).ToList();
                Debug.WriteLine("EmployeePage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing EmployeePage: {ex}");
            }
        }
        
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

        private async void OnPunishClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new PunishmentPage(_employee, _controller));
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
                    _controller.FireEmployee(_company, _employee.UUID, fireReason);
                    await Navigation.PopAsync();

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
                if (button?.CommandParameter is int index)
                {
                    var record = _employee.EmploymentHistoryRecords.ElementAtOrDefault(index - 1);
                    if (record == null) return;
                    await Navigation.PushAsync(new EmployeeDetailsPage(record));
                    Debug.WriteLine("OnViewDetailsClicked executed successfully.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnViewDetailsClicked: {ex}");
            }
        }
    }
}