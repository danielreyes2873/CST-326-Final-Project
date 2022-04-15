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
    
    
    //Ammo Testing
    [Header("Weapon Ammo Testing")] 
    public int maxAmmo = 210;
    public int currentAmmo;
    public int magazineCapacity = 30;
    public int currentMagazine;
    public float fireRate = 13f;
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
        if (currentMagazine >= 0)
        {
            myAmmoCountText.text = $"{currentMagazine.ToString("00")} / {currentAmmo.ToString("00")}";
        }
        
        
        //If player's current magazine is empty but still has ammo reload weapon
        if (currentMagazine <= 0 && currentAmmo > 0)
        {
            //Reload
            Reload();
        }
        
        //testing player shooting
        // if (Input.GetMouseButtonDown(0))
        // {
        //     //if player still has ammo in their current magazine, shoot.
        //     Shoot();
        // }

        //Automatic weapon firing
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    
    
    //Set slider's maxValue
    public void SetCurrentMaxMagazine(int ammo)
    {
        myAmmoSlider.maxValue = ammo;
        myAmmoSlider.value = ammo;
    }

    //Set slider's current value
    public void SetCurrentMagazine(int ammo)
    {
        myAmmoSlider.value = ammo;
    }
    
    

    //Shoot
    private void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            //One shoot
            currentMagazine -= 1;
            //instantiate projectile -----> ray cast projectile
        }

        //Update slider UI display
        SetCurrentMagazine(currentMagazine);
    }


    
    //Reload
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

    IEnumerator DisplayReloadingText()
    {
        //Reloading Text
        reloadingText.text = "Reloading...";

        yield return new WaitForSeconds(1f);

        reloadingText.text = "";
    }
    
    
}
