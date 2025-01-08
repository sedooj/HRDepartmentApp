using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeDetailsPage
    {
        public EmployeeDetailsPage(EmploymentHistoryRecord record)
        {
            InitializeComponent();
            BindingContext = record;
        }
        
    }
}