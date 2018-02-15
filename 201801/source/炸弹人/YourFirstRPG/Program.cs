using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YourFirstRPG
{
    class ObjectManager
    {
        public Dictionary<int, Objects> objectdict = new Dictionary<int, Objects>();
        static int counter = 0;
        public void AddObject(Objects objects)
        {
            counter += 1;
            objectdict[counter] = objects;
            objects.id = counter;
        }
        public void RemoveObjectByid(int id)
        {
            objectdict.Remove(id);
        }
    }
    class Map
    {
        public char[,] mapframe = new char[20, 50];
        public void ShowMap(ObjectManager objectmanager)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (j == 49 && i != 19)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(this.mapframe[i, j]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        if (this.mapframe[i, j] == '口')
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(this.mapframe[i, j]);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            if (this.mapframe[i,j]== '■')
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write(this.mapframe[i, j]);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else if (this.mapframe[i, j] == '●')
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write(this.mapframe[i, j]);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else if (this.mapframe[i, j] == '□')
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(this.mapframe[i, j]);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else if (this.mapframe[i, j] == '★')
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(this.mapframe[i, j]);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else
                                Console.Write(this.mapframe[i, j]);
                        }
                    }
                }
            }
        }
        public Map(ObjectManager objectmanager)
        {
            this.Border();
            this.Background(objectmanager.objectdict);
        }
        public void Border()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (i != 0 && j != 0 && i != 19 && j != 49)
                    {
                        mapframe[i, j] = '口';
                        //mapframe[i, j] = ' ';
                    }
                    else
                        //mapframe[i, j] = '#';
                        mapframe[i, j] = '■';
                }
            }
        }
        public void Background(Dictionary<int, Objects> objectlist)
        {
            List<Objects> Level0Victor = new List<Objects>();
            List<Objects> Level0Speed = new List<Objects>();
            List<Objects> Level0Damage = new List<Objects>();
            List<Objects> Level1Monster = new List<Objects>();
            List<Objects> Level1Brick = new List<Objects>();
            List<Objects> Level1Grass = new List<Objects>();
            List<Objects> Level2 = new List<Objects>();
            foreach (var pair in objectlist)
            {
                if (pair.Value.ShowLevel == 0&&pair.Value is Door)
                {
                    Level0Victor.Add(pair.Value);
                }
                else if (pair.Value.ShowLevel == 0 && pair.Value is SpeedBuff)
                {
                    Level0Speed.Add(pair.Value);
                }
                else if (pair.Value.ShowLevel == 0 && pair.Value is DamageBuff)
                {
                    Level0Damage.Add(pair.Value);
                }
            }
            foreach (var pair in objectlist)
            {
                if (pair.Value.ShowLevel == 1&&pair.Value is Monster)
                {
                    Level1Monster.Add(pair.Value);
                }
                else if (pair.Value.ShowLevel == 1 && pair.Value is Brick)
                {
                    Level1Brick.Add(pair.Value);
                }
                else if (pair.Value.ShowLevel==1&&pair.Value is Grass)
                {
                    Level1Grass.Add(pair.Value);
                }
            }
            foreach (var pair in objectlist)
            {
                if (pair.Value.ShowLevel == 2)
                {
                    Level2.Add(pair.Value);
                }
            }
            for (int i = 0; i < Level0Victor.Count; i++)
            {
                this.mapframe[Level0Victor[i].x, Level0Victor[i].y] = 'Π';
            }
            for (int i = 0; i < Level0Speed.Count; i++)
            {
                this.mapframe[Level0Speed[i].x, Level0Speed[i].y] = 'ξ';
            }
            for (int i = 0; i < Level0Damage.Count; i++)
            {
                this.mapframe[Level0Damage[i].x, Level0Damage[i].y] = 'Ω';
            }
            for (int i = 0; i < Level1Monster.Count; i++)
            {
                this.mapframe[Level1Monster[i].x, Level1Monster[i].y] = '●';
            }
            for (int i = 0; i < Level1Brick.Count; i++)
            {
                this.mapframe[Level1Brick[i].x, Level1Brick[i].y] = '■';
                //this.mapframe[Level1Brick[i].x, Level1Brick[i].y] = '▢';
            }
            for (int i = 0; i < Level1Grass.Count; i++)
            {
                this.mapframe[Level1Grass[i].x,Level1Grass[i].y] = '□';
            }
            for (int i = 0; i < Level2.Count; i++)
            {
                this.mapframe[Level2[i].x, Level2[i].y] = '★';
            }
        }
    }
    class Objects
    {
        public int ExistTime;
        public int id;
        public int x;
        public int y;
        public int ShowLevel;
    }
    class Boom : Objects
    {
        public Boom(int x, int y)
        {
            this.ExistTime = 15;
            this.ShowLevel = 3;
            this.x = x;
            this.y = y;
        }
    }
    class Brick : Objects
    {
        public Brick(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.ShowLevel = 1;
        }
    }
    class Grass : Objects
    {
        public Grass(int x,int y)
        {
            this.ShowLevel = 1;
            this.x = x;
            this.y = y;
        }
    }
    class Door : Objects
    {
        public Door(int x,int y)
        {
            this.x = x;
            this.y = y;
            this.ShowLevel = 0;
        }
    }
    class SpeedBuff : Objects
    {
        public SpeedBuff(int x,int y)
        {
            this.x = x;
            this.y = y;
            this.ShowLevel = 0;
        }
    }
    class DamageBuff : Objects
    {
        public DamageBuff(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.ShowLevel = 0;
        }
    }
    class Monster : Objects
    {
        public Monster(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.ShowLevel = 1;
        }
        public void AIMove(int randomchoice, Dictionary<int, Objects> objectsdict)
        {
            switch (randomchoice)
            {
                case 0:
                    {
                        int die = -1;
                        foreach (var pair in objectsdict)
                        {
                            if (pair.Value.x == (this.x) && pair.Value.y == (this.y - 1) && !(pair.Value is Character))
                                return;
                            else if (pair.Value.x == (this.x) && pair.Value.y == (this.y - 1) && pair.Value is Character)
                                die = pair.Value.id;
                        }
                        if (die!=-1)
                        {
                            objectsdict.Remove(die);
                        }
                        if (this.y > 1)
                        {
                            this.y -= 1;
                        }
                        break;
                    }
                case 1:
                    {
                        int die = -1;
                        foreach (var pair in objectsdict)
                        {
                            if (pair.Value.y == (this.y) && pair.Value.x == (this.x + 1) && !(pair.Value is Character))
                                return;
                            else if (pair.Value.x == (this.x+1) && pair.Value.y == this.y && pair.Value is Character)
                                die = pair.Value.id;
                        }
                        if (die != -1)
                        {
                            objectsdict.Remove(die);
                        }
                        if (this.x < 18)
                        {
                            this.x += 1;
                        }
                        break;
                    }
                case 2:
                    {
                        int die = -1;
                        foreach (var pair in objectsdict)
                        {
                            if (pair.Value.x == (this.x) && pair.Value.y == (this.y + 1) && !(pair.Value is Character))
                                return;
                            else if (pair.Value.x == (this.x ) && pair.Value.y == (this.y+1)&&pair.Value is Character)
                                die = pair.Value.id;
                        }
                        if (die != -1)
                        {
                            objectsdict.Remove(die);
                        }
                        if (this.y < 48)
                        {
                            this.y += 1;
                        }
                        break;
                    }
                case 3:
                    {
                        int die = -1;
                        foreach (var pair in objectsdict)
                        {
                            if (pair.Value.y == (this.y) && pair.Value.x == (this.x - 1) && !(pair.Value is Character))
                                return;
                            else if (pair.Value.x == (this.x-1) && pair.Value.y == (this.y) && pair.Value is Character)
                                die = pair.Value.id;
                        }
                        if (die != -1)
                        {
                            objectsdict.Remove(die);
                        }
                        if (this.x > 1)
                        {
                            this.x -= 1;
                        }
                        break;
                    }
                default:
                    return;
            }
        }
    }
    class Character : Objects
    {
        public int damage;
        public int speed = 0;
        public int win;
        public Character(int x, int y)
        {
            this.win = 0;
            this.damage = 2;
            this.id = 0;
            this.x = x;
            this.y = y;
            this.ShowLevel = 2;
        }
        public void CharacterMove(ObjectManager objectmanager)
        {
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.A)
            {
                int id = -1;
                int win = 0;
                int count = 0;
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value.x == (this.x) && pair.Value.y == (this.y - 1))
                    {
                        if (!(pair.Value is SpeedBuff||pair.Value is DamageBuff || pair.Value is Door))
                        {
                            return;
                        }
                        else
                        {
                            id = pair.Value.id;
                            if (pair.Value is SpeedBuff)
                            {
                                this.speed += 1;
                            }
                            else if (pair.Value is DamageBuff)
                            {
                                this.damage += 2;
                            }
                            else
                            {
                                win +=1;
                            }
                        }
                    }
                }
                if (objectmanager.objectdict.ContainsKey(id)&&!(objectmanager.objectdict[id] is Door))
                objectmanager.RemoveObjectByid(id);
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value is Monster)
                    {
                        count += 1;
                    }
                }
                if (count == 0 && win == 1)
                {
                    this.win = 1;
                }
                if (this.y > 1)
                {
                    this.y -= 1;
                    return;
                }
                else
                    return;
            }
            if (cki.Key == ConsoleKey.S)
            {
                int id = -1;
                int win = 0;
                int count = 0;
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value.y == (this.y) && pair.Value.x == (this.x + 1))
                    {
                        if (!(pair.Value is SpeedBuff || pair.Value is DamageBuff || pair.Value is Door))
                        {
                            return;
                        }
                        else
                        {
                            id = pair.Value.id;
                            if (pair.Value is SpeedBuff)
                            {
                                this.speed += 1;
                            }
                            else if (pair.Value is DamageBuff)
                            {
                                this.damage += 2;
                            }
                            else
                            {
                                win += 1;
                            }
                        }
                    }

                }
                if (objectmanager.objectdict.ContainsKey(id)&&!(objectmanager.objectdict[id] is Door))
                    objectmanager.RemoveObjectByid(id);
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value is Monster)
                    {
                        count += 1;
                    }
                }
                if (count == 0 && win == 1)
                {
                    this.win = 1;
                }
                if (this.x < 18)
                {
                    this.x += 1;
                    return;
                }
                else
                    return;
            }
            if (cki.Key == ConsoleKey.D)
            {
                int id = -1;
                int win = 0;
                int count = 0;
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value.x == (this.x) && pair.Value.y == (this.y + 1))
                    {
                        if (!(pair.Value is SpeedBuff || pair.Value is DamageBuff || pair.Value is Door))
                        {
                            return;
                        }
                        else
                        {
                            id = pair.Value.id;
                            if (pair.Value is SpeedBuff)
                            {
                                this.speed += 1;
                            }
                            else if (pair.Value is DamageBuff)
                            {
                                this.damage += 2;
                            }
                            else
                            {
                                win +=1;
                            }
                        }
                    }
                }
                if (objectmanager.objectdict.ContainsKey(id) && !(objectmanager.objectdict[id] is Door))
                    objectmanager.RemoveObjectByid(id);
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value is Monster)
                    {
                        count += 1;
                    }
                }
                if (count == 0 && win == 1)
                {
                    this.win = 1;
                }
                if (this.y < 48)
                {
                    this.y += 1;
                    return;
                }
                else
                    return;
            }
            if (cki.Key == ConsoleKey.W)
            {
                int id = -1;
                int win = 0;
                int count = 0;
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value.y == (this.y) && pair.Value.x == (this.x - 1))
                    {
                        if (!(pair.Value is SpeedBuff || pair.Value is DamageBuff || pair.Value is Door))
                        {
                            return;
                        }
                        else
                        {
                            id = pair.Value.id;
                            if (pair.Value is SpeedBuff)
                            {
                                this.speed += 1;
                            }
                            else if (pair.Value is DamageBuff)
                            {
                                this.damage += 2;
                            }
                            else
                            {
                                win+=1;
                            }
                        }
                    }
                }
                if (objectmanager.objectdict.ContainsKey(id) && !(objectmanager.objectdict[id] is Door))
                    objectmanager.RemoveObjectByid(id);
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value is Monster)
                    {
                        count += 1;
                    }
                }
                if (count == 0 && win == 1)
                {
                    this.win = 1;
                }
                if (this.x > 1)
                {
                    this.x -= 1;
                    return;
                }
                else
                    return;
            }
            if (cki.Key == ConsoleKey.Spacebar)
            {
                if (objectmanager.objectdict.ContainsKey(0))
                objectmanager.AddObject(new Boom(this.x, this.y));
            }
            return;
        }
    }
    class Program
    {
        static Random random = new Random();
        static ObjectManager objectmanager = new ObjectManager();
        static Character character = new Character(5, 5);
        static void MapRead(string s,StreamReader read,ObjectManager objectmanager)
        {
            int x = 2;
            while ((s = read.ReadLine()) != null)
            {
                for (int y = 2; y < s.Length; y++)
                {
                    if (s[y] == '#')
                    {
                        Brick brick = new Brick(x, y);
                        objectmanager.AddObject(brick);
                    }
                    else if (s[y] == 'W')
                    {
                        Grass grass = new Grass(x, y);
                        objectmanager.AddObject(grass);
                    }
                    else if (s[y] == 'V')
                    {
                        Grass grass = new Grass(x, y);
                        objectmanager.AddObject(grass);
                        Door door = new Door(x, y);
                        objectmanager.AddObject(door);
                    }
                    else if (s[y] == 'K')
                    {
                        Grass grass = new Grass(x, y);
                        objectmanager.AddObject(grass);
                        SpeedBuff speedBuff = new SpeedBuff(x, y);
                        objectmanager.AddObject(speedBuff);
                    }
                    else if (s[y] == 'D')
                    {
                        Grass grass = new Grass(x, y);
                        objectmanager.AddObject(grass);
                        DamageBuff damageBuff = new DamageBuff(x, y);
                        objectmanager.AddObject(damageBuff);
                    }
                }
                x += 1;
            }
        }
        static bool CharacterDie()
        {
            if (!objectmanager.objectdict.ContainsKey(0))
                return true;
            else
                return false;
        }
        static void LoadMap()
        {
            FileStream fileStream = new FileStream("Map1.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fileStream, Encoding.Default);
            string s = null;
            MapRead(s, read, objectmanager);
        }
        static void MapRefresh()
        {
            Console.Clear();
            Map map = new Map(objectmanager);
            map.ShowMap(objectmanager);
        }
        static void BufferRecord(ObjectManager objectmanager,Dictionary<int,int> tempdict)
        {
            foreach (var pair in objectmanager.objectdict)
            {
                tempdict.Add(pair.Key, pair.Value.x * 49 + pair.Value.y);
            }
        }
        static void BufferShow(ObjectManager objectmanager, Dictionary<int, int> tempdict)
        {
            int moved = 0;
            int xold=0;
            int yold=0;
            int xnew = 0;
            int ynew = 0;
            int type = 0;
            int Level0Victor = 0;
            int Level0Speed = 0;
            int Level0Damage = 0;
            int Level1Monster =0;
            int Level1Brick =0;
            int Level1Grass = 0;
            int Level2 = 0;
            int Level3 = 0;
            foreach (var pair1 in objectmanager.objectdict)
            {
                foreach (var pair2 in tempdict)
                {
                    if (pair2.Key==pair1.Key)
                    {
                            if (!((pair2.Value / 49) == pair1.Value.x && (pair2.Value % 49) == pair1.Value.y))
                            {
                                moved = 1;
                            xold = (pair2.Value / 49);
                            yold = (pair2.Value % 49);
                            xnew = pair1.Value.x;
                            ynew = pair1.Value.y;
                            if (pair1.Value is Character)
                            {
                                type = 1;
                            }
                            else if (pair1.Value is Monster)
                            {
                                type = 2;
                            }
                                foreach (var pair3 in objectmanager.objectdict)
                                {
                                   if (pair3.Value.x==xold&& pair3.Value.y == yold)
                                   {
                                       if (pair3.Value.ShowLevel == 0&&pair3.Value is Door)
                                       {
                                          Level0Victor+= 1;
                                       }
                                       else if (pair3.Value.ShowLevel == 0 && pair3.Value is SpeedBuff)
                                       {
                                          Level0Speed += 1;
                                       }
                                       else if (pair3.Value.ShowLevel == 0 && pair3.Value is DamageBuff)
                                       {
                                          Level0Damage += 1;
                                       }
                                       if (pair3.Value.ShowLevel == 1&& pair3.Value is Monster)
                                       {
                                            Level1Monster += 1;
                                       }
                                       else if (pair3.Value.ShowLevel == 1&&pair3.Value is Brick)
                                       {
                                            Level1Brick+=1;
                                       }
                                        else if (pair3.Value.ShowLevel == 1 && pair3.Value is Grass)
                                       {
                                            Level1Grass += 1;
                                       }
                                       if (pair3.Value.ShowLevel == 2)
                                       {
                                          Level2 += 1;
                                       }
                                       if (pair3.Value.ShowLevel == 3)
                                       {
                                          Level3 += 1;
                                       }
                                   }
                                }
                            }
                    }
                }
            }
            if (moved == 1)
            {
                Console.SetCursorPosition(2*yold, xold);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("口");
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int i = 0; i < Level0Victor; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Π");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level0Speed; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("ξ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level0Damage; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Ω");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level1Monster; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("●");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level1Brick; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("■");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level1Grass; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("□");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level2; i++)
                {
                    Console.SetCursorPosition(yold, xold);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("★");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < Level3; i++)
                {
                    Console.SetCursorPosition(2*yold, xold);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("⊙");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.SetCursorPosition(2*ynew,xnew);
                switch (type)
                {
                    case 1:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("★");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        }
                    case 2:
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("●");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        static void RefreshPoint(int x,int y,ObjectManager objectmanager)
        {
            if (x > 18 || x < 1 || y > 48 || y < 0)
                return;
            int Level0Victor = 0;
            int Level0Speed = 0;
            int Level0Damage = 0;
            int Level1Monster = 0;
            int Level1Brick = 0;
            int Level1Grass = 0;
            int Level2 = 0;
            int Level3 = 0;
            foreach (var pair in objectmanager.objectdict)
            {
                if (pair.Value.x == x && pair.Value.y == y)
                {
                    if (pair.Value.ShowLevel == 0 && pair.Value is Door)
                    {
                        Level0Victor += 1;
                    }
                    else if (pair.Value.ShowLevel == 0 && pair.Value is SpeedBuff)
                    {
                        Level0Speed += 1;
                    }
                    else if (pair.Value.ShowLevel == 0 && pair.Value is DamageBuff)
                    {
                        Level0Damage += 1;
                    }
                    if (pair.Value.ShowLevel == 1 && pair.Value is Monster)
                    {
                        Level1Monster += 1;
                    }
                    else if (pair.Value.ShowLevel == 1 && pair.Value is Brick)
                    {
                        Level1Brick += 1;
                    }
                    else if (pair.Value.ShowLevel == 1 && pair.Value is Grass)
                    {
                        Level1Grass += 1;
                    }
                    if (pair.Value.ShowLevel == 2)
                    {
                        Level2 += 1;
                    }
                    if (pair.Value.ShowLevel == 3)
                    {
                        Level3 += 1;
                    }
                }
            }
            Console.SetCursorPosition(2*y, x);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("口");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < Level0Victor; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Π");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level0Speed; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("ξ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level0Damage; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Ω");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level1Monster; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("●");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level1Brick; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("■");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level1Grass; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("□");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level2; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("★");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < Level3; i++)
            {
                Console.SetCursorPosition(2*y, x);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("⊙");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        static bool ExistBrick(int x, int y, ObjectManager objectmanager)
        {
            foreach(var pair in objectmanager.objectdict)
            {
                if (pair.Value.x==x&&pair.Value.y==y&&pair.Value is Brick)
                {
                    return true;
                }
            }
            return false;
        }
        static bool ExistGrass(int x, int y, ObjectManager objectmanager)
        {
            foreach (var pair in objectmanager.objectdict)
            {
                if (pair.Value.x == x && pair.Value.y == y && pair.Value is Grass)
                {
                    return true;
                }
            }
            return false;
        }
        static void KillObject(int x, int y, ObjectManager objectmanager)
        {
            if (x > 18 || x < 1 || y > 48 || y < 0)
                return;
            int id=0;
            bool kill=false;
            foreach (var pair in objectmanager.objectdict)
            {
                if (x == pair.Value.x && y == pair.Value.y)
                {
                    if (pair.Value is Character|| pair.Value is Monster||pair.Value is Grass)
                    {
                        id=pair.Value.id;
                        kill = true;
                    }
                }
            }
            if (kill)
            {
                objectmanager.RemoveObjectByid(id);
            }
        }
        static void Boomeffect(int id,ObjectManager objectmanager,Character character)
        {
            int i = character.damage;
            int x = objectmanager.objectdict[id].x;
            int y = objectmanager.objectdict[id].y;
            Console.SetCursorPosition(2*y, x);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("※");
            Console.ForegroundColor = ConsoleColor.Gray;
            KillObject(x, y, objectmanager);
            for (int j = 1; j <=i; j++)
            {
                int t = 0;
                if (x > 18 || x < 1 || y-j > 48 || y-j < 0)
                    break;
                if (ExistBrick(x, y - j, objectmanager))
                    break;
                Console.SetCursorPosition(2*(y-j), x);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("※");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (ExistGrass(x, y - j, objectmanager))
                    t = 1;
                KillObject(x, y - j, objectmanager);
                if (t==1)
                    break;
            }
            for (int j = 1; j <= i; j++)
            {
                int t = 0;
                if (x-j> 18 || x-j< 1 || y> 48 || y< 0)
                    break;
                if (ExistBrick(x-j, y, objectmanager))
                    break;
                Console.SetCursorPosition(2*y , x-j);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("※");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (ExistGrass(x-j, y, objectmanager))
                    t = 1;
                KillObject(x-j, y, objectmanager);
                if (t == 1)
                    break;
            }
            for (int j = 1; j <= i; j++)
            {
                int t = 0;
                if (x> 18 || x < 1 || y+j> 48 || y+j< 0)
                    break;
                if (ExistBrick(x, y+j, objectmanager))
                    break;
                Console.SetCursorPosition(2*(y +j), x);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("※");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (ExistGrass(x, y+j, objectmanager))
                    t = 1;
                KillObject(x, y+j, objectmanager);
                if (t == 1)
                    break;
            }
            for (int j = 1; j <= i; j++)
            {
                int t = 0;
                if (x+j> 18 || x+j< 1 || y> 48 || y< 0)
                    break;
                if (ExistBrick(x+j, y, objectmanager))
                    break;
                Console.SetCursorPosition(2*y , x+j);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("※");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (ExistGrass(x+j, y, objectmanager))
                    t = 1;
                KillObject(x+j, y, objectmanager);
                if (t == 1)
                    break;
            }
        }
        static void Boomharm(int id, ObjectManager objectmanager, Character character)
        {
            int i = character.damage;
            int x = objectmanager.objectdict[id].x;
            int y = objectmanager.objectdict[id].y;
            objectmanager.RemoveObjectByid(id);
            RefreshPoint(x, y, objectmanager);
            for (int j = 1; j <= i; j++)
                RefreshPoint(x, y - j, objectmanager);
            for (int j = 1; j <= i; j++)
                RefreshPoint(x - j, y, objectmanager);
            for (int j = 1; j <= i; j++)
                RefreshPoint(x, y + j, objectmanager);
            for (int j = 1; j <= i; j++)
                RefreshPoint(x + j, y, objectmanager);
        }
        static void Main(string[] args)
        {
            int time = 999;
            Console.CursorVisible = false;
            LoadMap();
            objectmanager.objectdict[0] = character;
            Monster MonsterA = new Monster(15, 11);
            objectmanager.AddObject(MonsterA);
            Monster MonsterB = new Monster(1, 8);
            objectmanager.AddObject(MonsterB);
            Monster MonsterC = new Monster(9, 27);
            objectmanager.AddObject(MonsterC);
            Monster MonsterD = new Monster(3, 38);
            objectmanager.AddObject(MonsterD);
            Monster MonsterE = new Monster(15, 19);
            objectmanager.AddObject(MonsterE);
            Monster MonsterF = new Monster(17, 39);
            objectmanager.AddObject(MonsterF);
            Monster MonsterG = new Monster(17, 45);
            objectmanager.AddObject(MonsterG);
            MapRefresh();
            while (true)
            {
                Console.SetCursorPosition(0, 20);
                Console.Write("剩余时间: " + time);
                time -= 1;
                int max = 0;
                List<int> boomed = new List<int>();
                foreach (var pair in objectmanager.objectdict)
                {
                    if (pair.Value is Boom)
                    {
                        if (pair.Value.ExistTime > 0)
                        {
                            pair.Value.ExistTime -= 1;
                        }
                        else if (pair.Value.ExistTime == 0)
                        {
                            boomed.Add(pair.Value.id);
                        }
                    }
                }
                for (int i = 0; i < boomed.Count; i++)
                {
                    Boomeffect(boomed[i], objectmanager,character);
                }
                while (Console.KeyAvailable)
                {
                    max++;
                    Dictionary<int, int> tempdict = new Dictionary<int, int>();
                    BufferRecord(objectmanager, tempdict);
                    character.CharacterMove(objectmanager);
                    BufferShow(objectmanager, tempdict);
                    if (max > character.speed)
                    {
                        while (Console.KeyAvailable)
                        {
                            Console.ReadKey(true);
                        }
                        break;
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterA.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterA.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterB.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterB.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterC.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterC.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterD.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterD.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterE.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterE.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterF.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterF.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                if (random.Next(0, 51) > 25)
                {
                    if (objectmanager.objectdict.ContainsKey(MonsterG.id))
                    {
                        Dictionary<int, int> tempdict2 = new Dictionary<int, int>();
                        BufferRecord(objectmanager, tempdict2);
                        MonsterG.AIMove(random.Next(0, 4), objectmanager.objectdict);
                        BufferShow(objectmanager, tempdict2);
                    }
                }
                System.Threading.Thread.Sleep(100);
                for (int i = 0; i < boomed.Count; i++)
                {
                    Boomharm(boomed[i], objectmanager, character);
                }
                if (CharacterDie()||time<=0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(8,4);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("你死了傻逼!");
                    break;
                }
                if (character.win==1)
                {
                    Console.SetCursorPosition(8, 4);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("你赢了傻逼!");
                    break;
                }
            }
            Console.SetCursorPosition(8,5);
            Console.Write("游戏结束.");
            Console.ReadKey();
        }
    }
}
