using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { BAG, WEAPON }

public class SlotHolder : MonoBehaviour, IPointerClickHandler
{
    public SlotType slotType;
    public ItemUI itemUI;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount % 2 == 0)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (itemUI.GetItemData() == null) return;

        //if it is a heal props
        if(itemUI.GetItemData().healProps != null && itemUI.GetItemAmount() > 0)
        {
            //TODO Setup Health
            GameManager.Instance.playerStats.AddHealth(itemUI.GetItemData().healProps.healHealth);
            itemUI.Bag.itemsList[itemUI.Index].amount -= 1;
        }

        UpdateIetm();
    }

    public void UpdateIetm()
    {
        switch(slotType)
        {
            case SlotType.BAG:
                //get InventoryData_SO, and InventoryData is holding a list
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            case SlotType.WEAPON:
                break;
        }
        //get the items from the list, which is InventoryItem --- its holding itemData_SO and amount
        var item = itemUI.Bag.itemsList[itemUI.Index];

        //show item in the backpack
        itemUI.SetupItemUI(item.itemData, item.amount);
    }

}
