using SeaSharpShell;

class Program
{
    
    static void Main(string[] args)
    {
        Console.Title = "SeaSharpShell";

        while (true)
        {
            Color.SetColor(Colors.CYAN);
            Console.Write(Filesystem.CurrentDirectory);
            Color.SetColor(Colors.GREEN);
            Console.Write(" >> ");
            Color.SetColor(Colors.DEFAULT);
            string input = Console.ReadLine()?.ToLower() ?? string.Empty;

            if (!string.IsNullOrEmpty(input))
            {
                string[] inputParts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                string command = inputParts[0].ToLower();
                string[] commandArgs = inputParts.Length > 1 ? inputParts[1].Split(' ') : new string[0]; // Extract command arguments

                if (CommandList.Commands.ContainsKey(command))
                {
                    CommandList.Commands[command].Invoke(commandArgs);
                }
                else
                {
                    Console.WriteLine("No such command exists. Use the help command to get a list.");
                }
            }
        }
    }
}