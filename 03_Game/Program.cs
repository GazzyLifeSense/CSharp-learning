# region 控制台设置
Console.CursorVisible = false;
int w = 50, h = 20;
Console.SetWindowSize(w, h);
Console.SetBufferSize(w, h);
#endregion

int currentSceneId = 1;
while (true)
{
    switch (currentSceneId)
    {
        case 1: drawMainMenu();break;
        case 2: startGame();break;
        case 3: drawEndMenu();break;
    }
}
// 绘制主菜单
void drawMainMenu() {
    Console.SetCursorPosition(w / 2 - 7, 4);
    Console.Write("Rescue Princess");

    int func = 1;
    while (true)
    {
        bool breakWhile = false;

        Console.ForegroundColor = func % 2 == 0 ? ConsoleColor.White : ConsoleColor.Red;
        Console.SetCursorPosition(w / 2 - 5, 6);
        Console.Write("Start Game");

        Console.ForegroundColor = func % 2 == 0 ? ConsoleColor.Red : ConsoleColor.White;
        Console.SetCursorPosition(w / 2 - 4, 8);
        Console.Write("Exit Game");

        char? opt = Console.ReadKey(true).KeyChar;
        switch (opt)
        {
            case 'W':
            case 'w':
                func--;
                break;
            case 'S':
            case 's':
                func++;
                break;
            case '\u000D':
                if (func % 2 != 0) { currentSceneId = 2; breakWhile = true; }
                else Environment.Exit(0);
                break;
        }
        if (breakWhile) break;
    }
}

// 开始游戏
void startGame() {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;

    int playerX = 4, playerY = 4, playerAtk,
        princessX = 10, princessY = 5,
        monsterX = 10, monsterY = 10,
        monsterHp = 20, monsterDef = 10,
        borderTop = 0, borderLeft = 0,
        borderRight = Console.BufferWidth - 2, borderBottom = Console.BufferHeight - 5;
    bool princessAppear = false;

    for (int j = 0; j <  Console.BufferHeight; j++)
    {
        if(j == 0 || j == borderBottom || j == Console.BufferHeight - 1)
        {
            Console.SetCursorPosition(0, j);
            for(int i = 0; i <= borderRight; i+=2)
                Console.Write("■");
        }
        else
        {
            Console.SetCursorPosition(borderLeft, j);
            Console.Write('■');
            Console.SetCursorPosition(borderRight, j);
            Console.Write('■');
        }
    }

    // monster position
    Console.ForegroundColor = ConsoleColor.Green;
    Console.SetCursorPosition(monsterX, monsterY);
    Console.Write("■");
    Console.ForegroundColor = ConsoleColor.White;

    while (true)
    {
        int lastPositionX = playerX, lastPositionY = playerY;
        Console.SetCursorPosition(playerX, playerY);
        Console.Write("■");

        char? move = Console.ReadKey(true).KeyChar;

        Console.SetCursorPosition(playerX, playerY);
        Console.Write("  ");

        switch (move)
        {
            case 'a':
            case 'A':
                playerX -= 2;
                if (playerX < borderLeft + 2) playerX = borderLeft + 2;
                break;
            case 'd':
            case 'D':
                playerX += 2;
                if (playerX > borderRight - 2) playerX = borderRight - 2;
                break;
            case 'w':
            case 'W':
                playerY--;
                if(playerY < borderTop + 1) playerY = borderTop + 1;
                break;
            case 's':
            case 'S':
                playerY++;
                if (playerY > borderBottom - 1) playerY = borderBottom - 1;
                break;

        }

        // 战斗逻辑
        if (!princessAppear && playerX == monsterX && playerY == monsterY)
        {
            playerX = lastPositionX;
            playerY = lastPositionY;

            playerAtk = new Random().Next(8, 15);
            if(playerAtk > monsterDef)
            {
                monsterHp -= playerAtk - monsterDef;
                Console.SetCursorPosition(borderLeft + 2, borderBottom + 1);
                Console.Write($"atk: {playerAtk}, monster -{playerAtk - monsterDef}hp, now left: {monsterHp}hp".PadRight(Console.BufferWidth - 4));
            }
            else
            {
                Console.SetCursorPosition(borderLeft + 2, borderBottom + 1);
                Console.Write($"atk: {playerAtk}, cause no dmg.".PadRight(Console.BufferWidth - 4));
            }
            if (monsterHp <= 0)
            {
                // clear monster
                Console.SetCursorPosition(monsterX, monsterY);
                Console.Write("  ");

                // show princess
                princessAppear = true;
                Console.SetCursorPosition(princessX, princessY);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("■");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        // 营救逻辑
        if(princessAppear && playerX == princessX && playerY == princessY)
        {
            currentSceneId = 3;
            break;
        }
    }
}
// 绘制结束菜单
void drawEndMenu() {
    Console.SetCursorPosition(w / 2 - 7, 4);
    Console.Write("Rescue Success!");

    int func = 1;
    while (true)
    {
        bool breakWhile = false;

        Console.ForegroundColor = func % 2 == 0 ? ConsoleColor.White : ConsoleColor.Red;
        Console.SetCursorPosition(w / 2 - 5, 6);
        Console.Write("Play Again");

        Console.ForegroundColor = func % 2 == 0 ? ConsoleColor.Red : ConsoleColor.White;
        Console.SetCursorPosition(w / 2 - 4, 8);
        Console.Write("Exit Game");

        char? opt = Console.ReadKey(true).KeyChar;
        switch (opt)
        {
            case 'W':
            case 'w':
                func--;
                break;
            case 'S':
            case 's':
                func++;
                break;
            case '\u000D':
                if (func % 2 != 0) { currentSceneId = 2; breakWhile = true; }
                else Environment.Exit(0);
                break;
        }
        if (breakWhile) break;
    }
}