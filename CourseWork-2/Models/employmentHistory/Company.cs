namespace CourseWork.entity.employmentHistory;

public class Company
{
    private string _name;
    private string _address;
    private string _phone;

    public Company(string name, string address, string phone)
    {
        _name = name;
        _address = address;
        _phone = phone;
    }

    public string Name => _name;
    public string Address => _address;
    public string Phone => _phone;
}