using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_SnakeGame.Scene
{
    internal class StartMenu: BaseMenu
    {
        public StartMenu() : base("贪吃蛇", "开始游戏", "退出游戏")
        {
        }

        public override void Execute()
        {
            if (optIndex % 2 == 0) Game.ChangeScene(E_SceneType.GameScene);
            else Environment.Exit(0);
        }
    }
}
