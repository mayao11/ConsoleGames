using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    static class DataManager
    {
        //创建对应类型的技能的方法
        /// <summary>
        /// 创建普通攻击的方法，不包含计算公式
        /// </summary>
        /// <param name="hitTimes">普通攻击执行的次数</param>
        /// <returns>返回普通攻击的Skill</returns>
        public static Skill CreateAttackSkill(int hitTimes, int dispersion, DamageType damageType, TargetType targetType)
        {
            Skill skill = new Skill();
            skill.name = "攻击";
            skill.hpCost = 0;
            skill.mpCost = 0;
            skill.hitTimes = hitTimes;
            skill.dispersion = dispersion;
            skill.type = SkillType.NormalAttack;
            skill.damageType = damageType;
            skill.targetType = targetType;
            return skill;
        }

        /// <summary>
        /// 创建一个施加状态的技能，不包含实际的buff实现方法
        /// </summary>
        /// <param name="name">技能名称</param>
        /// <param name="hpCost">技能消耗的hp</param>
        /// <param name="mpCost">技能消耗的mp</param>
        /// <param name="type">施加的buff的类型</param>
        /// <returns></returns>
        public static Skill CreateStateSkill(string name, int hpCost, int mpCost, TargetType type)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.hpCost = hpCost;
            skill.mpCost = mpCost;
            skill.type = SkillType.State;
            skill.targetType = type;
            return skill;
        }

        /// <summary>
        /// 创建一个伤害技能
        /// </summary>
        /// <param name="name">技能名字</param>
        /// <param name="hpCost">消耗HP</param>
        /// <param name="mpCost">消耗MP</param>
        /// <param name="damage">伤害值，不为0的时候会覆盖掉effect里的伤害公式</param>
        /// <param name="hitTimes">攻击次数</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="damageType">伤害类型</param>
        /// <returns></returns>
        public static Skill CreateDamageSkill(string name, int hpCost, int mpCost, int hitTimes, int dispersion, TargetType targetType, DamageType damageType)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.hpCost = hpCost;
            skill.mpCost = mpCost;
            skill.hitTimes = hitTimes;
            skill.dispersion = dispersion;
            skill.targetType = targetType;
            skill.damageType = damageType;
            skill.type = SkillType.Damage;
            return skill;
        }

        /// <summary>
        /// 创建一个恢复技能
        /// </summary>
        /// <param name="name">技能名称</param>
        /// <param name="hpCost">技能消耗的HP</param>
        /// <param name="mpCost">技能消耗的MP</param>
        /// <param name="heal">技能的恢复量，不为零的时候会覆盖后面的恢复量公式</param>
        /// <param name="hitTimes">技能生效的次数</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        public static Skill CreateHealSkill(string name, int hpCost, int mpCost, int hitTimes, int dispersion, TargetType targetType)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.hpCost = hpCost;
            skill.mpCost = mpCost;
            skill.hitTimes = hitTimes;
            skill.dispersion = dispersion;
            skill.targetType = targetType;
            skill.type = SkillType.Heal;
            return skill;
        }

        //创建对应类型状态的方法
        /// <summary>
        /// 创建一个buff状态
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="times">次数</param>
        /// <param name="counts">持续回合</param>
        /// <returns></returns>
        public static State CreateBuffState(string name, int times, int counts)
        {
            State state = new State();
            state.name = name;
            state.times = times;
            state.counts = counts;
            state.type = StateType.Buff;
            return state;
        }

        /// <summary>
        /// 创建一个Debuff状态
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="times">次数</param>
        /// <param name="counts">持续回合</param>
        /// <returns></returns>
        public static State CreateDebuffState(string name, int times, int counts)
        {
            State state = new State();
            state.name = name;
            state.times = times;
            state.counts = counts;
            state.type = StateType.Debuff;
            return state;
        }

        /// <summary>
        /// 创建一个DOT类型的状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="times"></param>
        /// <param name="counts"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        public static State CreateDOTState(string name, int times, int counts, int damage)
        {
            State state = new State();
            state.name = name;
            state.times = times;
            state.counts = counts;
            state.damage = damage;
            state.type = StateType.DamageOverTime;
            return state;
        }

        /// <summary>
        /// 创建一个HOT类型的状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="times"></param>
        /// <param name="counts"></param>
        /// <param name="hprecover"></param>
        /// <returns></returns>
        public static State CreateHOTState(string name, int times, int counts, int hprecover)
        {
            State state = new State();
            state.name = name;
            state.times = times;
            state.counts = counts;
            state.hprecover = hprecover;
            state.type = StateType.HealOverTime;
            return state;
        }

        /// <summary>
        /// 创建一个特殊状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="times"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public static State CreateSpecialState(string name, int times, int counts)
        {
            State state = new State();
            state.name = name;
            state.times = times;
            state.counts = counts;
            return state;
        }

        /// <summary>
        /// 创建一个伤害提高或减少的状态
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="times">伤害加深的次数</param>
        /// <param name="counts">伤害加深的回合数</param>
        /// <param name="ratio">伤害加深的倍率</param>
        /// <returns></returns>
        public static State CreateIncOrDecState(string name, int times, int counts, double ratio)
        {
            State state = new State();
            state.name = name;
            state.times = times;
            state.counts = counts;
            state.ratio = ratio;
            return state;
        }

        /// <summary>
        /// 创建角色五月，战士
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharSatsuki()
        {
            //角色的基本属性
            BaseCharacter satsuki = new BaseCharacter();
            satsuki.name = "五月";
            satsuki.maxHp = 421;
            satsuki.maxMp = 196;
            satsuki.Hp = 421;
            satsuki.Mp = 196;
            satsuki.atk = 42;
            satsuki.def = 23;
            satsuki.mat = 17;
            satsuki.men = 18;
            satsuki.hit = 95;
            satsuki.crt = 10;
            satsuki.level = 5;
            satsuki.type = CharacterType.Hero;

            //五月的技能
            //添加普通攻击
            Skill normalAttack = CreateAttackSkill(1, 10, DamageType.Physical, TargetType.EnemySingle);
            normalAttack.description = "普通攻击，物理伤害。";
            normalAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = attacker.atk * 2 - target.def;
                return temp > 0 ? temp : 1;
            };
            satsuki.skill.Add(normalAttack);

            //添加技能“蓄力”
            Skill charge = CreateStateSkill("蓄力", 0, 30, TargetType.Self);
            charge.description = "积蓄力量，使得下一次的物理攻击伤害翻倍。（MP:30）";
            charge.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State chargeState = CreateIncOrDecState("伤害提高", 1, 0, 2.0);
                chargeState.type = StateType.PhysicalDamageIncrease;
                chargeState.skillName = charge.name;
                attacker.AddState(chargeState);
            };
            satsuki.skill.Add(charge);

            //添加技能“狂化”
            Skill berserker = CreateStateSkill("狂化", 0, 20, TargetType.Self);
            berserker.description = "引发自己的吸血冲动，在3回合内增加物理攻击力，降低物理防御力。（MP：20）";
            berserker.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State berserkerBuff = CreateBuffState("物理攻击力上升", 0, 4);
                berserkerBuff.skillName = berserker.name;
                berserkerBuff.AddState = (BaseCharacter a) =>
                {
                    berserkerBuff.atkUp = a.atk / 2;
                    a.atk = a.atk + berserkerBuff.atkUp;
                };
                berserkerBuff.RemoveState = (BaseCharacter a) =>
                {
                    a.atk = a.atk - berserkerBuff.atkUp;
                };
                attacker.AddState(berserkerBuff);
                State berserkerDebuff = CreateBuffState("物理防御力下降", 0, 4);
                berserkerDebuff.skillName = berserker.name;
                berserkerDebuff.AddState = (BaseCharacter a) =>
                {
                    berserkerDebuff.defUp =  0 - a.def / 2;
                    a.def = a.def + berserkerDebuff.defUp;
                };
                berserkerDebuff.RemoveState = (BaseCharacter a) =>
                {
                    a.def = a.def - berserkerDebuff.defUp;
                };
                attacker.AddState(berserkerDebuff);
            };
            satsuki.skill.Add(berserker);

            //添加技能“BAKA杀”
            Skill bakaKill = CreateDamageSkill("八嘎杀", 0, 30, 3, 20, TargetType.EnemySingle, DamageType.Physical);
            bakaKill.description = "高速的连续攻击，对单个敌人造成3次小幅度的物理伤害。（MP：30）";

            bakaKill.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = (attacker.atk * 2 - target.def) * 5 / 10;
                return temp > 0 ? temp : 1;
            };
            satsuki.skill.Add(bakaKill);

            //添加技能“枯渴庭院”
            Skill drynessGarden = CreateDamageSkill("枯渴庭院", 0, 80, 1, 10, TargetType.EnemyMulti, DamageType.Physical);
            drynessGarden.description = "迅速的抽空大气中的魔力，对敌方全体造成伤害并且减少魔法值。（MP：80）";
            drynessGarden.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                target.Mp = target.Mp - attacker.atk / 2;
                return attacker.atk * 3 - target.def * 2;
            };
            satsuki.skill.Add(drynessGarden);
            return satsuki;
        }

        /// <summary>
        /// 创建角色艾露露，治疗
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharEruruu()
        {
            BaseCharacter eruruu = new BaseCharacter();
            eruruu.name = "艾露露";
            eruruu.maxHp = 288;
            eruruu.maxMp = 321;
            eruruu.Hp = 288;
            eruruu.Mp = 321;
            eruruu.atk = 0;
            eruruu.def = 17;
            eruruu.mat = 23;
            eruruu.men = 27;
            eruruu.hit = 95;
            eruruu.crt = 10;
            eruruu.level = 5;
            eruruu.type = CharacterType.Hero;

            Skill herb = CreateHealSkill("草药", 0, 15, 1, 10, TargetType.PartySingle);
            herb.description = "艾露露自制的草药，恢复己方单体生命值。（MP：15）";
            herb.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                return attacker.men * 2;
            };
            eruruu.skill.Add(herb);

            Skill rest = CreateStateSkill("摩洛洛粥", 0, 0, TargetType.Self);
            rest.description = "艾露露为自己准备的家乡的小食，恢复自身的MP值。";
            rest.skillEffect = (BaseCharacter attacker, BaseCharacter target) => 
            {
                int temp = Convert.ToInt32(attacker.men * 1.34);
                if (attacker.Mp + temp > attacker.maxMp)
                    temp = attacker.maxMp - attacker.Mp;
                attacker.Mp += temp;
                MyDraw.DrawCharacterInfo(eruruu, 1);
                MyDraw.DrawBattleMessageDelay(string.Format("艾露露恢复了{0}点魔法值", temp));
            };
            eruruu.skill.Add(rest);

            Skill lilac = CreateHealSkill("花语", 0, 40, 1, 0, TargetType.PartyMulti);
            lilac.description = "艾露露利用山野中的鲜花特质的线香，为己方全体施加生命恢复效果。（MP：40）";
            lilac.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int tempRecover = Convert.ToInt32(attacker.men * 3.0 / 2);
                State lilacState = CreateHOTState("花语", 0, 3, tempRecover);
                lilacState.skillName = lilac.name;
                target.AddState(lilacState);
            };
            eruruu.skill.Add(lilac);

            Skill mandara = CreateDamageSkill("毒雾", 0, 30, 1, 0, TargetType.EnemyMulti, DamageType.Magic);
            mandara.description = "艾露露利用山中的毒草制成的迷雾攻击敌方全体，一定概率使得敌方中毒。（MP：30）";
            mandara.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int tempRecover = Convert.ToInt32(target.maxHp * 0.07);
                if (Program.random.Next(0, 100) > 70)
                {
                    State mandaraState = CreateDOTState("中毒", 0, 3, tempRecover);
                    mandaraState.skillName = mandara.name;
                    target.AddState(mandaraState);
                }
            };
            eruruu.skill.Add(mandara);

            Skill callBack = CreateHealSkill("复活", 0, 50, 1, 10, TargetType.PartySingle);
            callBack.description = "艾露露利用流传下来的秘药，复活一名已经阵亡的队友。（MP：50）";
            callBack.canDeath = true;
            callBack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                return Convert.ToInt32(target.maxHp * 0.3);
            };
            eruruu.skill.Add(callBack);

            Skill lifeLoop = CreateHealSkill("子守歌", 0, 60, 1, 20, TargetType.PartyMulti);
            lifeLoop.description = "艾露露唱起小时候听过的子守歌，恢复己方全体的生命值。（MP：60）";
            lifeLoop.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                return Convert.ToInt32(attacker.mat * 2.3 + 16);
            };
            eruruu.skill.Add(lifeLoop);

            return eruruu;
        }

        /// <summary>
        /// 创建角色玲，魔法师
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharRenne()
        {
            BaseCharacter renne = new BaseCharacter();
            renne.name = "玲";
            renne.maxHp = 267;
            renne.maxMp = 352;
            renne.Hp = 267;
            renne.Mp = 352;
            renne.atk = 10;
            renne.def = 14;
            renne.mat = 32;
            renne.men = 23;
            renne.hit = 90;
            renne.crt = 5;
            renne.level = 5;
            renne.type = CharacterType.Hero;

            Skill normalAttack = CreateAttackSkill(1, 10, DamageType.Magic, TargetType.EnemySingle);
            normalAttack.description = "普通攻击，魔法伤害。";
            normalAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = attacker.mat * 2 - target.men;
                return temp > 0 ? temp : 1;
            };
            renne.skill.Add(normalAttack);

            Skill stone = CreateStateSkill("石化光线", 0, 20, TargetType.EnemySingle);
            stone.description = "召唤帕蒂尔·玛蒂尔释放石化光线，有70%的概率使敌人进入石化状态3回合。（MP：20）";
            stone.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                if (Program.random.Next(0, 100) > 70)
                {
                    return;
                }
                MyDraw.DrawBattleMessageDelay("触发了石化效果！");
                State dizzy = CreateSpecialState("石化", 0, 3);
                dizzy.type = StateType.Dizzy;
                dizzy.skillName = stone.name;
                target.AddState(dizzy);
                State damageDec = CreateIncOrDecState("物理抗性提升", 0, 3, -0.5);
                damageDec.type = StateType.PhysicalBeDamageIncrease;
                damageDec.skillName = stone.name;
                target.AddState(damageDec);
            };
            renne.skill.Add(stone);

            Skill renneKill = CreateDamageSkill("玲·歼灭", 20, 40, 1, 15, TargetType.EnemyMulti, DamageType.Magic);
            renneKill.description = "玲挥动收割敌人的生命，对敌方全体造成伤害并且低概率即死。（HP:20，MP：40）";
            renneKill.isDeathNow = true;
            renneKill.deathRatio = 15;
            renneKill.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = Convert.ToInt32(attacker.mat * 1.67);
                return temp;
            };
            renne.skill.Add(renneKill);

            Skill silence = CreateDamageSkill("天国之门", 0, 32, 1, 13, TargetType.EnemySingle, DamageType.Magic);
            silence.description = "开启天国之门，对单个敌人造成魔法伤害并且必定沉默。（MP：32）";
            silence.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = Convert.ToInt32(attacker.mat * 2.44 - target.men * 0.83);
                return temp;
            };
            silence.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State silenceState = CreateSpecialState("沉默", 0, 3);
                silenceState.skillName = silence.name;
                silenceState.type = StateType.Silence;
                target.AddState(silenceState);
            };
            renne.skill.Add(silence);

            Skill PatelMattel = CreateDamageSkill("帕蒂尔·玛蒂尔", 0, 83, 3, 10, TargetType.EnemyMulti, DamageType.Magic);
            PatelMattel.description = "帕蒂尔·玛蒂尔用加农炮轰击前方的全体敌人，血量越低伤害越高。（MP：83）";
            PatelMattel.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = Convert.ToInt32((attacker.mat * 2.5 - target.men * 0.6) * (2 - target.Hp * 1.0 / target.maxHp));
                return temp;
            };
            renne.skill.Add(PatelMattel);

            return renne;
        }

        /// <summary>
        /// 创建角色马修，骑士
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharMatthew()
        {
            BaseCharacter matthew = new BaseCharacter();
            matthew.name = "马修";
            matthew.maxHp = 631;
            matthew.maxMp = 214;
            matthew.Hp = 631;
            matthew.Mp = 214;
            matthew.atk = 19;
            matthew.def = 42;
            matthew.mat = 17;
            matthew.men = 23;
            matthew.hit = 80;
            matthew.crt = 10;
            matthew.level = 5;
            matthew.type = CharacterType.Hero;

            Skill normalAttack = CreateAttackSkill(1, 10, DamageType.Physical, TargetType.EnemySingle);
            normalAttack.description = "普通攻击，物理伤害。";
            normalAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = attacker.atk * 2 - target.def;
                return temp > 0 ? temp : 1;
            };
            matthew.skill.Add(normalAttack);

            Skill invincible = CreateStateSkill("白垩之壁", 0, 30, TargetType.PartySingle);
            invincible.description = "撑起扰乱时间的屏障，为己方单人赋予一次无敌效果。（MP：30）";
            invincible.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State invState = CreateSpecialState("无敌", 1, 0);
                invState.type = StateType.Invincible;
                invState.skillName = invincible.name;
                target.AddState(invState);
            };
            matthew.skill.Add(invincible);

            Skill defUp = CreateStateSkill("雪花之壁", 0, 28, TargetType.PartyMulti);
            defUp.description = "为己方全体撑起精神护盾，提高全体的物理防御力。（MP：28）";
            defUp.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State defupState = CreateBuffState("物理防御力上升", 0, 4);
                defupState.type = StateType.Buff;
                defupState.AddState = (BaseCharacter b) => 
                {
                    defupState.defUp = Convert.ToInt32(b.def * 0.3);
                    b.def += defupState.defUp;
                };
                defupState.RemoveState = (BaseCharacter b) =>
                {
                    b.def -= defupState.defUp;
                };
                defupState.skillName = defUp.name;
                target.AddState(defupState);
            };
            matthew.skill.Add(defUp);

            Skill taunt = CreateStateSkill("决意之盾", 0, 17, TargetType.Self);
            taunt.description = "坚定守护队友的决心，一定时间内提高自己被攻击的概率。（MP：17）";
            taunt.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State tauntState = CreateSpecialState("嘲讽", 0, 4);
                tauntState.skillName = taunt.name;
                tauntState.ratio = 0.5;
                tauntState.type = StateType.Taunt;
                attacker.AddState(tauntState);
            };
            matthew.skill.Add(taunt);

            Skill revive = CreateStateSkill("神圣之城", 0, 44, TargetType.PartySingle);
            revive.description = "为队友附加神圣的守护，使一名队友能够复活一次。（MP：44）";
            revive.skillEffect = (BaseCharacter attacker, BaseCharacter target) => 
            {
                State revState = CreateSpecialState("复活", 1, 0);
                revState.skillName = revive.name;
                revState.type = StateType.Revive;
                target.AddState(revState);
            };
            matthew.skill.Add(revive);

            return matthew;
        }

        /// <summary>
        /// 创建一个怪物红色史莱姆
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharSlimeRed()
        {
            BaseCharacter slime = new BaseCharacter();
            slime.name = "红色史莱姆";
            slime.maxHp = 1351;
            slime.maxMp = 385;
            slime.Hp = 1351;
            slime.Mp = 385;
            slime.atk = 40;
            slime.def = 40;
            slime.mat = 20;
            slime.men = 20;
            slime.hit = 80;
            slime.crt = 5;
            slime.level = 15;
            slime.type = CharacterType.Enemy;
            slime.actionType = ActionMode.MaxHP;
            Skill normalAttack = CreateAttackSkill(1, 20, DamageType.Physical, TargetType.EnemySingle);
            normalAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                return attacker.atk * 2 - target.def;
            };
            slime.skill.Add(normalAttack);

            Skill strongeAttack = CreateDamageSkill("强击", 0, 10, 1, 10, TargetType.EnemySingle, DamageType.Physical);
            strongeAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                return attacker.atk * 3 - target.def;
            };
            slime.skill.Add(strongeAttack);

            Skill softBody = CreateStateSkill("柔化", 0, 20, TargetType.Self);
            softBody.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State softState = CreateIncOrDecState("物理伤害降低", 0, 4, -0.5);
                softState.type = StateType.PhysicalBeDamageIncrease;
                softState.skillName = softBody.name;
                attacker.AddState(softState);
            };
            slime.skill.Add(softBody);

            Skill phyReflect = CreateStateSkill("物理反射", 0, 20, TargetType.Self);
            phyReflect.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State phyReflectState = CreateSpecialState("物理反射", 1, 0);
                phyReflectState.type = StateType.PhysicalReflect;
                phyReflectState.skillName = phyReflect.name;
                attacker.AddState(phyReflectState);
            };
            slime.skill.Add(phyReflect);

            //史莱姆的技能选择逻辑
            slime.GetRandomSkill = (BaseCharacter bc, List<BaseCharacter> bcList, int counts) =>
            {
                //每7回合用一次柔化
                if (counts % 7 == 1)
                {
                    if(bc.skill[2].CanUseSkill(bc))
                        return bc.skill[2];
                }
                //血量低于一半的时候如果自身没有物理反射状态，释放物理反射技能
                if (bc.Hp < (bc.maxHp / 2))
                {
                    if(!bc.HasState(StateType.PhysicalReflect))
                        if (bc.skill[3].CanUseSkill(bc))
                            return bc.skill[3];
                }
                //其他情况下，优先在普通攻击和强击中选择技能
                List<int> tempList = new List<int>();
                for (int i = 0; i < 2; i++)
                {
                    if (bc.skill[i].CanUseSkill(bc))
                        tempList.Add(i);
                }
                return bc.skill[tempList[Program.random.Next(0, tempList.Count)]];
            };
            return slime;
        }

        /// <summary>
        /// 创造一个蓝色史莱姆
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharSlimeBlue()
        {
            BaseCharacter slime = new BaseCharacter();
            slime.name = "蓝色史莱姆";
            slime.maxHp = 952;
            slime.maxMp = 502;
            slime.Hp = 952;
            slime.Mp = 502;
            slime.atk = 20;
            slime.def = 20;
            slime.mat = 40;
            slime.men = 40;
            slime.hit = 80;
            slime.crt = 0;
            slime.level = 15;
            slime.type = CharacterType.Enemy;
            slime.actionType = ActionMode.MinHP;

            Skill normalAttack = CreateAttackSkill(1, 10, DamageType.Magic, TargetType.EnemySingle);
            normalAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                return attacker.mat * 2 - target.men;
            };
            slime.skill.Add(normalAttack);

            Skill magReflect = CreateStateSkill("魔法反射", 0, 30, TargetType.Self);
            magReflect.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                State magrefState = CreateSpecialState("魔法反射", 1, 0);
                magrefState.skillName = magReflect.name;
                magrefState.type = StateType.MagicReflect;
                target.AddState(magrefState);
            };
            slime.skill.Add(magReflect);

            Skill forbidHeal = CreateStateSkill("禁疗", 0, 30, TargetType.EnemyMulti);
            forbidHeal.skillEffect = (BaseCharacter attacker, BaseCharacter target) => 
            {
                State forbidHealState = CreateSpecialState("禁疗", 0, 4);
                forbidHealState.skillName = forbidHeal.name;
                forbidHealState.type = StateType.ForbidHeal;
                target.AddState(forbidHealState);
            };
            slime.skill.Add(forbidHeal);

            Skill magicBullet = CreateDamageSkill("魔法爆弹", 0, 10, 1, 5, TargetType.EnemySingle, DamageType.Magic);
            magicBullet.skillDamage = (BaseCharacter attacker, BaseCharacter target) => 
            {
                int temp = Convert.ToInt32(attacker.mat * 2.75 - target.men);
                return temp > 0 ? temp : 1;
            };
            slime.skill.Add(magicBullet);

            Skill heal = CreateHealSkill("治疗", 0, 20, 1, 0, TargetType.PartySingle);
            heal.skillDamage = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int temp = Convert.ToInt32(attacker.men * 3.3);
                return temp > 0 ? temp : 1;
            };
            slime.skill.Add(heal);

            slime.GetRandomSkill = (BaseCharacter bc, List<BaseCharacter> bcList, int counts) => 
            {
                //从第三回合开始每7回合一次禁疗
                if (counts % 7 == 3)
                {
                    if (bc.skill[2].CanUseSkill(bc))
                        return bc.skill[2];
                }

                //从第二回合开始，每四回合检测一次血量并选择释放回血技能
                if (counts % 4 == 2)
                {
                    for (int i = 0; i < bcList.Count; i++)
                    {
                        if (bcList[i].Hp < bcList[i].maxHp / 2)
                        {
                            if (bc.skill[4].CanUseSkill(bc) && bcList[i].CanBeTarget(bc.skill[4]))
                                return bc.skill[4];
                        }
                    }
                }

                //从第一回合开始每5回合一次魔法反射
                if (counts % 5 == 1)
                {
                    if (!bc.HasState(StateType.MagicReflect))
                    {
                        if (bc.skill[1].CanUseSkill(bc))
                            return bc.skill[1];
                    }
                }

                //其他情况能用魔法爆弹就用魔法爆弹
                if (bc.skill[3].CanUseSkill(bc))
                    return bc.skill[3];

                return bc.skill[0];
            };
            return slime;
        }

        /// <summary>
        /// 创造一个史莱姆王
        /// </summary>
        /// <returns></returns>
        public static BaseCharacter CreateCharSlimeKing()
        {
            BaseCharacter slimeKing = new BaseCharacter();
            slimeKing.name = "史莱姆王";
            slimeKing.maxHp = 5428;
            slimeKing.maxMp = 2301;
            slimeKing.Hp = 5428;
            slimeKing.Mp = 2301;
            slimeKing.atk = 50;
            slimeKing.def = 30;
            slimeKing.mat = 42;
            slimeKing.men = 19;
            slimeKing.hit = 85;
            slimeKing.crt = 15;
            slimeKing.level = 15;
            slimeKing.type = CharacterType.Enemy;

            Skill normalAttack = CreateAttackSkill(1, 20, DamageType.Physical, TargetType.EnemySingle);
            normalAttack.skillDamage = (BaseCharacter attacker, BaseCharacter target) => 
            {
                int temp = Convert.ToInt32(attacker.atk * 1.78 - target.def);
                return temp > 0 ? temp : 1;
            };
            slimeKing.skill.Add(normalAttack);

            Skill damageDec = CreateStateSkill("伤害加深", 0, 40, TargetType.EnemySingle);
            damageDec.skillEffect = (BaseCharacter attacker, BaseCharacter target) => 
            {
                State damageIncState = CreateIncOrDecState("物理伤害加深", 0, 3, 0.4);
                damageIncState.skillName = damageDec.name;
                damageIncState.type = StateType.PhysicalBeDamageIncrease;
                target.AddState(damageIncState);

                State damageIncState2 = CreateIncOrDecState("魔法伤害加深", 0, 3, 0.4);
                damageIncState2.skillName = damageDec.name;
                damageIncState2.type = StateType.MagicBeDamageIncrease;
                target.AddState(damageIncState2);
            };
            slimeKing.skill.Add(damageDec);

            Skill recover = CreateStateSkill("再生", 0, 52, TargetType.Self);
            recover.skillEffect = (BaseCharacter attacker, BaseCharacter target) =>
            {
                int hprecover = Convert.ToInt32(attacker.maxHp * 0.02);
                State recoverState = CreateHOTState("再生", 0, 3, hprecover);
                recoverState.skillName = damageDec.name;
                target.AddState(recoverState);
            };
            slimeKing.skill.Add(recover);

            slimeKing.GetRandomSkill = (BaseCharacter bc, List<BaseCharacter> bcList, int counts) => 
            {
                //如果掉血了且不处于再生状态下，释放再生技能
                if (bc.Hp < bc.maxHp)
                {
                    if(!bc.HasState(StateType.HealOverTime))
                        if (bc.skill[2].CanUseSkill(bc))
                            return bc.skill[2];
                }

                if(counts % 4 == 2)
                        if (bc.skill[1].CanUseSkill(bc))
                            return bc.skill[2];

                return bc.skill[0];
            };

            return slimeKing;
        }
    }
}
