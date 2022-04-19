using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{
    public List<InventoryItem> itemsList = new List<InventoryItem>();

    public void AddItem(itemData_SO newItem, int amount)
    {
        //to check if items are repeat
        //bool found = false;
        
        //if found items in the Inventory(list) and it was stackable, put them together
        if(newItem.stackable)
        {
            foreach(var item in itemsList)
            {
                if(item.itemData == newItem)
                {
                    item.amount += amount;
                    return;
                }
            }
        }

        //Find Empty Space in the Inventory and put the item in
        for(int i = 0; i < itemsList.Count; i++)
        {
            if(itemsList[i].itemData == null)
            {
                itemsList[i].itemData = newItem;
                itemsList[i].amount = amount;
                break;
            }
        }
        
    }
}

[System.Serializable]
public class InventoryItem 
{
    public itemData_SO itemData;
    //the number depends on the total number of slot in the bag
    public int amount;
}
