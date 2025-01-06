using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;
using System.Diagnostics;
using System.Linq;
using CourseWork_2.Data.ViewModels;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage
    {
        private readonly EmployeeManagementPageViewModel _controller;

        public EmployeePage(EmployeeManagementPageViewModel controller)
        {
            _controller = controller;
            InitializeComponent();

            Title = _controller.SelectedHuman?.UserDefaultCredentials.FirstName + " " +
                    _controller.SelectedHuman?.UserDefaultCredentials.LastName + " – " + _controller.SelectedHuman?.EmploymentHistoryRecords.Last().PositionAtWork;
            BindingContext = _controller;


            try
            {
                Debug.WriteLine("Initializing EmployeePage...");
                EmploymentHistoryCollectionView.ItemsSource = _controller.SelectedHuman?.EmploymentHistoryRecords
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
                await Navigation.PushAsync(new RewardPage(_controller));
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
                await Navigation.PushAsync(new PunishmentPage(_controller));
                Debug.WriteLine("OnPunishClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnPunishClicked: {ex}");
            }
        }

        private async void OnViewDetailsClicked(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("OnViewDetailsClicked started.");
                var button = sender as Button;
                if (button?.CommandParameter is int index)
                {
                    var record = _controller.SelectedHuman?.EmploymentHistoryRecords.ElementAtOrDefault(index - 1);
                    if (record == null)
                    {
                        Debug.WriteLine("Error while OnViewDetailsClicked. Record is null.");
                        return;
                    }
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