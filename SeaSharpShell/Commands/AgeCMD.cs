namespace SeaSharpShell.Commands;

public class AgeCMD
{
    public static void Age()
    {
        Console.WriteLine("When were you born? (YYYY-MM-DD)");
        Console.Write("Date: ");
        string input = Console.ReadLine()?.ToLower() ?? string.Empty;

        if (TryParseDate(input, out DateTime dob))
        {
            AgeResult age = CalculateAge(dob);
            Console.WriteLine($"Your age is: {age.Years} years and {age.Days} days.");
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter your date of birth in YYYY-MM-DD or YYYYMMDD format.");
        }
    }

    static bool TryParseDate(string input, out DateTime dob)
    {
        // Try parsing standard format YYYY-MM-DD
        if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dob))
        {
            return true;
        }

        // Try parsing numeric format YYYYMMDD
        if (input.Length == 8 && int.TryParse(input, out int dateValue))
        {
            int year = dateValue / 10000;
            int month = (dateValue % 10000) / 100;
            int day = dateValue % 100;

            // Construct a DateTime object and validate it
            try
            {
                dob = new DateTime(year, month, day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                dob = default;
                return false;
            }
        }

        dob = default;
        return false;
    }

    static AgeResult CalculateAge(DateTime dob)
    {
        DateTime today = DateTime.Today;
        int years = today.Year - dob.Year;

        // Adjust years if birthday hasn't occurred yet this year
        if (dob.Date > today.AddYears(-years))
        {
            years--;
        }

        // Calculate the number of days
        DateTime lastBirthday = dob.AddYears(years);
        int days = (today - lastBirthday).Days;

        return new AgeResult(years, days);
    }

    public struct AgeResult
    {
        public int Years { get; }
        public int Days { get; }

        public AgeResult(int years, int days)
        {
            Years = years;
            Days = days;
        }
    }
}