using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Controllers;

public class EmployeePageController
{

    private PageComponents Components { get; set; }
    public void InitPage(EmployeeManagementPageController controller, CollectionView employmentHistoryCollectionView, Label employeeNameLabel, Label employeePositionLabel)
    {
        employmentHistoryCollectionView.ItemsSource = controller.SelectedHuman?.EmploymentHistoryRecords
            .Select((record, index) => new
            {
                Index = index + 1,
                record.PositionAtWork,
                WorkingPeriod =
                    $"{record.WorkingStartDate:dd-MM-yyyy} - {(record.WorkingEndDate.HasValue ? record.WorkingEndDate.Value.ToString("dd.MM.yyyy") : " Нынешнее время")}"
            }).ToList();
        employeeNameLabel.Text = controller.SelectedHuman?.UserDefaultCredentials.FirstName + " " +
                                 controller.SelectedHuman?.UserDefaultCredentials.LastName + " " +
                                 controller.SelectedHuman?.UserDefaultCredentials.SecondName;
        employeePositionLabel.Text = controller.SelectedHuman?.LastEmploymentHistoryRecord?.PositionAtWork!;
        employeeNameLabel.TextColor = EntryUtil.GetInvertedColor(null);
        employeePositionLabel.TextColor = EntryUtil.GetInvertedColor(null);
        
        Components = new PageComponents
        {
            Title = "Сотрудник: " + controller.SelectedHuman?.UserDefaultCredentials.FirstName + " " +
                               controller.SelectedHuman?.UserDefaultCredentials.LastName + " - " +
                               controller.SelectedHuman?.LastEmploymentHistoryRecord?.PositionAtWork!,
            EmployeeNameLabel = employeeNameLabel,
            EmployeePositionLabel = employeePositionLabel,
            EmploymentHistoryCollectionView = employmentHistoryCollectionView
        };
    }

    public EmploymentHistoryRecord? OnViewDetailsClicked(object sender, EmployeeManagementPageController controller)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int index)
        {
            var asd = controller.SelectedHuman?.EmploymentHistoryRecords.ElementAtOrDefault(index - 1);
            return asd;
        }
        return null;
    }
    
    public class PageComponents
    {
        public required string Title { get; init; }
        public required Label EmployeeNameLabel { get; init; }
        public required Label EmployeePositionLabel { get; init; }
        public required CollectionView EmploymentHistoryCollectionView{ get; init; }
    }
}