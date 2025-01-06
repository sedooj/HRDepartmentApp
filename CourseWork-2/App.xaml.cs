using CourseWork_2.Presentation.Pages;

namespace CourseWork_2;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}