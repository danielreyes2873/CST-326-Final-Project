using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public int weaponStrength=10;
    public ParticleSystem blood;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ 
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000f)){  
                if(hit.transform.tag=="Enemy"){
                    if(hit.transform.name=="Common(Clone)"){
                        hit.transform.GetComponent<Enemy>().decrementHealth(weaponStrength);
                        Instantiate(blood,hit.point,Quaternion.LookRotation(hit.normal));
                    }
                    if(hit.transform.name=="BigZombie(Clone)"){
                        hit.transform.GetComponent<BigZombie>().decrementHealth(weaponStrength);
                    }
                    if(hit.transform.name=="Ghoul(Clone)"){
                        hit.transform.GetComponent<Ghoul>().decrementHealth(weaponStrength);
                    }
                }
            }
        }

    }
}
