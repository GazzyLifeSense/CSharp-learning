using _06_SnakeGame.GameObject;
using _06_SnakeGame.Struct;

namespace _06_SnakeGame
{
    enum E_Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    internal class Snake: IDrawable
    {
        private SnakeBody[] bodys;
        private int index = 0;
        private E_Direction? moveDirection;
        public Snake(int x, int y)
        {
            bodys = new SnakeBody[200];
            bodys[index++] = new SnakeBody(x, y, E_BodyType.Head);
        }
        public void Draw()
        {
            for(int i = 0;i < index; i++)
            {
                bodys[i].Draw();
            }
        }
        public void Move()
        {
            if (moveDirection != null)
            {
                // 擦除尾部
                SnakeBody tail = bodys[index - 1];
                Console.SetCursorPosition(tail.pos.x, tail.pos.y);
                Console.Write("  ");

                // 身体补位
                for (int i = index - 1; i > 0; i--)
                {
                    bodys[i].pos = bodys[i - 1].pos;
                }

                // 头部移动
                switch (moveDirection)
                {
                    case E_Direction.Up:
                        --bodys[0].pos.y;
                        break;
                    case E_Direction.Down:
                        ++bodys[0].pos.y;
                        break;
                    case E_Direction.Left:
                        bodys[0].pos.x -= 2;
                        break;
                    case E_Direction.Right:
                        bodys[0].pos.x += 2;
                        break;
                }
            }
        }
        public void Turn(E_Direction direction)
        {
            if(direction != this.moveDirection)
            {
                if(index > 1 && (moveDirection == E_Direction.Left && direction == E_Direction.Right ||
                    moveDirection == E_Direction.Right && direction == E_Direction.Left ||
                    moveDirection == E_Direction.Up && direction == E_Direction.Down ||
                    moveDirection == E_Direction.Down && direction == E_Direction.Up))
                {
                    return;
                }
                this.moveDirection = direction;
            }
        }
        public bool CheckEnd(Map map)
        {
            for(int i = 0;i < map.blocks.Length; i++)
            {
                if (bodys[0].pos == map.blocks[i].pos)
                {
                    return true;
                }
            }
            for (int i = 1; i < index; i++)
            {
                if (bodys[0].pos == bodys[i].pos)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCollide(Position p)
        {
            for (int i = 1; i < index; i++)
            {
                if (p == bodys[i].pos)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckEatFood(Position p)
        {
            if (bodys[0].pos == p)
            {
                return true;
            }
            return false;
        }
        public void GrowUp()
        {
            SnakeBody tail = bodys[index - 1];
            bodys[index++] = new SnakeBody(tail.pos.x, tail.pos.y, E_BodyType.Body);
        }
    }
}
