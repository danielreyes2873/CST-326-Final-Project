using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public GameObject weapon;
    public Transform MagazineSlot;
    protected override void Awake()
    {
        base.Awake();
    }
    public void RigisterWeapon(GameObject weapon)
    {
        this.weapon = weapon;
    }

    public void RigisterMagazineTransform(Transform magazine)
    {
        MagazineSlot = magazine;
    }
    
    public void LogoutWeapon()
    {
        this.weapon = null;    
    }

}
