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
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    void Start()
    {
        health=20;
        enemyAnimation=this.GetComponent<Animator>();
        enemyAnimation.speed=2.0f;
        agent.speed = 0.5f;

        wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;

        setHealth(wave);
        setStrength(wave);
        // setSpeed(wave);
    }

    void Update()
    {
        if(!dead){
            if(GameObject.FindWithTag("Player")!=null){
                Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
                agent.SetDestination(playerPosition);

            if(Vector3.Distance(this.transform.position,playerPosition)<0.75f){
                enemyAnimation.speed=2.0f;
                enemyAnimation.SetTrigger("Attack");
            }
            else{
                enemyAnimation.SetTrigger("Walk");
                agent.speed = 0.5f;
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
        enemyAnimation.speed=0.7f;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        dead=true;
      }
    }

    public void setHealth(int wave){
      health += 10*wave;
    }

    public void setStrength(int damageDealt){
      int newStrength = wave - 1;
      health += 10 * newStrength;
    }
}