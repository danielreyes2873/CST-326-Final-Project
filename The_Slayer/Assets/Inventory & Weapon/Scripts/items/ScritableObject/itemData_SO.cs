using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Useable, Weapon, Armor }
public enum GunType { Pistal, Rifle };

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item Data")]
public class itemData_SO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;
    [TextArea]
    public string description;
    public bool stackable;

    [Header("Useable")]
    public HealProps_SO healProps;

    [Header("Weapon")]
    /// <summary>
    /// The interval time between each bullet
    /// </summary>
    public float coolDown;
    /// <summary>
    /// Have to be end with eq
    /// </summary>
    [Header("Equipment")]
    public GameObject weaponPrefab;
    [Header("OnWorld")]
    public GameObject weaponOnWorld;

    public GunType gunType;

    public int damage;

    public int currentMag;
    public int spareAmmo;
    
    //magazine capacity - total rounds a magazine can hold.
    public int currentMagCap;
    //maxAmmo is total ammo the player can hold of the weapon.
    public int maxAmmo;
    //Weapon's fireRate
    public float fireRate;
    public float reloadDelay = 1f;

}
