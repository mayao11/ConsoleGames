using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拳愿阿修罗
{
    class Program
    {
      
       
        static void Main(string[] args)
        {
            Console.WindowWidth = 115;
            Console.WindowHeight = 40;
            Console.CursorVisible = false;
            Manager m = new Manager();
            m.Player = Data.CreateCharacter1();
            m.Player.type = CharaType.Player;
            m.Computer = Data.CreateCharacter2();
            m.Computer.type = CharaType.Computer;
            m.DrawBattle();
            m.RoundBattle();
            Console.ReadKey();
        }
    }
}
