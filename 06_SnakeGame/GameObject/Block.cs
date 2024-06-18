namespace _06_SnakeGame.GameObject
{
    internal class Block: GameObject
    {
        public Block(int x, int y)
        {
            this.pos = new Struct.Position(x, y);
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
        }
    }
}
