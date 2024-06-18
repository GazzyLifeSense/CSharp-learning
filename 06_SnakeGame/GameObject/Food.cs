using _06_SnakeGame.Struct;

namespace _06_SnakeGame.GameObject
{
    internal class Food: GameObject
    {
        public Food(Snake snake){
            RandomPos(snake);
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("$");
        }

        public void RandomPos(Snake snake)
        {
            Random r = new Random();
            int x = r.Next(2, Game.w / 2 - 1) * 2;
            int y = r.Next(1, Game.h - 1);
            pos = new Position(x, y);
            if(snake.CheckCollide(pos)) RandomPos(snake);
        }
    }
}
