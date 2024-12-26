namespace CourseWork_2.ViewControllers;

public class PassportController
{
    private const int MaxSerialLength = 4;
    private const int MaxNumberLength = 6;

    public (bool, bool) ValidateSerial(string serial)
    {
        if (!Validator.ValidateInt(serial, out _)) return (false, true);
        var result = serial.Length == MaxSerialLength ? (true, false) : (false, true);
        return result;
    }

    public (bool, bool) ValidateNumber(string number)
    {
        if (!Validator.ValidateInt(number, out _)) return (false, true);
        var result = number.Length == MaxNumberLength ? (true, false) : (false, true);
        return result;
    }

    public async Task<bool> ValidateInputs(Entry serialEntry, Entry numberEntry, DatePicker dateOfIssueEntry, Entry whoIssuedEntry)
    {
        // Implement validation logic for other fields if needed
        return true;
    }
}