using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Data.ViewModels;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class RewardPage
    {
        private readonly EmployeeManagementPageViewModel _controller;

        public RewardPage(EmployeeManagementPageViewModel controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (RewardTypePicker.SelectedIndex == -1)
                {
                    Debug.WriteLine("RewardPage, Reward type not selected.");
                    await DisplayAlert("Ошибка", "Выберите награду", "OK");
                    return;
                }

                var selectedType = (Reward.RewardType)RewardTypePicker.SelectedIndex;

                if (selectedType == Reward.RewardType.Promotion)
                {
                    if (_controller.SelectedHuman == null)
                    {
                        Debug.WriteLine("RewardPage, Selected human is null.");
                        return;
                    }
                    
                    string newPosition = await DisplayPromptAsync("Повышение", "Введите новую должность:");
                    if (string.IsNullOrEmpty(newPosition))
                    {
                        await DisplayAlert("Ошибка", "Введите новую должность", "OK");
                        return;
                    }

                    string reason = await DisplayPromptAsync("Повышение", "Введите причину:");
                    if (string.IsNullOrEmpty(reason))
                    {
                        await DisplayAlert("Ошибка", "Введите причину повышения", "OK");
                        return;
                    }       

                    _controller.PromoteEmployee(_controller.SelectedHuman.Uuid, newPosition, reason);
                }
                else
                {
                    string reason = await DisplayPromptAsync("Повышение", "Введите причину:");
                    if (string.IsNullOrEmpty(reason))
                    {
                        await DisplayAlert("Ошибка", "Введите причину повышения", "OK");
                        return;
                    } 
                    var reward = new Reward(id: Guid.NewGuid().ToString(), type: selectedType, date: DateTime.Now, reason: reason);
                    _controller.GiveReward(_controller.SelectedHuman!, reward);

                    Debug.WriteLine("RewardPage, Reward successfully added.");
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnSaveClicked: {ex.Message}");
                await DisplayAlert("Ошибка", "Ошибка выдачи награды", "OK");
            }
        }
    }
}