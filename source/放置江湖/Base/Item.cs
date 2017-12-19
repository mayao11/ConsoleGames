using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ItemType
{
    Consumable,
    Equip
}

public enum EquipType
{
    Head,
    Body,
    Hand,
    Shoes
}


namespace 放置江湖.Base
{
    class Item
    {
        public int id;
        public string name;
        public int proHp;
        public int proMp;
        public int proAtk;
        public int proDef;
        public int price;
        public string itemDepict;
        public int number;

        public ItemType itemType;
        public EquipType equipType;

        public static Item CreateConsumable(int id,string name,int proHp,int proMp,int price,string itemDepict,int number)
        {
            var item = new Item()
            {
                id = id,
                name = name,
                proHp = proHp,
                proMp = proMp,
                price = price,
                itemType = ItemType.Consumable,
                itemDepict = itemDepict,
                number = number
            };
            return item;
        }

        public static Item CreateEquip(int id,string name,int proHp,int proMp,int proAtk,int proDef,int price,EquipType equipType,string itemDepict)
        {
            var item = new Item()
            {
                id = id,
                name = name,
                proHp = proHp,
                proMp = proMp,
                proAtk = proAtk,
                proDef = proDef,
                price = price,
                equipType = equipType,
                itemType = ItemType.Equip,
                itemDepict=itemDepict,
                number=1

            };
            return item;

        }

       

    }
}
