using CourseWork_2.Models;
using CourseWork_2.ViewControllers;

namespace CourseWork_2;

public static class Validator
{
    public static bool ValidateHuman(HumanDataHolder human)
    {
        return ValidateName(human.UserDefaultCredentials.FirstName) &&
               ValidateName(human.UserDefaultCredentials.LastName) &&
               ValidateName(human.UserDefaultCredentials.SecondName) &&
               RequireDateIsNotGreaterThanNow(human.UserDefaultCredentials.DateOfBirth) &&
               ValidateHomeAddress(human.UserDefaultCredentials.HomeAddress) &&
               ValidatePhoneNumber(human.UserDefaultCredentials.PhoneNumber) &&
               ValidatePassport(human.Passport);
    }

    public static bool ValidateName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }

    public static bool RequireDateIsNotGreaterThanNow(DateTime? dateOfBirth)
    {
        return dateOfBirth.HasValue && dateOfBirth.Value <= DateTime.Now;
    }

    public static bool ValidateHomeAddress(string homeAddress)
    {
        return !string.IsNullOrWhiteSpace(homeAddress);
    }

    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.All(char.IsDigit);
    }

    public static bool ValidatePassport(Passport? passport)
    {
        return passport != null &&
               ValidateInt(passport.Serial, out _) && passport.Serial.Length == 4 &&
               ValidateInt(passport.Number, out _) && passport.Number.Length == 6 &&
               RequireDateIsNotGreaterThanNow(passport.DateOfIssue) &&
               !string.IsNullOrWhiteSpace(passport.WhoIssued);
    }

    public static bool ValidateInt(string value, out int result)
    {
        return int.TryParse(value, out result);
    }
}