namespace FlyingChessGame
{
    struct GameWindow
    {
        // 窗口宽高
        public int w, h;
        // 窗口前景，背景色
        public ConsoleColor foregroundColor, backgroundColor, borderColor, selectedForegroundColor;
        // 地图墙体
        public string borderIcon;
        // 地图活动边界
        public int borderLeft, borderRight, borderTop, borderBottom;

        public GameWindow(int w, int h, ConsoleColor foregroundColor, ConsoleColor backgroundColor, ConsoleColor borderColor,  ConsoleColor selectedForegroundColor, string borderIcon = "■") 
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
            borderBottom = h - 1;
        }

        public void drawBorder()
        {
            Console.ForegroundColor = borderColor;
            for (int row = 0;row < h;row++)
            {
                if(row == 0 || row == borderBottom || row == h - 1)
                {
                    for(int column = 0; column <= borderRight - 2;column++) {
                        Console.SetCursorPosition(column, row);
                        Console.Write(borderIcon);
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, row);
                    Console.Write(borderIcon);
                    Console.SetCursorPosition(borderRight - 2, row);
                    Console.Write(borderIcon);
                }
            }
        }
    }

    enum SceneType
    {
        StartMenu,
        GameScene,
        EndMenu,
    }

    class Program
    { 
        // 初始化窗口
        static GameWindow gw = new GameWindow(50, 20, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Red, ConsoleColor.Red);
        static SceneType currentSceneType = SceneType.StartMenu;
        static string endInfo = "";

        static void Main(string[] args)
        {
    
            while (true)
            {
                switch (currentSceneType)
                {
                    case SceneType.StartMenu:
                        drawStartMenu();
                        break;
                    case SceneType.GameScene:
                        startGame();
                        break;
                    case SceneType.EndMenu:
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
                Console.SetCursorPosition(gw.w / 2 - 4, 6);
                Console.Write("开始游戏");

                Console.ForegroundColor = func % 2 == 0 ? gw.selectedForegroundColor : gw.foregroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 8);
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
                        if (func % 2 != 0) { currentSceneType = SceneType.GameScene; breakWhile = true; }
                        else Environment.Exit(0);
                        break;
                }
                if (breakWhile) break;
            }
        }

        static void startGame()
        {
            gw.drawBorder();
            Console.ReadLine();
        }

        static void drawEndMenu()
        {
            Console.ForegroundColor = gw.foregroundColor;
            Console.SetCursorPosition(gw.w / 2 - 4, 4);
            Console.Write(endInfo);

            int func = 1;
            while (true)
            {
                bool breakWhile = false;

                Console.ForegroundColor = func % 2 == 0 ? gw.foregroundColor : gw.selectedForegroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 6);
                Console.Write("再玩一次");

                Console.ForegroundColor = func % 2 == 0 ? gw.selectedForegroundColor : gw.foregroundColor;
                Console.SetCursorPosition(gw.w / 2 - 4, 8);
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
                        if (func % 2 != 0) { currentSceneType = SceneType.GameScene; breakWhile = true; }
                        else Environment.Exit(0);
                        break;
                }
                if (breakWhile) break;
            }
        }
    }
}
