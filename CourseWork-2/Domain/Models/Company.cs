namespace CourseWork_2.Domain.Models
{
    public class Company
    {
        private string _id;
        private string _name;
        private string _address;
        private string _phone;
        private List<string> _employeeUUIDs;

        public Company(string id, string name, string address, string phone, List<string> employeeUUIDs)
        {
            _id = id;
            _name = name;
            _address = address;
            _phone = phone;
            _employeeUUIDs = employeeUUIDs;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }

        public List<string> EmployeeUUIDs
        {
            get => _employeeUUIDs;
            set => _employeeUUIDs = value;
        }
    }
}