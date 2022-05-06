using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy has a cube prefab that checks if the enemy hits the enemy.
 * References the player's CharacterStats script.
*/ 

public class PlayerHit : MonoBehaviour
{
    public GameObject UI;
    public int commonStrength=5;
    public int ghoulStrength=2;


    public void Start(){
        UI = GameObject.Find("PlayerUI");
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="Player"){
// <<<<<<< HEAD
            if(GameObject.Find("Player")!=null && GameManager.Instance.playerStats.currentHealth>0){
                 if(this.transform.parent.gameObject.name.Contains("Common")){
                     GameManager.Instance.playerStats.currentHealth-=commonStrength;
                 }
                 else if (this.name.Contains("Ghoul")){
                     GameManager.Instance.playerStats.currentHealth-=ghoulStrength;
                 }
                 
                 UI.GetComponentInChildren<HealthBar>().SetHealth(GameManager.Instance.playerStats.currentHealth);
                 GameObject.Find("HitEffect").GetComponent<UI>().playHitEffect();
// =======
//             if((GameObject.Find("Player2")!=null)){
//               // GameObject.Find("Player2").GetComponent<PlayerTest>().Hit();
//               GameObject.Find("Player2").GetComponent<CharacterStats>().TakeDamage(5);
// >>>>>>> main
            }
        }
    }
}
