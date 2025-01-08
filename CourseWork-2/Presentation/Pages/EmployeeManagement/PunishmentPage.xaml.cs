using CourseWork_2.Domain.Models;
using System;
using System.Diagnostics;
using CourseWork_2.Data.Controllers;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement;

public partial class PunishmentPage
{
    private readonly EmployeeManagementPageController _controller;

    public PunishmentPage(EmployeeManagementPageController controller)
    {
        InitializeComponent();
        _controller = controller;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (PunishmentTypePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Ошибка", "Выберите тип наказания", "OK");
                return;
            }

            var selectedType = (Punishment.PunishmentType)PunishmentTypePicker.SelectedIndex;

            string reason = await DisplayPromptAsync("Причина", "Введите причину:");
            
            if (selectedType == Punishment.PunishmentType.Demotion)
            {
                string newPosition = await DisplayPromptAsync("Новая должность", "Введите новую должность:");
                if (string.IsNullOrEmpty(newPosition))
                {
                    await DisplayAlert("Ошибка", "Введите новую должность", "OK");
                    return;
                }
                _controller.DemoteEmployee(_controller.SelectedHuman!.Uuid, newPosition, reason);
            }
            var punishment = new Punishment(id: Guid.NewGuid(), selectedType, date: DateTime.Now, reason: reason);
            _controller.PunishEmployee(_controller.SelectedHuman!, punishment);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in OnSaveClicked: {ex.Message}");
            await DisplayAlert("Ошибка", "Ошибка при выдаче наказания", "OK");
        }
    }
}