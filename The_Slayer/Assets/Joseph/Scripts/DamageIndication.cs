using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndication : MonoBehaviour
{

    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(enemy);
        
    }

    public void setPlayer(Transform enemi){
        Debug.Log("Im active!");
        this.gameObject.SetActive(true);
        enemy=enemi;
        transform.LookAt(new Vector3 (enemy.position.x, enemy.position.y, transform.position.z));
    }
}
