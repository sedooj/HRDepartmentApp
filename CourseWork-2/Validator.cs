namespace CourseWork_2
{
    public static class Validator
    {
        public static bool RequireNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool RequireGreaterThan(int value, int threshold)
        {
            return value > threshold;
        }

        public static bool ValidateInt(string input, out int result)
        {
            if (!int.TryParse(input, out result)) return false;
            return result >= 0;
        }

        public static bool ValidateDate(string input)
        {
            return DateTime.TryParse(input, out _);
        }
    }
}