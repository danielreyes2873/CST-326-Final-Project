using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    public GameObject spawn2;
       private void OnTriggerEnter(Collider other)
    {  
       spawn2.SetActive(true);
    }
}
