using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    private int weaponStrength=10;
    public ParticleSystem blood;
    public ParticleSystem smoke;
    public ParticleSystem brains;
    public GameObject bulletHolePrefab;
    public GameObject bulletWoundPrefab;

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
                        Instantiate(blood,hit.point,Quaternion.LookRotation(hit.normal));
                        
                    }
                    if(hit.transform.name=="Ghoul(Clone)"){
                        hit.transform.GetComponent<Ghoul>().decrementHealth(weaponStrength);
                        Instantiate(blood,hit.point,Quaternion.LookRotation(hit.normal));
                    }
                }
                else if(hit.transform.tag=="Head"){
                    Debug.Log("Hit the head");
                    hit.transform.parent.GetComponent<Enemy>().decrementHealth(weaponStrength*3);
                    if(hit.transform.parent.GetComponent<Enemy>().dead==true){
                        hit.transform.parent.GetComponent<Enemy>().Headshot();
                        hit.transform.gameObject.SetActive(false);
                    }
                }
                else{

                }
            }
        }

    }
}