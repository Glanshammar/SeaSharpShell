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
            int age = CalculateAge(dob);
            Console.WriteLine($"Your age is: {age} years.");
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter your date of birth in YYYY-MM-DD format.");
        }
    }
    
    static int CalculateAge(DateTime dob)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age))
        {
            age--; // User hasn't had birthday yet this year
        }
        return age;
    }
}