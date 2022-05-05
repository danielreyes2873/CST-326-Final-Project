using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//CheckItem and pick up is in the function, on the bottom
public class AnimationController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator anim;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            layerControl();
            walkAnimation();
            AimAndFireAnim();
        }
    }

    private void AimAndFireAnim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isFire", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isFire", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("isAim", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isAim", false);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            checkItem();
        }

        if (Input.GetKeyDown(KeyCode.G) && GameManager.Instance.playerStats.secondWeapon != null)
        {
            GameManager.Instance.playerStats.DropWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
        }


    }


    private void layerControl()
    {
        var currentWeapon = GameManager.Instance.playerStats.currentWeapon;
        if (currentWeapon != null) //if player do not have weapon, play base layer
        {
            if (currentWeapon.gunType == GunType.Rifle)
            {
                anim.SetLayerWeight(1, 0);
            }
            else
            {
                anim.SetLayerWeight(1, 1);
            }
        }
    }
    private void walkAnimation()
    {
        if (Mathf.Approximately(playerMovement.inputX, 0) || Mathf.Approximately(playerMovement.inputZ, 0))
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("InputX", playerMovement.inputX);
            anim.SetFloat("InputZ", playerMovement.inputZ);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }


    /// <summary>
    /// It create a sphere to check if there has item I can pick up
    /// </summary>
    private void checkItem()
    {
        var checkItems = Physics.OverlapSphere(transform.position + new Vector3(0, 0.9f, 0.9f), 1.2f);
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



}
