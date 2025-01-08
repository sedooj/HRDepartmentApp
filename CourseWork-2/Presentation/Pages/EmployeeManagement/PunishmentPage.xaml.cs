using CourseWork_2.Domain.Models;
using CourseWork_2.Data.ViewControllers;
using System;
using System.Diagnostics;
using CourseWork_2.Data.ViewModels;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement;

public partial class PunishmentPage
{
    private readonly EmployeeManagementPageViewModel _controller;

    public PunishmentPage(EmployeeManagementPageViewModel controller)
    {
        InitializeComponent();
        _controller = controller;
    }

    private async void OnPunishmentTypeChanged(object sender, EventArgs e)
    {
        if (PunishmentTypePicker.SelectedIndex != -1)
        {
            var selectedType = (Punishment.PunishmentType)PunishmentTypePicker.SelectedIndex;
            if (selectedType == Punishment.PunishmentType.Demotion)
            {
                ReasonLabel.IsVisible = false;
            }
            else
            {
                string reason = await DisplayPromptAsync("Причина", "Введите причину:");
                if (!string.IsNullOrEmpty(reason))
                {
                    ReasonLabel.Text = $"Причина: {reason}";
                    ReasonLabel.IsVisible = true;
                }
                else
                {
                    ReasonLabel.IsVisible = false;
                }
            }
        }
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

            if (selectedType == Punishment.PunishmentType.Demotion)
            {
                string newPosition = await DisplayPromptAsync("Новая должность", "Введите новую должность:");
                if (string.IsNullOrEmpty(newPosition))
                {
                    await DisplayAlert("Ошибка", "Введите новую должность", "OK");
                    return;
                }

                string reason = await DisplayPromptAsync("Причина", "Введите причину:");
                if (string.IsNullOrEmpty(reason))
                {
                    await DisplayAlert("Ошибка", "Введите причину", "OK");
                    return;
                }

                _controller.DemoteEmployee(_controller.SelectedHuman!.Uuid, newPosition, reason);
            }
            else
            {
                string reason = ReasonLabel.Text.Replace("Причина: ", "");
                if (string.IsNullOrEmpty(reason))
                {
                    await DisplayAlert("Ошибка", "Введите причину", "OK");
                    return;
                }

                var punishment = new Punishment(id: Guid.NewGuid().ToString(), selectedType, date: DateTime.Now, reason: reason);
                _controller.PunishEmployee(_controller.SelectedHuman!, punishment);
            }

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in OnSaveClicked: {ex.Message}");
            await DisplayAlert("Ошибка", "Ошибка при выдаче наказания", "OK");
        }
    }
}