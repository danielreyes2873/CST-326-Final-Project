using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSection : MonoBehaviour
{

    [Header("Ammo Magazine Slider")] 
    public Slider myAmmoSlider;
    
    [Header("Ammo Count Text")]
    public TextMeshProUGUI myAmmoCountText;

    [Header("Reloading Text")] 
    public TextMeshProUGUI reloadingText;
    
    
    
    //--------AMMO TESTING TO BE REPLACED WITH WEAPON'S SPECIFIC CODE LATER----------
    //Ammo Testing
    [Header("Weapon Ammo Testing")] 
    //'maxAmmo' is the TOTAL ammo you can CARRY on that weapon + the current magazine loaded in.
    public int maxAmmo = 210;
    //'currentAmmo' is your current TOTAL ammo you have LEFT + the current magazine loaded in.
    public int currentAmmo;
    //'magazineCapacity' is the total bullets you can fix in your magazine before reloading
    public int magazineCapacity = 30;
    //'currentMagazine' is the amount of bullets in your current magazine.
    public int currentMagazine;
    //'fireRate' is projectiles per second
    public float fireRate = 13f;
    //'lastFired' is the time you last fired a projectile.
    public float lastFired;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Start of game full ammo
        currentAmmo = maxAmmo;
        currentMagazine = magazineCapacity;

        //Ammo Count Text 
        myAmmoCountText.text = $"{magazineCapacity} / {maxAmmo}";
        
        //Ammo Slider
        SetCurrentMaxMagazine(magazineCapacity);


    }

    // Update is called once per frame
    void Update()
    {
        //If your magazine isn't empty, display ammo count text.
        if (currentMagazine >= 0)
        {
            myAmmoCountText.text = $"{currentMagazine.ToString("00")} / {currentAmmo.ToString("00")}";
        }
        
        
        //If player's current magazine is empty but you still has ammo, reload weapon
        if (currentMagazine <= 0 && currentAmmo > 0)
        {
            //Reload
            Reload();
        }
        
        
        //---------UNCOMMENT TO TEST SHOOTING HERE------
        
        //Automatic weapon firing while holding down mouse left click. (Still able to do single shot)
        
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    
    
    //Set slider's max Magazine Value
    public void SetCurrentMaxMagazine(int ammo)
    {
        myAmmoSlider.maxValue = ammo;
        myAmmoSlider.value = ammo;
    }

    //Set slider's current Magazine value
    public void SetCurrentMagazine(int ammo)
    {
        myAmmoSlider.value = ammo;
    }
    
    

    //Todo: Set this Shoot function up in 'Player' Script
    //Shoot Testing
    private void Shoot()
    {
        //ready to fire.
        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            
            //One shoot, one bullet
            currentMagazine -= 1;
            
            //todo: instantiate projectile
            //instantiate projectile -----> ray cast projectile
        }

        //Update slider UI display
        SetCurrentMagazine(currentMagazine);
    }


    //Todo: Set this Reload function up in 'Weapon/Player' Script
    //Reload Testing
    private void Reload()
    {
        //Reloading Text
        StartCoroutine(DisplayReloadingText());
        
        //remove a full magazine from our current ammo
        currentAmmo -= magazineCapacity;
        
        //current magazine is now full
        currentMagazine += magazineCapacity;
        
        //update ammo slider back to being full
        myAmmoSlider.value = currentMagazine;
    }

    
    //Displaying "Reloading..." Text.
    IEnumerator DisplayReloadingText()
    {
        //Reloading Text
        reloadingText.text = "Reloading...";

        //Display "Reloading..." for 1 second.
        yield return new WaitForSeconds(1f);

        //Set reloading text back to blank.
        reloadingText.text = "";
    }
    
    
}