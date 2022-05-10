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
    public GameObject DI;
    private int commonStrength=5;
    private int ghoulStrength=2;
    public string enemyName;
    private GameObject [] enemies;


    public void Start()
    {
        UI = GameObject.Find("PlayerUI");
        DI = GameObject.Find("Damage");
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="Player"){
            if(GameManager.Instance.playerStats.currentHealth>0){
                 if(enemyName.Contains("Common")){
                     GameManager.Instance.playerStats.currentHealth-=commonStrength;
                 }
                 else if (enemyName.Contains("Ghoul")){
                     GameManager.Instance.playerStats.currentHealth-=ghoulStrength;
                 }

                //  GameManager.Instance.playerStats.currentHealth-=commonStrength;

                 UI.GetComponentInChildren<HealthBar>().SetHealth(GameManager.Instance.playerStats.currentHealth);
                 GameObject.Find("HitEffect").GetComponent<UI>().playHitEffect();



                //  DI.GetComponent<DamageIndication>().setPlayer(this.transform);
//             if((GameObject.Find("Player2")!=null)){
//               // GameObject.Find("Player2").GetComponent<PlayerTest>().Hit();
//               GameObject.Find("Player2").GetComponent<CharacterStats>().TakeDamage(5);
            }
            else{
                EndGame();
            }
            
        }
    }
    private void EndGame(){
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy")){
            if(enemies.transform.name.Contains("Common")){
            enemies.GetComponent<Enemy>().playerDie();
            }
            else{
                enemies.GetComponent<Ghoul>().playerDie();
            }
        }
    }
}
