using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptForUI : MonoBehaviour
{


    //
    // public itemData_SO currentWeapon;

    public GameObject playerUI;

    
    
    

    // public int currentMagazine
    // {
    //     get => currentWeapon?.currentMag ?? 0;
    //     set => currentWeapon.currentMag = value;
    // }
    //
    // public int maxAmmo
    // {
    //     get => currentWeapon?.spareAmmo ?? 0;
    //     set => currentWeapon.spareAmmo = value;
    // }


    
    


    // int currentAmmo = GameManager.Instance.playerStats.currentWeapon.spareAmmo;
    

    


    // Start is called before the first frame update
    void Start()
    {
        
        //player connection to UI - > ammo section - > display reload text in reload function
        // StartCoroutine(playerUI.GetComponentInChildren<AmmoSection>().DisplayReloadingText());
    }

    // Update is called once per frame
    void Update()
    {


        //Test connection player TakeDamage()
        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     TakeDamage(25);
        // }
        //
        // //Test connection weapon Fire()
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     TestShotFired();
        // }
        
        
        
    }


    public void TakeDamage(int damage)
    {
        GameManager.Instance.playerStats.currentHealth -= damage;
        playerUI.GetComponentInChildren<HealthBar>().SetHealth(GameManager.Instance.playerStats.currentHealth);
    }



    public void TestShotFired()
    {
        GameManager.Instance.playerStats.currentWeapon.currentMag -= 1;
        playerUI.GetComponentInChildren<AmmoSection>().RemoveOneBulletImageAfterFiring();
    }
}
