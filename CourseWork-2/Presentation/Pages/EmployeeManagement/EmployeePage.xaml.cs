using System.Diagnostics;
using CourseWork_2.Data.ViewModels;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage
    {
        private readonly EmployeeManagementPageViewModel _controller;

        public EmployeePage(EmployeeManagementPageViewModel controller)
        {
            _controller = controller;
            InitializeComponent();

            Title = "Сотрудник: " + _controller.SelectedHuman?.UserDefaultCredentials.FirstName + " " +
                    _controller.SelectedHuman?.UserDefaultCredentials.LastName + " - " + _controller.SelectedHuman?.EmploymentHistoryRecords.Last().PositionAtWork! ;
            
            EmployeeName = _controller.SelectedHuman?.UserDefaultCredentials.FirstName + " " +
                           _controller.SelectedHuman?.UserDefaultCredentials.LastName + " " +
                           _controller.SelectedHuman?.UserDefaultCredentials.SecondName;
            EmployeeNameLabel.TextColor = EntryUtil.GetInvertedColor(null);

            EmployeePosition = _controller.SelectedHuman?.EmploymentHistoryRecords.Last().PositionAtWork!;
            EmployeePositionLabel.TextColor = EntryUtil.GetInvertedColor(null);
            
            BindingContext = this;

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

        public string? EmployeePosition { get; set; }

        public string EmployeeName { get; set; }

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