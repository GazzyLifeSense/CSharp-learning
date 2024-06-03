# region 控制台设置
using System;

Console.CursorVisible = false;
int w = 50, h = 20;
Console.SetWindowSize(w, h);
Console.SetBufferSize(w, h);
#endregion

int currentSceneId = 1;
string endInfo = "";

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

    // 玩家属性
    int playerX = 4, playerY = 4, playerHp = 100, playerAtk;
    ConsoleColor playerColor = ConsoleColor.White;
    string playerIcon = "■";

    // 怪物属性
    int monsterX = 10, monsterY = 10,
        monsterHp = 20, monsterDef = 10, monsterAtk;
    ConsoleColor monsterColor = ConsoleColor.Green;
    string monsterIcon = "■";

    // 公主属性
    int princessX = 10, princessY = 5;
    ConsoleColor princessColor = ConsoleColor.Magenta;
    string princessIcon = "★";

    // 地图属性
    int borderTop = 0, borderLeft = 0,
        borderRight = Console.BufferWidth - 2, borderBottom = Console.BufferHeight - 5;
    ConsoleColor defaultColor = ConsoleColor.White;
    string borderIcon = "■";

    #region 绘制地图
    for (int j = 0; j <  Console.BufferHeight; j++)
    {
        if(j == 0 || j == borderBottom || j == Console.BufferHeight - 1)
        {
            Console.SetCursorPosition(0, j);
            for(int i = 0; i <= borderRight; i+=2)
                Console.Write(borderIcon);
        }
        else
        {
            Console.SetCursorPosition(borderLeft, j);
            Console.Write(borderIcon);
            Console.SetCursorPosition(borderRight, j);
            Console.Write(borderIcon);
        }
    }
    #endregion

    while (true)
    {
        #region 绘制随机移动的怪兽
        if(monsterHp > 0)
        {
            Console.ForegroundColor = monsterColor;
            Console.SetCursorPosition(monsterX, monsterY);
            Console.Write(monsterIcon);
            Console.ForegroundColor = defaultColor;
        }
        #endregion

        // 记录玩家移动前位置
        int lastPositionX = playerX, lastPositionY = playerY;
        Console.ForegroundColor = playerColor;
        Console.SetCursorPosition(playerX, playerY);
        Console.Write(playerIcon);

        char? move = Console.ReadKey(true).KeyChar;

        Console.SetCursorPosition(playerX, playerY);
        Console.Write("  ");

        // 玩家移动逻辑(不会与怪兽或公主重合)
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
        if (monsterHp > 0 && playerX == monsterX && playerY == monsterY)
        {
            playerX = lastPositionX;
            playerY = lastPositionY;

            playerAtk = new Random().Next(18, 25);
            monsterAtk = new Random().Next(8, 15);

            playerHp -= monsterAtk;
            Console.SetCursorPosition(borderLeft + 2, borderBottom + 1);
            Console.Write($"monster atk: {monsterAtk}, player -{monsterAtk}hp, ({(playerHp > 0 ? playerHp : 0)}hp remain)".PadRight(Console.BufferWidth - 4));
            if (playerHp > 0)
            {
                if (playerAtk > monsterDef)
                {
                    monsterHp -= playerAtk - monsterDef;
                    Console.SetCursorPosition(borderLeft + 2, borderBottom + 2);
                    Console.Write($"player atk: {playerAtk}, monster -{playerAtk - monsterDef}hp, ({(monsterHp > 0 ? monsterHp : 0)}hp remain)".PadRight(Console.BufferWidth - 4));
                }
                else
                {
                    Console.SetCursorPosition(borderLeft + 2, borderBottom + 2);
                    Console.Write($"player atk: {playerAtk}, cause no dmg.".PadRight(Console.BufferWidth - 4));
                }
            }
            #region 玩家被打败
            else
            {
                endInfo = "营救失败";
                currentSceneId = 3;
                break;
            }
            #endregion

            // 怪物被打败
            if (monsterHp <= 0)
            {
                // 清除怪物
                Console.SetCursorPosition(monsterX, monsterY);
                Console.Write("  ");

                // 绘制公主
                Console.SetCursorPosition(princessX, princessY);
                Console.ForegroundColor = princessColor;
                Console.Write(princessIcon);
                Console.ForegroundColor = defaultColor;
            }
            // 怪兽被攻击后随机移动
            else
            {
                
                Console.SetCursorPosition(monsterX, monsterY);
                Console.Write("  ");
                monsterX = new Random().Next((borderLeft + 2) / 2, (borderRight - 3) / 2) * 2;
                monsterY = new Random().Next(borderTop + 1, borderBottom - 3);
            }
        }
        
        // 营救逻辑
        if(monsterHp < 0 && playerX == princessX && playerY == princessY)
        {
            endInfo = "营救成功";
            currentSceneId = 3;
            break;
        }
    }
}

// 绘制结束菜单
void drawEndMenu() {
    Console.SetCursorPosition(w / 2 - 4, 4);
    Console.Write(endInfo);

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