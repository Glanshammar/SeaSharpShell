namespace SeaSharpShell.Commands;

public class Filesystem
{
    private static string _currentDirectory = @"C:/";

    public static string currentDirectory
    {
        get => NormalizePath(_currentDirectory);
        private set => _currentDirectory = value;
    }
    
    public static string NormalizePath(string path)
    {
        return path.Replace('\\', '/');
    }
    
    static void Print(Color color, params string[] messages)
    {
        ColorHelper.SetColor(color);
        foreach (var message in messages)
        {
            Console.Write(message);
        }
        Console.WriteLine();
        ColorHelper.SetColor(Color.DEFAULT);
    }
    
    public static void ListFiles()
    {
        try
        {
            string path = currentDirectory;

            var entries = Directory.GetFileSystemEntries(path);

            if (entries.Length == 0)
            {
                Console.WriteLine("The directory is empty.");
                return;
            }

            foreach (var entry in entries)
            {
                if (Directory.Exists(entry))
                {
                    ColorHelper.SetColor(Color.GREEN);
                }
                else if (File.Exists(entry))
                {
                    ColorHelper.SetColor(Color.YELLOW);
                }
                Console.WriteLine(Path.GetFileName(entry));
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Print(Color.RED, "Error accessing directory: ", e.Message);
        }

        ColorHelper.SetColor(Color.DEFAULT);
    }
    
    public static void ChangeDirectory(params string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine($"No path provided. Staying in the current directory: {NormalizePath(_currentDirectory)}");
            return;
        }

        // Join all arguments into a single path string
        string path = string.Join(" ", args);

        if (string.IsNullOrEmpty(path))
        {
            Console.WriteLine($"No path provided. Staying in the current directory: {NormalizePath(_currentDirectory)}");
            return;
        }

        if (!Path.IsPathRooted(path))
        {
            path = Path.Combine(_currentDirectory, path);
        }

        try
        {
            if (Directory.Exists(path))
            {
                _currentDirectory = path;
                Console.WriteLine($"Changed directory to: {NormalizePath(_currentDirectory)}");
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while changing directory: {ex.Message}");
        }
    }
    
    public static void CreateDirectory(params string[] args)
    {
        string path = string.IsNullOrEmpty(args[0]) ? currentDirectory : args[0];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            Console.WriteLine("Directory already exists.");
        }
    }
    
    public static void DeleteDirectory(params string[] args)
    {
        string path = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
    }
    
    public static void DeleteFile(params string[] args)
    {
        string path = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }
    
    public static void CopyFile(params string[] args)
    {
        string source = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        string destination = string.IsNullOrEmpty(args[1]) ? "NULL" : args[1];
        if (File.Exists(source))
        {
            File.Copy(source, destination);
        }
        else
        {
            Console.WriteLine("Source file does not exist.");
        }
    }
    
    public static void MoveFile(params string[] args)
    {
        string source = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        string destination = string.IsNullOrEmpty(args[1]) ? "NULL" : args[1];
        if (File.Exists(source))
        {
            File.Move(source, destination);
        }
        else
        {
            Console.WriteLine("Source file does not exist.");
        }
    }
    
    public static void RenameFile(params string[] args)
    {
        string file = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        string newName = string.IsNullOrEmpty(args[1]) ? "NULL" : args[1];
        if (File.Exists(file))
        {
            string directory = Path.GetDirectoryName(file);
            string newFile = Path.Combine(directory, newName);
            File.Move(file, newFile);
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }
    
    public static void ReadFile(params string[] args)
    {
        string file = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        if (File.Exists(file))
        {
            string content = File.ReadAllText(file);
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }
    
    public static void CreateFile(params string[] args)
    {
        string file = string.IsNullOrEmpty(args[0]) ? "NULL" : args[0];
        if (!File.Exists(file))
        {
            File.Create(file);
        }
        else
        {
            Console.WriteLine("File already exists.");
        }
    }
}