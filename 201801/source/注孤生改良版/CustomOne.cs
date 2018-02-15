using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 注孤生改良版
{
    public class CustomOne
    {
        // 利用Delegate，简化MapPos函数的使用。否则每次用MapPos都要写Rogue.MapPos
        delegate int DelegateMapPos(int x, int y);
        static DelegateMapPos MapPos = Rogue.MapPos;
        delegate void DelegateMapXY(int pos, out int x, out int y);
        static DelegateMapXY MapXY = Rogue.MapXY;
        // 自定义关卡流程，新加关卡可以再后面加，也可以删除关卡或者调整顺序
        //public static void AddAllLevels()
        //{
        //    Rogue.AddLevel(InitLevel1);
        //}

        //public static void InitLevel1()
        //{
            //Rogue.level_target = "提示：";

            //Monster monster1 = new Monster("♀", 1,5,1);
            //monster1.SetPos(9, 9);
            //Rogue.monsters[MapPos(monster1.x, monster1.y)] = monster1;

            //Monster monster2 = new Monster("〓", 1, 5, 1);
            //monster2.SetPos(4, 4);
            //Rogue.monsters[MapPos(monster2.x, monster2.y)] = monster2;

            //Monster monster3 = new Monster("￡", 1, 5, 1);
            //monster3.SetPos(7, 7);
            //Rogue.monsters[MapPos(monster3.x, monster3.y)] = monster3;

            //Monster monster4 = new Monster("※", 1, 5, 1);
            //monster4.SetPos(6, 6);
            //Rogue.monsters[MapPos(monster4.x, monster4.y)] = monster4;

            //Door door = new Door("〓", 1001);
            //door.SetPos(2, 2);
            //Rogue.doors[MapPos(door.x, door.y)] = door;

            //Wall wall = new Wall("█", 1001);
            //wall.SetPos(10, 2);
            //Rogue.walls[MapPos(wall.x, wall.y)] = wall;

            //ElectricityDoor electricitydoor = new ElectricityDoor("〓", 4001);
            //electricitydoor.SetPos(3, 3);
            //Rogue.electricitydoors[MapPos(electricitydoor.x, electricitydoor.y)] = electricitydoor;

            //ElectricSwitch electricSwitch = new ElectricSwitch("※", 2001);
            //electricSwitch.SetPos(5, 5);
            //Rogue.electricSwitchs[MapPos(electricSwitch.x, electricSwitch.y)] = electricSwitch;

            //LongPole longPole = new LongPole("￡", 3001);
            //longPole.SetPos(4, 10);
            //Rogue.longpoles[MapPos(longPole.x, longPole.y)] = longPole;

            //Key key = new Key("♀", 4001);
            //key.SetPos(8, 8);
            //Rogue.keys[MapPos(key.x, key.y)] = key;

            //Thorn thorn = new Thorn("◆", 5001);
            //thorn.SetPos(15, 10);
            //Rogue.thorns[MapPos(thorn.x, thorn.y)] = thorn;

            //OldMan oldman = new OldMan("何炅", 101);
            //oldman.SetPos(16,16);
            //Rogue.npcs[MapPos(oldman.x, oldman.y)] = oldman;

            //Transfer transfer1 = new Transfer("¤", 100);
            //transfer1.SetPos(1, 10);
            //Rogue.transfers[MapPos(transfer1.x, transfer1.y)] = transfer1;

            //Transfer transfer2 = new Transfer("¤", 100);
            //transfer2.SetPos(2, 10);
            //Rogue.transfers[MapPos(transfer2.x, transfer2.y)] = transfer2;

            //Rogue.onMonsterDead = (Monster monster) =>
            //{
            //    if (Rogue.monsters.Count() <= 1)
            //    {
            //        Rogue.OnStageClear();
            //    }
            //    return true;
            //};
        }
}
