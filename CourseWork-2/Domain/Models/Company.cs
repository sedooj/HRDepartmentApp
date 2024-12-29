namespace CourseWork_2.Domain.Models;

public class Company
{
    private string _name;
    private string _address;
    private string _phone;
    private List<Employee> _employees;

    public Company(string name, string address, string phone, List<Employee> employees)
    {
        _name = name;
        _address = address;
        _phone = phone;
        _employees = employees;
    }

    public string Name => _name;
    public string Address => _address;
    public string Phone => _phone;

    public List<Employee> Employees
    {
        get => _employees;
        set => _employees = value;
    }
}