using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public int health;
    public void Hit(){
        GameObject.Find("HitEffect").GetComponent<UI>().playHitEffect();
        health--;
        if(health<=0){
            // Destroy(this.gameObject);
            Debug.Log("You were killed!");
        }
    }
}
