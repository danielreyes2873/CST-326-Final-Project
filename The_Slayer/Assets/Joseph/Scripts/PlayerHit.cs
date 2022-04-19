using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {   
        if(other.tag=="Player"){
            if((GameObject.Find("Player")!=null)){
              GameObject.Find("Player").GetComponent<PlayerTest>().Hit();
            }
        }
    }
}
