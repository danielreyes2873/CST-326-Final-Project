using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health=20;
    public float speed=0.5f;
    public int strength;
    public int wave;
    public bool dead=false;
    private float regularSpeed = 0.3f;
    private float regularAnimationSpeed = 2.0f;
    private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 0.75f;

    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    void Start()
    {
        enemyAnimation=this.GetComponent<Animator>();
        wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;
        setHealth(wave);
        setStrength(wave);
    }

    void Update()
    {
        if(!dead){
            if(GameObject.FindWithTag("Player")!=null){
                Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
                agent.SetDestination(playerPosition);

            if(Vector3.Distance(this.transform.position,playerPosition)<attackDistance){
                enemyAnimation.speed=regularAnimationSpeed;
                enemyAnimation.SetTrigger("Attack");
            }
            else{
                enemyAnimation.SetTrigger("Walk");
                agent.speed = regularSpeed;
            }
            }
        }
    }

    public void decrementHealth(int damageDealt){
      health=health-10;
      if(health<=0 && !dead){
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        enemyAnimation.speed=deathAnimationSpeed;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        dead=true;
      }
    }

    public void setHealth(int wave){
      health += 10;
    }

    public void setStrength(int wave){
      health += 10;
    }
}