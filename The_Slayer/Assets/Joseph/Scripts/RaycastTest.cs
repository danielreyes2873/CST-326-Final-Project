using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public ParticleSystem blood;
    public ParticleSystem smoke;
    public ParticleSystem brains;
    public ParticleSystem limb;
    public GameObject bulletHolePrefab;
    public GameObject bloodP;
    private int headshotMultiplier=2;
    // public GameObject bulletWoundPrefab;

    public void Hit(RaycastHit hit)
    {

                
                if(hit.transform.tag=="Limb"){
                        if(hit.transform.name.Contains("Forearm")){
                            hit.transform.parent.GetComponent<Enemy>().decrementHealth(GameManager.Instance.playerStats.currentWeapon.damage);
                            Instantiate(limb,hit.point,Quaternion.LookRotation(hit.normal));
                            hit.transform.parent.GetComponent<Enemy>().playHeadLimb();
                            Destroy(hit.transform.gameObject);
                        }
                        // else{
                        //     hit.transform.parent.GetComponent<Enemy>().decrementHealth(GameManager.Instance.playerStats.currentWeapon.damage);
                        //     if(hit.transform.parent.GetComponent<Enemy>().dead==true){
                        //         Instantiate(limb,hit.point,Quaternion.LookRotation(hit.normal));
                        //         hit.transform.parent.GetComponent<Enemy>().playHeadLimb();
                        //         Destroy(hit.transform.gameObject);
                        //     }
                        //     else{
                        //         GameObject p= Instantiate(bloodP,hit.point + hit.normal *0.1f ,Quaternion.LookRotation(-hit.normal)) as GameObject;
                        //         p.transform.parent= hit.transform;
                        //         Destroy(p,1f);
                        //     }
                        // }
                }

                else if(hit.transform.tag=="Enemy"){
                    if(hit.transform.name.Contains("Ghoul")){
                        hit.transform.GetComponent<Ghoul>().decrementHealth(GameManager.Instance.playerStats.currentWeapon.damage);
                    }
                    else{
                        hit.transform.GetComponent<Enemy>().decrementHealth(GameManager.Instance.playerStats.currentWeapon.damage);
                    }
                    GameObject p= Instantiate(bloodP,hit.point + hit.normal *0.1f ,Quaternion.LookRotation(-hit.normal)) as GameObject;
                    p.transform.parent= hit.transform;
                    Destroy(p,1f);

                }
                else if(hit.transform.tag=="Head"){
                    hit.transform.parent.GetComponent<Enemy>().decrementHealth(GameManager.Instance.playerStats.currentWeapon.damage*headshotMultiplier);
                    if(hit.transform.parent.GetComponent<Enemy>().dead==true){
                        hit.transform.parent.GetComponent<Enemy>().Headshot();
                        hit.transform.gameObject.SetActive(false);
                    }
                }
            }

}