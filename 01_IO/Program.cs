using System.Diagnostics;
Console.CursorVisible = false;
Console.BackgroundColor = ConsoleColor.Red;
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Clear();

char? inp;
int initialX = 10;
int initialY = 10;

while (true)
{
    // Console.Clear();
    Console.SetCursorPosition(initialX, initialY);
    Console.Write("□");

    inp = Console.ReadKey(true).KeyChar;
    // 擦除之前位置内容
    Console.SetCursorPosition(initialX, initialY);
    Console.Write("  ");
    switch (inp.ToString()?.ToLower())
    {
        case "a":
            initialX -= 2;
            if(initialX < 0) initialX = 0;
            break;
        case "d":
            initialX += 2;
            if(initialX > Console.BufferWidth - 2) initialX = Console.BufferWidth - 2;
            break;
        case "w":
            initialY--;
            if(initialY < 0) initialY = 0;
            break;
        case "s":
            initialY++;
            if(initialY > Console.BufferHeight - 1) initialY = Console.BufferHeight - 1;
            break;
    }
}
