using CourseWork_2.Data.Controllers;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class RewardPage
    {
        private readonly EmployeeManagementPageController _controller;

        public RewardPage(EmployeeManagementPageController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (RewardTypePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Ошибка", "Выберите награду", "OK");
                return;
            }

            var selectedType = (Reward.RewardType)RewardTypePicker.SelectedIndex;
            if (selectedType == Reward.RewardType.Promotion)
            {
                if (_controller.SelectedHuman == null) return;
                string newPosition = await DisplayPromptAsync(selectedType.ToString(), "Введите новую должность:");
                if (string.IsNullOrEmpty(newPosition))
                {
                    await DisplayAlert("Ошибка", "Введите новую должность", "OK");
                    return;
                }

                string reason = await DisplayPromptAsync(selectedType.ToString(), "Введите причину:");
                if (string.IsNullOrEmpty(reason))
                {
                    await DisplayAlert("Ошибка", "Введите причину повышения", "OK");
                    return;
                }

                _controller.PromoteEmployee(_controller.SelectedHuman.Uuid, newPosition, reason);
            }
            else
            {
                string reason = await DisplayPromptAsync(selectedType.ToString(), "Введите причину:");
                if (string.IsNullOrEmpty(reason))
                {
                    await DisplayAlert("Ошибка", "Введите причину повышения", "OK");
                    return;
                } 
                var reward = new Reward(id: Guid.NewGuid().ToString(), type: selectedType, date: DateTime.Now, reason: reason);
                _controller.GiveReward(_controller.SelectedHuman!, reward);
            }

            await Navigation.PopAsync();
        }
    }
}