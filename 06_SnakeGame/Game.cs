using _06_SnakeGame.Scene;

namespace _06_SnakeGame
{
    // 场景类型
    enum E_SceneType
    {
        StartMenu,
        GameScene,
        EndMenu,
    }
    // 游戏窗体结构体
    internal class Game
    {
        // 窗口宽高
        public static int w, h;

        // 当前场景
        public static IUpdatable nowScene = new StartMenu();

        public Game(int w, int h)
        {
            Game.w = w; 
            Game.h = h;
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
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
                    nowScene = new GameScene(new Map());
                    break;
                case E_SceneType.EndMenu:
                    nowScene = new EndMenu();
                    break;
            }
        }
    }

}
