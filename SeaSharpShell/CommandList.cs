namespace SeaSharpShell;

public class CommandList
{
    public delegate void CommandAction(params string[] args);

    public static Dictionary<string, CommandAction> Commands { get; } = new Dictionary<string, CommandAction>()
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
}