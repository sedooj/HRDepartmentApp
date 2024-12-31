using CourseWork_2.Domain.Models;
using CourseWork_2.Data.ViewControllers;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class RewardPage
    {
        private readonly Human _employee;
        private readonly EmployeeManagementPageViewController _controller;

        public RewardPage(Human employee, EmployeeManagementPageViewController controller)
        {
            InitializeComponent();
            _employee = employee;
            _controller = controller;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (RewardTypePicker.SelectedIndex == -1)
                {
                    Console.WriteLine("Reward type not selected.");
                    await DisplayAlert("Ошибка", "Выберите награду", "OK");
                    return;
                }

                var selectedType = (Reward.RewardType)RewardTypePicker.SelectedIndex;

                var reward = new Reward(type: selectedType);
                _controller.GiveReward(_employee, reward);

                Console.WriteLine("Reward successfully added.");
                await DisplayAlert("Успех", "Награда успешно выдана", "OK");
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