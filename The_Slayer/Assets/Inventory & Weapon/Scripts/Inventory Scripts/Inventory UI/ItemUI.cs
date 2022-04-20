using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public Image icon = null;
    public TextMeshProUGUI amount = null;

    //will setup value in SlotHolder
    public InventoryData_SO Bag { get; set; }

    //will setup value in ContrainerUI
    public int Index { get; set; } = -1;

    public void SetupItemUI(itemData_SO item, int itemAmount)
    {
        //if run out of items
        if(itemAmount == 0)
        {
            Bag.itemsList[Index].itemData = null; 
            icon.gameObject.SetActive(false);
            return;
        }

        if (item == null) return;

        icon.sprite = item.itemIcon; 
        amount.text = itemAmount.ToString();
        icon.gameObject.SetActive(true);
    }

    public itemData_SO GetItemData()
    {
        return Bag.itemsList[Index].itemData;    
    }

    public int GetItemAmount()
    {
        return Bag.itemsList[Index].amount;
    }
}
