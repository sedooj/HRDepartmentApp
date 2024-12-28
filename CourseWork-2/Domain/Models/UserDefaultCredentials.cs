namespace CourseWork_2.Domain.Models
{
    public sealed class UserDefaultCredentials
    {
        private string _firstName;
        private string _lastName;
        private string _secondName;
        private DateTime _dateOfBirth;
        private string _homeAddress;
        private string _phoneNumber;
        private string _userPhotoUrl;

        public UserDefaultCredentials(string firstName, string lastName, string secondName, DateTime dateOfBirth, string homeAddress,
            string phoneNumber, string userPhotoUrl)
        {
            _firstName = firstName;
            _lastName = lastName;
            _secondName = secondName;
            _dateOfBirth = dateOfBirth;
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

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
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