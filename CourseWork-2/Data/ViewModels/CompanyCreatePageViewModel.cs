using System.Diagnostics;
using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.ViewModels;

public class CompanyCreatePageViewModel
{

    private readonly IStorage<Company> _companyService = new LocalStorageService<Company> ();
    
    public bool ValidateCompanyName(string name)
    {
        return Validator.ValidateName(name);
    }

    public bool ValidateCompanyAddress(string address)
    {
        return Validator.ValidateHomeAddress(address);
    }

    public bool ValidateCompanyPhone(string phone)
    {
        return Validator.ValidatePhoneNumber(phone);
    }

    public void CreateCompany(string name, string address, string phone)
    {
        try
        {
            if (!ValidateCompanyName(name) || !ValidateCompanyAddress(address) || !ValidateCompanyPhone(phone))
            {
                DisplayAlert("Validation Error", "Some fields are filled incorrectly.", "OK");
                return;
            }
            var company = new Company(Guid.NewGuid().ToString(), name, address, phone, new List<string>());
            _companyService.SaveEntity($"{Config.CompanyStoragePath}{company.Id}", company);
            Debug.WriteLine("Company entity created successfully.");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "An error occurred while creating the company.", "OK");
            Debug.WriteLine($"Error creating Company entity: {ex}");
        }
    }


    private void DisplayAlert(string title, string message, string cancel)
    {
        Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}