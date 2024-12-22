using CourseWork_2.Pages;
using Microsoft.Maui.Controls;

namespace CourseWork_2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnUserCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserCreationPage());
        }
    }
}