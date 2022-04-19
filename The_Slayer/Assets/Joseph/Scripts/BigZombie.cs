using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZombie : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health;
    public float speed=0.5f;
    public int strength;
    public bool dead=false;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    void Start()
    {
        health=50;
        enemyAnimation=this.GetComponent<Animator>();
        enemyAnimation.speed=1.0f;
        agent.speed = 0.2f;
    }

    void Update()
    {
        if(!dead){
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        agent.SetDestination(GameObject.FindWithTag("Player").transform.position);

        if(Vector3.Distance(this.transform.position,playerPosition)<1.0f){
            enemyAnimation.speed=1.2f;
            enemyAnimation.SetTrigger("Attack");
        }
        else{
            enemyAnimation.SetTrigger("Walk");
            agent.speed = 0.5f;
        }
        }
    }

    public void decrementHealth(int damageDealt){
      health=health-20;
      if(health<=0 && !dead){
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        enemyAnimation.speed=0.5f;
        enemyAnimation.SetTrigger("Dead");
        Destroy(this.gameObject,5f);
        dead=true;
      }
    }
}
