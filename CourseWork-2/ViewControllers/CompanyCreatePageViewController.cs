using CourseWork_2.Models;
using System.Diagnostics;

namespace CourseWork_2.ViewControllers;

public class CompanyCreatePageViewController
{
    public Company Company { get; private set; }

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

    public async Task<bool> CreateCompany(string name, string address, string phone)
    {
        try
        {
            if (!ValidateCompanyName(name) || !ValidateCompanyAddress(address) || !ValidateCompanyPhone(phone))
            {
                await DisplayAlert("Validation Error", "Some fields are filled incorrectly.", "OK");
                return false;
            }

            Company = new Company(name, address, phone);

            Debug.WriteLine("Company entity created successfully.");
            Console.WriteLine("Success");
            return true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred while creating the company.", "OK");
            Debug.WriteLine($"Error creating Company entity: {ex}");
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }

    private Task DisplayAlert(string title, string message, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}