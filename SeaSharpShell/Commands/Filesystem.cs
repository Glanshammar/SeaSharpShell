namespace SeaSharpShell.Commands;

public class Filesystem
{
    private static bool LINUX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    private static bool MACOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    private static bool WINDOWS = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    private static string _currentDirectory = GetDefaultDirectory();

    
    private static string GetDefaultDirectory()
    {
        if (WINDOWS)
            return @"C:\";
        else if (LINUX || MACOS)
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        else
            throw new NotSupportedException("Unsupported OS platform.");
    }
    
    public static string CurrentDirectory
    {
        get => NormalizePath(_currentDirectory);
        private set => _currentDirectory = value ?? GetDefaultDirectory();
    }

    public static string NormalizePath(string path)
    {
        return path.Replace(Path.DirectorySeparatorChar == '/' ? '\\' : '/', Path.DirectorySeparatorChar);
    }

    static void Print(Colors color, params string[] messages)
    {
        Color.SetColor(color);
        foreach (var message in messages)
            Console.Write(message);
        
        Console.WriteLine();
        Color.SetColor(Colors.DEFAULT);
    }

    public static void ListFiles()
    {
        try
        {
            string path = CurrentDirectory;

            var entries = Directory.GetFileSystemEntries(path, "*", new EnumerationOptions
            {
                AttributesToSkip = FileAttributes.System,
                ReturnSpecialDirectories = false
            });

            if (entries.Length == 0)
            {
                Console.WriteLine("The directory is empty.");
                return;
            }

            foreach (var entry in entries)
            {
                FileAttributes attr = File.GetAttributes(entry);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    Color.SetColor(Colors.GREEN);
                else
                    Color.SetColor(Colors.YELLOW);

                Console.WriteLine($"{Path.GetFileName(entry)} {(attr.HasFlag(FileAttributes.Hidden) ? "(Hidden)" : "")}");
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Print(Colors.RED, "Error accessing directory: ", e.Message);
        }

        Color.SetColor(Colors.DEFAULT);
    }

    public static void ChangeDirectory(params string[] args)
    {
        if (args.Length == 0)
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

        if (path == "..")
        {
            DirectoryInfo? parent = Directory.GetParent(_currentDirectory);
            if (parent != null)
            {
                CurrentDirectory = parent.FullName;
                Console.WriteLine($"Changed directory to: {NormalizePath(_currentDirectory)}");
            }
            else
            {
                Console.WriteLine("Already at the root directory.");
            }
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
                CurrentDirectory = path;
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
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No directory name provided.");
            return;
        }
        string directoryName = args[0];
        string fullPath = Path.GetFullPath(Path.Combine(CurrentDirectory, directoryName));
    
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
            Console.WriteLine("Directory created: " + fullPath);
        }
        else
        {
            Console.WriteLine("Directory already exists: " + fullPath);
        }
    }

    public static void DeleteDirectory(params string[] args)
    {
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No directory name provided.");
            return;
        }
        string directoryName = args[0];
        string fullPath = Path.GetFullPath(Path.Combine(CurrentDirectory, directoryName));
    
        if (Directory.Exists(fullPath))
        {
            Directory.Delete(fullPath, true);
            Console.WriteLine("Directory removed: " + fullPath);
        }
        else
        {
            Console.WriteLine("Directory does not exist: " + fullPath);
        }
    }

    public static void DeleteFile(params string[] args)
    {
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No file name provided.");
            return;
        }
        string fileName = args[0];
        string fullPath = Path.GetFullPath(Path.Combine(CurrentDirectory, fileName));
    
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Console.WriteLine("File deleted: " + fullPath);
        }
        else
        {
            Console.WriteLine("File does not exist: " + fullPath);
        }
    }

    public static void CopyFile(params string[] args)
    {
        if (args.Length < 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
        {
            Console.WriteLine("Invalid arguments provided for CopyFile.");
            return;
        }

        string source = args[0];
        string destination = args[1];
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
        if (args.Length < 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
        {
            Console.WriteLine("Invalid arguments provided for MoveFile.");
            return;
        }

        string source = args[0];
        string destination = args[1];
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
        if (args.Length < 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
        {
            Console.WriteLine("Invalid arguments provided for RenameFile.");
            return;
        }

        string file = args[0];
        string newName = args[1];
        if (File.Exists(file))
        {
            string directory = Path.GetDirectoryName(file);
            if (directory == null)
            {
                Console.WriteLine("Directory name could not be determined.");
                return;
            }
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
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No file name provided.");
            return;
        }

        string file = args[0];
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
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No file name provided.");
            return;
        }

        string file = args[0];
        if (!File.Exists(file))
        {
            using (var fs = File.Create(file))
            {
                // Ensure the file is closed immediately after creation
            }
        }
        else
        {
            Console.WriteLine("File already exists.");
        }
    }
    
    public static void OpenFile(params string[] args)
    {
        if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("No file name provided.");
            return;
        }

        string fileName = args[0];
        string fullPath = Path.GetFullPath(Path.Combine(CurrentDirectory, fileName));

        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"File does not exist: {fullPath}");
            return;
        }

        try
        {
            // This will open the file with the default associated application
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = fullPath,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);

            Console.WriteLine($"Opened file : {fullPath}");
        }
        catch (System.ComponentModel.Win32Exception) // Win32Exception is cross-platform
        {
            Console.WriteLine($"No application is associated with this file type: {Path.GetExtension(fullPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening file: {ex.Message}");
        }
    }
}