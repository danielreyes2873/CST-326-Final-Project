using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    //TODO: Add Save/load template
    [Header("Inventory Data")]
    public InventoryData_SO inventoryData;

    [Header("ContainerS")]
    public ContainerUI inventoryUI;
    public ItemTooltip tooltip;

    void Start()
    {
        inventoryUI.RefreshUI();  
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
