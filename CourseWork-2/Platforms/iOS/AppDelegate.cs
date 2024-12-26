using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace CourseWork_2;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}