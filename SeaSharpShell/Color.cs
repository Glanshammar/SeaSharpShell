namespace SeaSharpShell;

public enum Colors
{
    DEFAULT,
    RED,
    GREEN,
    BLUE,
    YELLOW,
    CYAN,
    MAGENTA,
    WHITE
}

public static class Color
{
    public static void SetColor(Colors color)
    {
        switch (color)
        {
            case Colors.DEFAULT:
                Console.ResetColor();
                break;
            case Colors.RED:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Colors.GREEN:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case Colors.BLUE:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case Colors.YELLOW:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case Colors.CYAN:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case Colors.MAGENTA:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case Colors.WHITE:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }
}