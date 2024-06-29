using SeaSharpShell.Commands;
using System.Diagnostics;

class Program
{
    delegate void CommandAction(params string[] args);
    static void Main(string[] args)
    {
        Console.Title = "SeaSharpShell";
        Dictionary<string, CommandAction> commandActions = new Dictionary<string, CommandAction>()
        {
            { "help", (args) => HelpCMD.ShowHelp() },
            { "age", (args) => AgeCMD.Age() },
            { "ls", (args) => Filesystem.ListFiles() },
            { "cd", (args) => Filesystem.ChangeDirectory(args) },
            { "mkdir", (args) => Filesystem.CreateDirectory(args) },
            { "rmdir", (args) => Filesystem.DeleteDirectory(args) },
            { "touch", (args) => Filesystem.CreateFile(args) },
            { "read", (args) => Filesystem.ReadFile(args) },
            { "rename", (args) => Filesystem.RenameFile(args) },
            { "mv", (args) => Filesystem.MoveFile(args) },
            { "cp", (args) => Filesystem.CopyFile(args) },
            { "delete", (args) => Filesystem.DeleteFile(args) },
            { "clear", (args) => Console.Clear() },
            { "cls", (args) => Console.Clear() },
            { "quit", (args) => Environment.Exit(0) },
            { "exit", (args) => Environment.Exit(0) },
        };
        

        while (true)
        {
            Console.Write(Filesystem.currentDirectory + " >> ");
            string input = Console.ReadLine().ToLower();

            if (!string.IsNullOrEmpty(input))
            {
                string[] inputParts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                string command = inputParts[0].ToLower();
                string[] commandArgs = inputParts.Length > 1 ? inputParts[1].Split(' ') : new string[0]; // Extract command arguments

                if (commandActions.ContainsKey(command))
                {
                    commandActions[command].Invoke(commandArgs); // Pass the command arguments
                }
                else
                {
                    Console.WriteLine("No such command exists. Use the help command to get a list.");
                }
            }
        }
    }
}