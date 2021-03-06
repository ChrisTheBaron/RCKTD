using System;

namespace RCKTD
{
    public static class Program
    {

        public static Game1 Game { get; private set; }

        [STAThread]
        static void Main()
        {
            using (Game = new Game1())
                Game.Run();
        }
    }
}
