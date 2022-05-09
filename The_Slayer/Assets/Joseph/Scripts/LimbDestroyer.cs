using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDestroyer : MonoBehaviour
{
    public GameObject limbToDestroy;

    public void DestroyLimb(){
        Destroy(limbToDestroy);
    }
}
