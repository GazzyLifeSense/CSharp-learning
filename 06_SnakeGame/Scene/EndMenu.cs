namespace _06_SnakeGame.Scene
{
    internal class EndMenu : BaseMenu
    {
        public EndMenu(): base("游戏结束", "再玩一次", "退出游戏")
        {
        }

        public override void Execute()
        {
            if (optIndex % 2 == 0) Game.ChangeScene(E_SceneType.GameScene);
            else Environment.Exit(0);
        }
    }
}
