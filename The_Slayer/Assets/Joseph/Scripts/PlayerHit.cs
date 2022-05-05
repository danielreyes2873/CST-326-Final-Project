using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public GameObject UI;

    public void Start(){
        UI = GameObject.Find("PlayerUI");
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="Player"){
            if(GameObject.Find("Player")!=null && GameManager.Instance.playerStats.currentHealth>0){
                 GameManager.Instance.playerStats.currentHealth-=5;
                 UI.GetComponentInChildren<HealthBar>().SetHealth(GameManager.Instance.playerStats.currentHealth);
                 GameObject.Find("HitEffect").GetComponent<UI>().playHitEffect();
            }
        }
    }
}
