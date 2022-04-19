using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZombie : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health=50;
    public float speed=0.5f;
    public int strength;
    public bool dead=false;
    private float regularSpeed = 0.5f;
    private float regularAnimationSpeed = 1.0f;
    private float attackAnimationSpeed = 1.2f;
    private float deathAnimationSpeed = 0.5f;
    private float attackDistance = 0.85f;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    void Start()
    {
        enemyAnimation=this.GetComponent<Animator>();
    }

    void Update()
    {
        if(!dead){
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        agent.SetDestination(GameObject.FindWithTag("Player").transform.position);

        if(Vector3.Distance(this.transform.position,playerPosition)<attackDistance){
            enemyAnimation.speed=attackAnimationSpeed;
            enemyAnimation.SetTrigger("Attack");
        }
        else{
            enemyAnimation.SetTrigger("Walk");
            agent.speed = regularSpeed;
            enemyAnimation.speed=regularAnimationSpeed;
        }
        }
    }

    public void decrementHealth(int damageDealt){
      health=health-20;
      if(health<=0 && !dead){
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        enemyAnimation.speed=deathAnimationSpeed;
        enemyAnimation.SetTrigger("Dead");
        Destroy(this.gameObject,5f);
        dead=true;
      }
    }
}
