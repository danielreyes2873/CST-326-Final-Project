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

    public void Start()
    {
        UI = GameObject.Find("PlayerUI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // <<<<<<< HEAD
            if (GameManager.Instance.playerStats.currentHealth > 0)
            {
                GameManager.Instance.playerStats.currentHealth -= 5;
                
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
