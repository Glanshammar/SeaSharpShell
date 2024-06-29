namespace SeaSharpShell.Commands;

public class AgeCMD
{
    public static void Age()
    {
        Console.WriteLine("When are you born? (YYYY-MM-DD)");
        Console.Write("Date: ");
        DateTime dob;

        if (DateTime.TryParse(Console.ReadLine(), out dob))
        {
            AgeResult age = CalculateAge(dob);
            Console.WriteLine($"Your age is: {age.Years} years and {age.Days} days.");
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter your date of birth in YYYY-MM-DD format.");
        }
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