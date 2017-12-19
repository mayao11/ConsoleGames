using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PlayerType
{
    player,
    enemy
}


namespace 放置江湖.Base
{
    /// <summary>
    /// 人物类
    /// </summary>
     class Character
    {
        public static Random random = null;

        Global.Global global = new Global.Global();

        public List<Skill> skills = new List<Skill>();

        public List<State> states = new List<State>();

        public List<Item> itemList = new List<Item>();

        public List<Item> equitList = new List<Item>();

        /// <summary>
        /// 玩家类型
        /// </summary>
        public PlayerType playertype;
        /// <summary>
        /// 名字
        /// </summary>
        public string name;
        /// <summary>
        /// 当前生命
        /// </summary>
        public int curHp;
        public int maxHp;
        /// <summary>
        /// 当前血量
        /// </summary>
        public int curMp;
        public int maxMp;
        /// <summary>
        /// 当前经验
        /// </summary>
        public int curExp;

        /// <summary>
        /// 升级经验
        /// </summary>
        public int maxExp;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int curLevel;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int atk;
        /// <summary>
        /// 防御力
        /// </summary>
        public int def;

        /// <summary>
        /// 提供经验
        /// </summary>
        public int proExp = 0;

        public Character()
        {
           
        }

        public List<Skill> GetSkills()
        {
            return skills;
        }

        public void AddState(State state)
        {
            states.Add(state);
        }

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public Skill GetSkill(int skill_index)
        {
            Skill skill = skills[skill_index];
            return skill;
        }

        public Skill RandSkill()
        {
            int r = random.Next(skills.Count);
            return GetSkill(r);
        }


        public Character IniPlayer(string name,int maxHp,int maxMp,int atk,int def,int level)
        {
            Character player = new Character();
            player.name = name;
            player.maxHp = maxHp;
            player.curHp = player.maxHp;
            player.maxMp = maxMp;
            player.curMp = maxMp;
            player.atk = atk;
            player.def = def;
            player.curLevel = level;
            player.curExp = 0;
            player.maxExp = 1000;
            Debug.WriteLine(maxExp);
            player.playertype = PlayerType.player;
            return player;
        }

        public Character IniMonster(string name,int hp,int atk,int def,int proExp)
        {
            Character monster = new Character();
            monster.name = name;
            monster.curHp = hp;
            monster.maxHp = hp;
            monster.atk = atk;
            monster.def = def;
            monster.proExp = proExp;
            monster.curMp = 100000;
            monster.playertype = PlayerType.enemy;
            return monster;
            
        }
        
        /// <summary>
        /// 降低血量
        /// </summary>
        /// <param name="n"></param>
        public virtual void CostHp(int n)
        {
            curHp -= n;
            if (curHp<0)
            {
                curHp = 0;
            }
        }

        /// <summary>
        /// 加血
        /// </summary>
        /// <param name="n"></param>
        public virtual void HealHp(int n)
        {
            curHp += n;
            if (curHp>maxHp)
            {
                curHp = maxHp;
            }
        }

        /// <summary>
        /// 加魔法
        /// </summary>
        /// <param name="n"></param>
        public virtual void HealMp(int n)
        {
            curMp += n;
            if (curMp > maxMp)
            {
                curMp = maxMp;
            }
        }

        /// <summary>
        /// 魔法计算
        /// </summary>
        /// <param name="n"></param>
        public virtual void CostMp(int n)
        {
            curMp -= n;
            if (curMp<=0)
            {
                curMp = 0;
            }
        }

        /// <summary>
        /// 经验计算
        /// </summary>
        /// <param name="n"></param>
        public virtual void CostExp(int n)
        {
            curExp += n;
            if (curExp>= global.maxExp[curLevel-1])
            {
                curExp = 0;
                if (curLevel<= global.maxLevel)
                {
                    LevelUp();
                }
            }
        }

        /// <summary>
        /// 人物升级
        /// </summary>
        public virtual void LevelUp()
        {
            curLevel++;
            atk += 10;
            def += 10;
            curExp = 0;
            maxHp += 100;
            maxMp += 100;
            maxExp = global.maxExp[curLevel];
        }

        /// <summary>
        /// 重置属性
        /// </summary>
        public void ResetAttribute()
        {
            curHp = maxHp;
            curMp = maxMp;

            
        }

        /// <summary>
        /// 穿上装备
        /// </summary>
        /// <returns></returns>
        public bool PutOnEquip(Item item)
        {
            if (item.itemType==ItemType.Equip)
            {
                switch (item.equipType)
                {
                    case EquipType.Head:

                        break;
                    case EquipType.Body:
                        break;
                    case EquipType.Hand:
                        break;
                    case EquipType.Shoes:
                        break;
                    default:
                        break;
                }

            }
            return false;
        }

        public bool PutOffEquip(Item item)
        {
            if (item.itemType == ItemType.Equip)
            {
                switch (item.equipType)
                {
                    case EquipType.Head:

                        break;
                    case EquipType.Body:
                        break;
                    case EquipType.Hand:
                        break;
                    case EquipType.Shoes:
                        break;
                    default:
                        break;
                }

            }
            return false;
        }
    }


}
