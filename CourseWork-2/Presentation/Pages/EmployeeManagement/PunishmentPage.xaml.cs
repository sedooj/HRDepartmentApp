using CourseWork_2.Domain.Models;
using CourseWork_2.Data.ViewControllers;
using System;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement;

public partial class PunishmentPage
{
    private readonly Human _employee;
    private readonly EmployeeManagementPageViewController _controller;

    public PunishmentPage(Human employee, EmployeeManagementPageViewController controller)
    {
        InitializeComponent();
        _employee = employee;
        _controller = controller;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (PunishmentTypePicker.SelectedIndex == -1)
            {
                Console.WriteLine("Punishment type not selected.");
                await DisplayAlert("Ошибка", "Выберите наказание", "OK");
                return;
            }

            var selectedType = (Punishment.PunishmentType)PunishmentTypePicker.SelectedIndex;
            var punishment = new Punishment(type: selectedType, date: DateTime.Now);
            _controller.PunishEmployee(_employee, punishment);

            Console.WriteLine("Punishment successfully added.");
            await DisplayAlert("Успех", "Наказание успешно добавлено", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnSaveClicked: {ex.Message}");
            await DisplayAlert("Ошибка", "Ошибка добавления наказания", "OK");
        }
    }
}