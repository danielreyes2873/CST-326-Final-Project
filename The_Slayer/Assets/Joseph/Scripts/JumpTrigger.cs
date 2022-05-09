using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    { 
      Debug.Log("triggered");
      if(other.tag=="JTrigger"){
        Debug.Log("Enemy Triggered");
        // other.transform.parent.GetComponent<Enemy>().Jump();
      }
    }
}
