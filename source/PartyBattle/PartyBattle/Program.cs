using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    class Program
    {
        public static Random random = new Random();
        public static int width = 100;
        public static int height = 30;
        static void Main(string[] args)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;
            Console.CursorVisible = false;
            BattleManager bm = new BattleManager();
            bm.heros.Add(DataManager.CreateCharSatsuki());
            bm.heros.Add(DataManager.CreateCharEruruu());
            bm.heros.Add(DataManager.CreateCharRenne());
            bm.heros.Add(DataManager.CreateCharMatthew());
            bm.enemy.Add(DataManager.CreateCharSlimeRed());
            bm.enemy.Add(DataManager.CreateCharSlimeBlue());
            bm.enemy.Add(DataManager.CreateCharSlimeKing());
            bm.DrawBattleField();
            bm.BattleStart();
            Console.ReadKey();
        }
    }
}
