namespace SeaSharpShell;


public enum Color
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

public static class ColorHelper
{
    public static void SetColor(Color color)
    {
        switch (color)
        {
            case Color.DEFAULT:
                Console.ResetColor();
                break;
            case Color.RED:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Color.GREEN:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case Color.BLUE:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case Color.YELLOW:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case Color.CYAN:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case Color.MAGENTA:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case Color.WHITE:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }
}