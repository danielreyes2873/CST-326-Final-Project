using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            checkItem();
        }
        if (Input.GetKeyDown(KeyCode.G) && GameManager.Instance.playerStats.secondWeapon != null)
        {
            GameManager.Instance.playerStats.DropWeapon();
        }

    }
    private void checkItem()
    {
        var checkItems = Physics.OverlapSphere(Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward, 1.2f);
        foreach (var item in checkItems)
        {
            if (item.gameObject.CompareTag("item"))
            {
                //check if the item is weapon or not;
                var mItem = item.GetComponent<ItemPickUp>().itemData;
                if (mItem.itemType == ItemType.Weapon)
                {
                    //give weapon to player
                    GameManager.Instance.playerStats.EquipWeapon(item.GetComponent<ItemPickUp>().itemData);
                }
                else
                {
                    //add item
                    InventoryManager.Instance.inventoryData.AddItem(mItem, mItem.itemAmount);
                    InventoryManager.Instance.inventoryUI.RefreshUI();
                }
                Destroy(item.gameObject);
                //only allow player pick up 1 items at 1 time
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Camera.main.gameObject.transform.position + Camera.main.gameObject.transform.forward, 1.2f);
    }
}
