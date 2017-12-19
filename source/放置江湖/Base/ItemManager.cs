using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 放置江湖.Base
{
    class ItemManager
    {

        List<Item> itemList = new List<Item>();


        public List<Item> GetItemList()
        {
            return itemList;
        }

        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UseItem(Item item,Character player)
        {
            if (item.itemType==ItemType.Consumable)
            {
                player.HealHp(item.proHp);
                player.CostMp(item.proMp);
                foreach (var _item in itemList)
                {
                    if (_item.id==item.id)
                    {
                        _item.number -= 1;
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// 使用装备
        /// </summary>
        /// <returns></returns>
        public bool UseEquip()
        {


            return false;
        }


        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddItem(Item item)
        {
            if (item.itemType==ItemType.Consumable)
            {
                bool isHav = false;
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (item.name == itemList[i].name)
                    {
                        itemList[i].number += item.number;
                        isHav = true;
                        return true;
                    }
                }
                if (!isHav)
                {
                    itemList.Add(item);
                    return true;
                }

            }
            else if (item.itemType==ItemType.Equip)
            {
                itemList.Add(item);
                return true;
            }

            return false;
        }

        

        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool RemoveItem(Item item)
        {

            if (item.itemType==ItemType.Consumable)
            {
                Console.WriteLine("请输入需要抛弃的{0}数量,按回车结束", item.name);
                int n = int.Parse(Console.ReadLine());

                for (int i = 0; i < itemList.Count; i++)
                {
                    if (item.name == itemList[i].name)
                    {
                        if (n> itemList[i].number)
                        {
                            n = itemList[i].number;
                        }

                        itemList[i].number -= n;
                        return true;
                    }
                }
            }
            else if (item.itemType==ItemType.Equip)
            {
                itemList.Remove(item);
                return true;
            }

            return false;
        }



    }
}
