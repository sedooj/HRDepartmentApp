using Microsoft.Maui.Controls;
using CourseWork.entity;

namespace CourseWork_2.Pages.userCreation;

public partial class EducationPage
{
    public EducationPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // var education = new Education
        // {
        //     Institution = InstitutionEntry.Text,
        //     Degree = DegreeEntry.Text,
        //     FieldOfStudy = FieldOfStudyEntry.Text,
        //     StartYear = int.Parse(StartYearEntry.Text),
        //     EndYear = int.Parse(EndYearEntry.Text)
        // };
        // MessagingCenter.Send(this, "EducationSaved", education);
        await Navigation.PopAsync();
    }
}