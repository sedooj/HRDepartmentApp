namespace CourseWork.entity
{
    public sealed class UserDefaultCredentials
    {
        private string _firstName;
        private string _lastName;
        private string _secondName;
        private int _age;
        private string _homeAddress;
        private string _phoneNumber;
        private string _userPhotoUrl;

        public UserDefaultCredentials(string firstName, string lastName, string secondName, int age, string homeAddress,
            string phoneNumber, string userPhotoUrl)
        {
            _firstName = firstName;
            _lastName = lastName;
            _secondName = secondName;
            _age = age;
            _homeAddress = homeAddress;
            _phoneNumber = phoneNumber;
            _userPhotoUrl = userPhotoUrl;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string SecondName => _secondName;

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public string HomeAddress
        {
            get => _homeAddress;
            set => _homeAddress = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public string UserPhotoUrl
        {
            get => _userPhotoUrl;
            set => _userPhotoUrl = value;
        }
    }
}