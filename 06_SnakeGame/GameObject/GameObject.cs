using _06_SnakeGame.Struct;

namespace _06_SnakeGame.GameObject
{
    internal abstract class GameObject : IDrawable
    {
        // 位置
        public Position pos;
        public abstract void Draw();
    }
}
