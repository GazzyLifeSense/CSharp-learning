using _06_SnakeGame.Scene;

namespace _06_SnakeGame
{
    internal // 游戏窗体结构体
    class Game
    {
        // 窗口宽高
        public int w, h;
        // 窗口前景，背景色
        public ConsoleColor foregroundColor = ConsoleColor.White, 
            backgroundColor = ConsoleColor.Black,
            borderColor = ConsoleColor.Red, 
            selectedForegroundColor = ConsoleColor.Red;
        // 地图墙体
        public string borderIcon = "■";
        // 地图活动边界
        public int borderLeft = 0, borderRight, borderTop = 0, borderBottom, maxCharLength;
        // 当前场景
        public static IUpdatable nowScene = new StartMenu();

        public Game(int w, int h)
        {
            this.w = w;
            this.h = h;

            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Clear();

            borderRight = w - 2;
            borderBottom = h - 1;
            maxCharLength = (borderRight - borderLeft - 2) / 2;
        }

        // 绘制地图框架
        public void DrawBorder()
        {
            Console.ForegroundColor = borderColor;
            for (int row = 0; row < h; row++)
            {
                if (row == 0 || row == borderBottom)
                {
                    for (int column = 0; column <= borderRight; column += 2)
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

        public void Start()
        {
            while (true)
            {
                if (nowScene != null)
                {
                    nowScene.Update();
                }
            }
        }

        public static void ChangeScene(E_SceneType type)
        {
            Console.Clear();

            switch (type)
            {
                case E_SceneType.StartMenu:
                    nowScene = new StartMenu();
                    break;
                case E_SceneType.GameScene:
                    nowScene = new GameScene();
                    break;
                case E_SceneType.EndMenu:
                    nowScene = new EndMenu();
                    break;
            }
        }
    }

}
