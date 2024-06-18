using _06_SnakeGame.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_SnakeGame
{
    internal class Map: IDrawable
    {
        public Block[] blocks;
        public Map()
        {
            this.blocks = new Block[Game.w + (Game.h - 3) * 2];
            int index = 0;
            // 上下边
            for (int i = 0; i < Game.w; i += 2)
            {
                blocks[index++] = new Block(i, 0);
            }
            for (int i = 0; i < Game.w; i += 2)
            {
                blocks[index++] = new Block(i, Game.h - 2);
            }
            // 侧边（与上下重合部分不用再画）
            for (int i = 1; i < Game.h - 2; i++)
            {
                blocks[index++] = new Block(0, i);
            }
            for (int i = 1; i < Game.h - 2; i++)
            {
                blocks[index++] = new Block(Game.w - 2, i);
            }
        }

        public void Draw()
        {
            foreach (Block block in blocks)
            {
                block.Draw();
            }
        }
    }
}
