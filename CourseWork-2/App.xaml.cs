using Microsoft.Maui.Controls;

namespace CourseWork_2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}