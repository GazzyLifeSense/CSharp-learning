using System.Drawing;

namespace FlyingChessGame
{
    // 场景类型
    enum E_SceneType
    {
        StartMenu,
        GameScene,
        EndMenu,
    }

    // 格子类型
    enum E_CellType
    {
        Normal,
        RoadBlock,
        Bomb,
        Tunnel,
    }

    // 玩家类型
    enum E_PlayerType
    {
        Local,
        Bot,
    }
    // 游戏窗体结构体
    struct GameWindow
    {
        // 窗口宽高
        public int w, h;
        // 窗口前景，背景色
        public ConsoleColor foregroundColor, backgroundColor, borderColor, selectedForegroundColor;
        // 地图墙体
        public string borderIcon;
        // 地图活动边界
        public int borderLeft, borderRight, borderTop, borderBottom, maxCharLength;
        // 格子数组
        public Cell[] cellArr;
        public int endCellIndex;
        // 玩家数组
        public Player[] playerArr;

        public GameWindow(int w, int h, ConsoleColor foregroundColor, ConsoleColor backgroundColor, ConsoleColor borderColor, ConsoleColor selectedForegroundColor, string borderIcon = "■")
        {
            this.w = w;
            this.h = h;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.borderColor = borderColor;
            this.selectedForegroundColor = selectedForegroundColor;
            this.borderIcon = borderIcon;

            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Clear();

            borderLeft = 0;
            borderRight = w - 2;
            borderTop = 0;
            borderBottom = h - 11;
            maxCharLength = (borderRight - borderLeft - 2) / 2;
            cellArr = new Cell[w * h];
        }

        // 绘制地图框架
        public void drawBorder()
        {
            Console.ForegroundColor = borderColor;
            for (int row = 0; row < h; row++)
            {
                if (row == 0 || row == borderBottom || row == h - 6 || row == h - 1)
                {
                    for (int column = 0; column <= borderRight; column+=2)
                    {
                        Console.SetCursorPosition(column, row);
                        Console.Write(borderIcon);
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, row);
                    Console.Write(borderIcon);
                    Console.SetCursorPosition(borderRight, row);
                    Console.Write(borderIcon);
                }
            }
        }

        // 绘制格子类型描述
        public void drawDesc()
        {
            Cell normal = new Cell(E_CellType.Normal);
            Cell roadBlock = new Cell(E_CellType.RoadBlock);
            Cell bomb = new Cell(E_CellType.Bomb);
            Cell tunnel = new Cell(E_CellType.Tunnel);
            Player local = new Player(E_PlayerType.Local);
            Player bot = new Player(E_PlayerType.Bot);

            Console.SetCursorPosition(borderLeft + 2, borderBottom + 1);
            Console.ForegroundColor = normal.color;
            Console.Write($"{normal.icon}:{normal.desc}");

            Console.SetCursorPosition(borderLeft + 2, borderBottom + 2);
            Console.ForegroundColor = roadBlock.color;
            Console.Write($"{roadBlock.icon}:{roadBlock.desc}");
            Console.ForegroundColor = bomb.color;
            Console.Write($"\t{bomb.icon}:{bomb.desc}");

            Console.SetCursorPosition(borderLeft + 2, borderBottom + 3);
            Console.ForegroundColor = tunnel.color;
            Console.Write($"{tunnel.icon}:{tunnel.desc}");

            Console.SetCursorPosition(borderLeft + 2, borderBottom + 4);
            Console.ForegroundColor = local.color;
            Console.Write($"{local.icon}:{local.desc}");
            Console.ForegroundColor = bot.color;
            Console.Write($"\t{bot.icon}:{bot.desc}");
        }

        // 绘制格子
        public void drawCell()
        {
            int startRow = borderTop + 2, endRow = borderBottom - 2, index = 0,
                maxColumn = (borderRight - borderLeft - 2) / 2, 
                startColumn = new Random().Next(1, maxColumn + 1), endColumn = new Random().Next(1, maxColumn + 1);
            while(startRow <= endRow)
            {
                // 随机当行起始格
                if(startRow != borderTop + 2)
                {
                    startColumn = endColumn;
                }
                endColumn = new Random().Next(1, maxColumn + 1);

                // 绘制当前行格子
                for (int i = startColumn;
                        startColumn < endColumn ? i <= endColumn : i >= endColumn;
                        i = startColumn < endColumn ? i + 1 : i - 1)
                {
                    // 随机格子类型
                    int randomType = new Random().Next(1, 16);
                    E_CellType cellType;
                    if(randomType < 13){ cellType = E_CellType.Normal; }
                    else if(randomType < 14) { cellType = E_CellType.RoadBlock; }
                    else if(randomType < 15) { cellType = E_CellType.Bomb; }
                    else { cellType = E_CellType.Tunnel; }

                    Cell cell = new Cell(cellType, borderLeft + i * 2, startRow);
                    cellArr[index++] = cell;
                    cell.draw();
                }

                // 绘制换行格子
                if (startRow < endRow)
                {
                    Cell cell = new Cell(E_CellType.Normal, borderLeft + endColumn * 2, startRow + 1);
                    cellArr[index++] = cell;
                    cell.draw();
                    startRow++;
                }
                startRow++;
            }
            endCellIndex = index - 1;
        }
    }
    // 格子结构体
    struct Cell
    {
        public E_CellType type;
        public int x, y;
        public ConsoleColor color;
        public string? icon, desc;

        public Cell(E_CellType type, int x = 0, int y = 0) {
            this.type = type;
            this.x = x;
            this.y = y;

            switch(type)
            {
                case E_CellType.Normal:
                    this.icon = "□";
                    this.color = ConsoleColor.Gray;
                    this.desc = "普通格子";
                    break;
                case E_CellType.RoadBlock:
                    this.icon = "△";
                    this.color = ConsoleColor.Yellow;
                    this.desc = "路障，跳过下一回合";
                    break;
                case E_CellType.Bomb:
                    this.icon = "⊙";
                    this.color = ConsoleColor.Red;
                    this.desc = "炸弹，后退三格";
                    break;
                case E_CellType.Tunnel:
                    this.icon = "○";
                    this.color = ConsoleColor.Magenta;
                    this.desc = "隧道，随机传送";
                    break;
            }
        }
        public void draw()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }
    }
    // 玩家结构体
    class Player
    {
        public E_PlayerType type;
        public int x, y, index;
        public ConsoleColor color;
        public string? icon, desc;
        public bool skip;

        public Player(E_PlayerType type, int x = 0, int y = 0, int index = 0)
        {
            this.type = type;
            this.x = x;
            this.y = y;
            this.index = index;

            switch (type)
            {
                case E_PlayerType.Local:
                    this.icon = "★";
                    this.color = ConsoleColor.Cyan;
                    this.desc = "玩家";
                    break;
                case E_PlayerType.Bot:
                    this.icon = "★";
                    this.color = ConsoleColor.Green;
                    this.desc = "电脑";
                    break;
            }
        }
        public void draw()
        {
            draw(x, y, index);
        }
        // 逐格飞行
        public void draw(GameWindow gw, int index)
        {
            int point = index - this.index;
            int finalIndex = index > gw.endCellIndex ? gw.endCellIndex : index;
            Cell finalTarget = gw.cellArr[finalIndex];
            for (int i = this.index; i < finalIndex; i++)
            {
                Thread.Sleep(30);
                Cell targetCell = gw.cellArr[i + 1];
                // 绘制玩家到目标格子
                draw(targetCell.x, targetCell.y, i + 1);
                // 若当前格子空闲 -》 将格子重绘
                Player[] ps = gw.playerArr.Where((Player p) => p.index == i).ToArray();
                if (ps.Length == 0){ gw.cellArr[i].draw(); }
                else{ ps[0].draw(); }
            }
            Console.ForegroundColor = this.color;
            Console.SetCursorPosition(gw.borderLeft + 2, gw.h - 4);
            switch (finalTarget.type)
            {
                case E_CellType.Normal:
                    Console.Write($"{this.desc}未触发事件。".PadRight(gw.maxCharLength));
                    break;
                case E_CellType.RoadBlock:
                    Console.Write($"{this.desc}触发事件: 路障，跳过下一回合。".PadRight(gw.maxCharLength));
                    this.skip = true;
                    break;
                case E_CellType.Bomb:
                    Console.Write($"{this.desc}触发事件: 炸弹，返回起点。".PadRight(gw.maxCharLength));
                    draw(gw.cellArr[0].x, gw.cellArr[0].y, 0);
                    break;
                case E_CellType.Tunnel:
                    Console.Write($"{this.desc}触发事件: 隧道，随机传送到任意隧道。".PadRight(gw.maxCharLength));
                    Cell[] tunnelCells = gw.cellArr.Where(c => c.type == E_CellType.Tunnel).ToArray();
                    int tunnelIndex = new Random().Next(0, tunnelCells.Length);
                    draw(tunnelCells[tunnelIndex].x, tunnelCells[tunnelIndex].y, gw.cellArr.ToList().IndexOf(tunnelCells[tunnelIndex])); 
                    break;
            }
            // 重绘
            if(finalTarget.type != E_CellType.Normal)
            {
                Player[] ps = gw.playerArr.Where((Player p) => p.index == finalIndex).ToArray();
                if (ps.Length == 0){ gw.cellArr[finalIndex].draw(); }
                else{ ps[0].draw(); }
            }
        }
        public void draw(int x, int y, int index)
        {
                this.x = x;
                this.y = y;
                this.index = index;
                Console.ForegroundColor = this.color;
                Console.SetCursorPosition(x, y);
                Console.Write(icon);
        }
    }
    class Program
    { 
        // 初始化窗口
        static GameWindow gw = new GameWindow(60, 30, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Red);
        static E_SceneType currentSceneType = E_SceneType.StartMenu;

        static void Main(string[] args)
        {
    
            while (true)
            {
                switch (currentSceneType)
                {
                    case E_SceneType.StartMenu:
                        drawStartMenu();
                        break;
                    case E_SceneType.GameScene:
                        startGame();
                        break;
                    case E_SceneType.EndMenu:
                        drawEndMenu();
                        break;
                }
                Console.Clear();
            }
        }

        static void drawStartMenu()
        {
            Console.ForegroundColor = gw.foregroundColor;
            Console.SetCursorPosition(gw.w / 2 - 3, 4);
            Console.Write("飞行棋");

            int func = 1;
            while (true)
            {
                bool breakWhile = false;

                Console.ForegroundColor = func % 2 == 0 ? gw.foregroundColor : gw.selectedForegroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 10);
                Console.Write("开始游戏");

                Console.ForegroundColor = func % 2 == 0 ? gw.selectedForegroundColor : gw.foregroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 12);
                Console.Write("退出游戏");

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
                        if (func % 2 != 0) { currentSceneType = E_SceneType.GameScene; breakWhile = true; }
                        else Environment.Exit(0);
                        break;
                }
                if (breakWhile) break;
            }
        }

        static void startGame()
        {
            Player? winner = null;

            gw.drawBorder();
            gw.drawDesc();
            gw.drawCell();

            Player local = new Player(E_PlayerType.Local, gw.cellArr[0].x, gw.cellArr[0].y, 0);
            Player bot = new Player(E_PlayerType.Bot, gw.cellArr[0].x, gw.cellArr[0].y, 0);
            local.draw();
            bot.draw();
            gw.playerArr = [local, bot];

            while(winner == null)
            {
                bool breakFor = false;
                for (int i = 0; i <= gw.playerArr.Length - 1 && !breakFor;)
                {
                    char? key = Console.ReadKey(true).KeyChar;
                    switch(key)
                    {
                        case 'j':
                        case 'J':
                            Player curPlayer = gw.playerArr[i];
                            int dice = new Random().Next(1, 7);
                            int targetIndex = dice + curPlayer.index;
                            Console.ForegroundColor = curPlayer.color;
                            Console.SetCursorPosition(gw.borderLeft + 2, gw.h - 5);
                            // 跳过回合
                            if (curPlayer.skip)
                            {
                                curPlayer.skip = false;
                                Console.Write($"{curPlayer.desc}跳过本轮行动...".PadRight(gw.maxCharLength));
                                Console.SetCursorPosition(gw.borderLeft + 2, gw.h - 4);
                                Console.Write(" ".PadRight(26));
                            }
                            // 正常回合
                            else
                            {
                                Console.Write($"{curPlayer.desc}投掷点数为：{dice}".PadRight(gw.maxCharLength));

                                if (targetIndex < gw.endCellIndex)
                                {
                                    curPlayer.draw(gw, targetIndex);
                                }
                                else
                                {
                                    curPlayer.draw(gw, targetIndex);
                                    winner = curPlayer;
                                    breakFor = true;
                                }
                            }
                       
                            i++;
                            break;
                    }
                }
                if (winner != null)
                {
                    Console.ForegroundColor = winner.color;
                    Console.SetCursorPosition(gw.borderLeft + 2, gw.h - 3);
                    Console.Write($"{winner.desc}赢得了比赛！ 按任意键继续...");
                    Console.ReadKey(true);
                    currentSceneType = E_SceneType.EndMenu;
                }
            }
        }

        static void drawEndMenu()
        {
            Console.ForegroundColor = gw.foregroundColor;
            Console.SetCursorPosition(gw.w / 2 - 4, 4);
            Console.Write("游戏结束");

            int func = 1;
            while (true)
            {
                bool breakWhile = false;

                Console.ForegroundColor = func % 2 == 0 ? gw.foregroundColor : gw.selectedForegroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 10);
                Console.Write("再玩一次");

                Console.ForegroundColor = func % 2 == 0 ? gw.selectedForegroundColor : gw.foregroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 12);
                Console.Write("退出游戏");

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
                        if (func % 2 != 0) { currentSceneType = E_SceneType.GameScene; breakWhile = true; }
                        else Environment.Exit(0);
                        break;
                }
                if (breakWhile) break;
            }
        }
    }
}
