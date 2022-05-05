using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy has a cube prefab that checks if the enemy hits the enemy.
 * References the player's CharacterStats script.
*/ 

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="Player"){
            if((GameObject.Find("Player2")!=null)){
              // GameObject.Find("Player2").GetComponent<PlayerTest>().Hit();
              GameObject.Find("Player2").GetComponent<CharacterStats>().TakeDamage(5);
            }
        }
    }
}
