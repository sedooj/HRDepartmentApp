using Microsoft.Maui.Controls;

namespace CourseWork_2.Pages;

public partial class UserCreationPage
{
    public UserCreationPage()
    {
        InitializeComponent();
    }

    private void OnCreateUserClicked(object? sender, EventArgs e)
    {
        
        
    }

    private void OnAgeEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(e.NewTextValue, out var inputAge) || inputAge <= 0)
        {
            AgeErrorLabel.IsVisible = true;
        }
        else
        {
            AgeErrorLabel.IsVisible = false;
        }
    }
}