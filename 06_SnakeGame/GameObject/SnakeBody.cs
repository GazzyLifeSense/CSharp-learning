namespace _06_SnakeGame.GameObject
{
    enum E_BodyType
    {
        Head,
        Body
    }
    internal class SnakeBody: GameObject
    {
        public E_BodyType type;
        public SnakeBody(int x, int y, E_BodyType type) {
            this.pos = new Struct.Position(x, y);
            this.type = type;
        }
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = type == E_BodyType.Head ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.Write(type == E_BodyType.Head ? "△" : "●");
        }
    }
}
