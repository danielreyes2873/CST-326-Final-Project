using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // public ParticleSystem powerup;
    public float powerupRotationSpeed =30f;
    public string powerupName;
    private int powerupPoints = 300;

    void Start(){
        Destroy(this.gameObject,20f);
    }

    void Update()
    {
        transform.Rotate(0f, powerupRotationSpeed* Time.deltaTime, 0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="Player"){
            if(powerupName=="Ammo"){
                
            }
            else if(powerupName=="Money"){
                PlayerStats.totalPlayerScore+=powerupPoints;

            }
            // Instantiate(powerup);
            Destroy(this.gameObject);
        }
    }
}
