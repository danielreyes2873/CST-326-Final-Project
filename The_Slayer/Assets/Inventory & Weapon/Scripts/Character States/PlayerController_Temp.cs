using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_Temp : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    
    
    public Vector3 forwardDistance;
    public float radius;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // Movement();
        OtherButtons();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(gameObject.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-gameObject.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-gameObject.transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(gameObject.transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        }

    }

    private void OtherButtons()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //see result in function OnDrawGizmosSelected
            checkItem();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.playerStats.DropWeapon();
        }
    }

    private void checkItem()
    {
        var checkItems = Physics.OverlapSphere(transform.position + forwardDistance, radius);
        foreach (var item in checkItems)
        {
            if(item.gameObject.CompareTag("item"))
            {
                //check if the item is weapon or not;
                var mItem = item.GetComponent<ItemPickUp>().itemData;
                if(mItem.itemType == ItemType.Weapon)
                {
                    GameManager.Instance.playerStats.EquipWeapon(item.GetComponent<ItemPickUp>().itemData);
                }
                else //TODO: it is a props, add it to bag
                {
                    InventoryManager.Instance.inventoryData.AddItem(mItem, mItem.itemAmount);
                    InventoryManager.Instance.inventoryUI.RefreshUI();
                }
                Destroy(item.gameObject);
                //only allow player pick up 1 items at 1 time
                break;
            }
        }
    }

    //You can ignore this if you do not want to see the blue sphere
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + forwardDistance, radius);
    }
}
