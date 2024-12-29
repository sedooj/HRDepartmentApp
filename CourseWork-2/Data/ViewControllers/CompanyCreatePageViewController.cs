using System.Diagnostics;
using System.Text.Json;
using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.ViewControllers;

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

    public bool CreateCompany(string name, string address, string phone)
    {
        try
        {
            if (!ValidateCompanyName(name) || !ValidateCompanyAddress(address) || !ValidateCompanyPhone(phone))
            {
                DisplayAlert("Validation Error", "Some fields are filled incorrectly.", "OK");
                return false;
            }

            Company = new Company(name, address, phone, new List<Employee>());

            Debug.WriteLine("Company entity created successfully.");
            Console.WriteLine("Success");
            return true;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "An error occurred while creating the company.", "OK");
            Debug.WriteLine($"Error creating Company entity: {ex}");
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }

    public void SaveCompanyToJson()
    {
        try
        {
            string json = JsonSerializer.Serialize(Company);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "companies");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, $"{Company.Name}.json");
            File.WriteAllText(filePath, json);
            Debug.WriteLine("Company saved to JSON file successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving Company to JSON file: {ex}");
            Console.WriteLine($"Error: {ex}");
        }
    }

    private void DisplayAlert(string title, string message, string cancel)
    {
        Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}