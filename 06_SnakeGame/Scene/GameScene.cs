using _06_SnakeGame.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_SnakeGame.Scene
{
    internal class GameScene : IUpdatable
    {
        Map map;
        Snake snake;
        Food food;

        public GameScene(Map map) {
            this.map = map;
            this.snake = new Snake(40, 10);
            this.food = new Food(snake);
        }
        public void Update()
        {
            // 绘制地图
            map.Draw();
            food.Draw();
            // 蛇体移动
            snake.Move();
            // 绘制最新蛇体
            snake.Draw();
            // 判断是否撞墙
            if (snake.CheckEnd(map))
            {
                Game.ChangeScene(E_SceneType.EndMenu);
                return;
            }
            // 判断是否进食
            if (snake.CheckEatFood(food.pos))
            {
                snake.GrowUp();
                food.RandomPos(snake);        
            }
            // 判断是否激活输入，避免程序阻塞
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.Turn(E_Direction.Up); break;
                    case ConsoleKey.S:
                        snake.Turn(E_Direction.Down); break;
                    case ConsoleKey.A:
                        snake.Turn(E_Direction.Left); break;
                    case ConsoleKey.D:
                        snake.Turn(E_Direction.Right); break;
                }
            }
            Thread.Sleep(100);
        }
    }
}
