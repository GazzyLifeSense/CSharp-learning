namespace _06_SnakeGame.Scene
{
    internal abstract class BaseMenu: IUpdatable
    {
        protected int optIndex = 0;
        string title, optionOne, optionTwo;
        public BaseMenu(string title, string optionOne, string optionTwo)
        {
            this.title = title;
            this.optionOne = optionOne;
            this.optionTwo = optionTwo;
        }

        public void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 3, 4);
            Console.Write(title);

            Console.ForegroundColor = optIndex % 2 == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 10);
            Console.Write(optionOne);

            Console.ForegroundColor = optIndex % 2 == 0 ? ConsoleColor.White : ConsoleColor.Red;
            Console.SetCursorPosition(Console.BufferWidth / 2 - 4, 12);
            Console.Write(optionTwo);

            char? opt = Console.ReadKey(true).KeyChar;
            switch (opt)
            {
                case 'W':
                case 'w':
                    optIndex--;
                    break;
                case 'S':
                case 's':
                    optIndex++;
                    break;
                case '\u000D':
                    Execute();
                    break;
            }
        }

        public abstract void Execute();
    }
}
