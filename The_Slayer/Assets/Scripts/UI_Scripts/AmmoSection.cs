using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSection : MonoBehaviour
{

    //Displaying Ammo Values
    [Header("Ammo Count Text")]
    public TextMeshProUGUI myAmmoCountText;

    //Display "reloading..."
    [Header("Reloading Text")] 
    public TextMeshProUGUI reloadingText;

    //Display bullet images
    [Header("Bullet Image Panel")] 
    public GameObject myPanel;
    public Image myBulletImage;
    

    //'lastFired' is the time you last fired a projectile.
    public float lastFired;

    // Start is called before the first frame update
    void Start()
    {
        //Testing linking up ammo to current weapon
        //gives a full magazine to the  player
        GameManager.Instance.playerStats.currentWeapon.currentMag =
            GameManager.Instance.playerStats.currentWeapon.currentMagCap;
        
        //gives a full max ammo to the player
        GameManager.Instance.playerStats.currentWeapon.spareAmmo =
            GameManager.Instance.playerStats.currentWeapon.maxAmmo;

        //Ammo Count Text 
        myAmmoCountText.text = $"{GameManager.Instance.playerStats.currentWeapon.currentMag} / {GameManager.Instance.playerStats.currentWeapon.spareAmmo}";
        
        
        
        //Bullet Image Test (remove if does not work)
        //displaying bullets for player weapon (if they start with one)
        //(THIS CODE IS ALREADY WORKING WITH UPDATE CODE)
        // for (int i = 0; i < GameManager.Instance.playerStats.currentWeapon.currentMag; i++)
        // {
        //     Instantiate(myBulletImage, myPanel.transform.position, Quaternion.identity, myPanel.transform);
        // }

        //gives a full max ammo to the player
        GameManager.Instance.playerStats.currentWeapon.spareAmmo =
            GameManager.Instance.playerStats.currentWeapon.maxAmmo;

        //Ammo Count Text 
        myAmmoCountText.text = $"{GameManager.Instance.playerStats.currentWeapon.currentMag} / {GameManager.Instance.playerStats.currentWeapon.spareAmmo}";
    }

    // Update is called once per frame
    void Update()
    {
        //If your magazine isn't empty, display ammo count text.
        if (GameManager.Instance.playerStats.currentWeapon.currentMag >= 0)
        {
            myAmmoCountText.text = $"{GameManager.Instance.playerStats.currentWeapon.currentMag.ToString("00")} /" +
                                   $"{GameManager.Instance.playerStats.currentWeapon.spareAmmo.ToString("00")}";
        }
        
        
        //If player's current magazine is empty but you still has ammo, reload weapon
        if (GameManager.Instance.playerStats.currentWeapon.currentMag <= 0 && GameManager.Instance.playerStats.currentWeapon.spareAmmo > 0)
        {
            //Reload
            Reload();
        }
        
        //instantiate bullet images per ammo in current magazine
        if (myPanel.transform.childCount < GameManager.Instance.playerStats.currentWeapon.currentMag)
        {
            for (int i = myPanel.transform.childCount; i < GameManager.Instance.playerStats.currentWeapon.currentMag; i++)
            {
                Instantiate(myBulletImage, myPanel.transform.position, Quaternion.identity, myPanel.transform);
            }
        }


        //Delete all bullet images if you have more images than bullets in current magazine.
        if(myPanel.transform.childCount > GameManager.Instance.playerStats.currentWeapon.currentMag)
        {
            // Debug.Log("Bullet images" + myPanel.transform.childCount + ">" + "Ammo in mag" + GameManager.Instance.playerStats.currentWeapon.currentMag);
            DeleteAllBulletDisplay();
        }
        
        //instantiate bullet images per ammo in current magazine
        if (myPanel.transform.childCount < GameManager.Instance.playerStats.currentWeapon.currentMag)
        {
            for (int i = myPanel.transform.childCount; i < GameManager.Instance.playerStats.currentWeapon.currentMag; i++)
            {
                Instantiate(myBulletImage, myPanel.transform.position, Quaternion.identity, myPanel.transform);
            }
        }
        
        
        //Delete all bullet images if you have more images than bullets in current magazine.
        if(myPanel.transform.childCount > GameManager.Instance.playerStats.currentWeapon.currentMag)
        {
            Debug.Log("Bullet images" + myPanel.transform.childCount + ">" + "Ammo in mag" + GameManager.Instance.playerStats.currentWeapon.currentMag);
            DeleteAllBulletDisplay();
        }
        
        
        //---------UNCOMMENT TO TEST SHOOTING HERE------
        
        //Automatic weapon firing while holding down mouse left click. (Still able to do single shot)
        
        // if (Input.GetMouseButton(0))
        // {
        //     Shoot();
        // }
    }
    
    //Delete bullet images. (IF YOU HAVE MORE BULLET IMAGES THAN BULLETS)
    public void DeleteAllBulletDisplay()
    {
        // Debug.Log("Destroying all bullet images");
        int childs = myPanel.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Destroy(myPanel.transform.GetChild(i).gameObject);
        }
    }

    //Shoot Testing
    public void Shoot()
    {
        //ready to fire.
        lastFired = Time.time;

        //Testing bullet image (remove if does not work)
        //removing bullet from bulletPanel after a shot is fired
        if (GameManager.Instance.playerStats.currentWeapon.currentMag > 0)
        {
            Destroy(myPanel.transform.GetChild(GameManager.Instance.playerStats.currentWeapon.currentMag - 1).gameObject);
        }
        
        //One shoot, one bullet
        GameManager.Instance.playerStats.currentWeapon.currentMag -= 1;

        //If you have a bullets in current magazine, you can shoot. else You can not.
        if (GameManager.Instance.playerStats.currentWeapon.currentMag > 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
    }
    
    
    // //Delete bullet images. (IF YOU HAVE MORE BULLET IMAGES THAN BULLETS)
    // public void DeleteAllBulletDisplay()
    // {
    //     Debug.Log("Destroying all bullet images");
    //     int childs = myPanel.transform.childCount;
    //     for (int i = childs - 1; i >= 0; i--)
    //     {
    //         Destroy(myPanel.transform.GetChild(i).gameObject);
    //     }
    // }
    
//     //Will remove one bullet image after shooting
//     public void RemoveOneBulletImageAfterFiring()
//     {
// <<<<<<< HEAD
//         //removing bullet from bulletPanel after a shot is fired
//         if (GameManager.Instance.playerStats.currentWeapon.currentMag > 0)
//         {
//             Destroy(myPanel.transform.GetChild(GameManager.Instance.playerStats.currentWeapon.currentMag - 1).gameObject);
//         }
// =======
//         //ready to fire.
//         if (Time.time - lastFired > 1 / GameManager.Instance.playerStats.currentWeapon.fireRate)
//         {
//             lastFired = Time.time;
            
//             //Testing bullet image (remove if does not work)
//             //removing bullet from bulletPanel after a shot is fired
//             if (GameManager.Instance.playerStats.currentWeapon.currentMag > 0)
//             {
//                 Destroy(myPanel.transform.GetChild(GameManager.Instance.playerStats.currentWeapon.currentMag - 1).gameObject);
//             }
            
            
//             //One shoot, one bullet
//             GameManager.Instance.playerStats.currentWeapon.currentMag -= 1;
            
//             //todo: instantiate projectile
//             //instantiate projectile -----> ray cast projectile
//         }

//     }


    //Used in shooting script
    //Will remove one bullet image after shooting
    public void RemoveOneBulletImageAfterFiring()
    {
        //removing bullet from bulletPanel after a shot is fired
        if (GameManager.Instance.playerStats.currentWeapon.currentMag > 0)
        {
            Destroy(myPanel.transform.GetChild(GameManager.Instance.playerStats.currentWeapon.currentMag - 1).gameObject);
        }
// >>>>>>> main
    }
    
    //Reload Testing
    public void Reload()
    {
        //Reloading Text
        StartCoroutine(DisplayReloadingText());
        // //remove a full magazine from our current ammo
        GameManager.Instance.playerStats.currentWeapon.spareAmmo -= GameManager.Instance.playerStats.currentWeapon.currentMagCap;
        //
        // //current magazine is now full
        GameManager.Instance.playerStats.currentWeapon.currentMag += GameManager.Instance.playerStats.currentWeapon.currentMagCap;
        
        
        //Testing bullet images in ammo section
        //reshow the bullet in mybulletPanel per bullet we have (CODE NOT NEEDED)
        // for (int i = 0; i < GameManager.Instance.playerStats.currentWeapon.currentMagCap; i++)
        // {
        //     Instantiate(myBulletImage, myPanel.transform.position, Quaternion.identity, myPanel.transform);
        // }
        
    }

    
    //Displaying "Reloading..." Text.
    public IEnumerator DisplayReloadingText()
    {
        //Reloading Text
        reloadingText.text = "Reloading...";

        //Display "Reloading..." for 1 second.
        yield return new WaitForSeconds(GameManager.Instance.playerStats.currentWeapon.reloadDelay);

        //Set reloading text back to blank.
        reloadingText.text = "";
    }
    
    
}
