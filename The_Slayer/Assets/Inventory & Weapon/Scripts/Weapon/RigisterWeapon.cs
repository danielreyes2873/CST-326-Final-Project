using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigisterWeapon : MonoBehaviour
{

    private void Awake() 
    {
        WeaponManager.Instance.RigisterWeapon(gameObject);
    }

    private void OnDestroy() 
    {
        WeaponManager.Instance.LogoutWeapon();
    }

}
