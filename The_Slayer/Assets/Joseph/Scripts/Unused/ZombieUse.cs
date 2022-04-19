using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieUse : MonoBehaviour
{
    public Zombie zombie;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    void Start()
    {
        enemyAnimation=this.GetComponent<Animator>();
        int wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;
        if(wave>1){
            zombie.IncreaseHealth();
            zombie.IncreaseStrength();
        }
    }

    void Update()
    {
        if(!zombie.dead){
            if(GameObject.FindWithTag("Player")!=null){
                Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
                agent.SetDestination(playerPosition);

            if(Vector3.Distance(this.transform.position,playerPosition)<zombie.attackDistance){
                enemyAnimation.speed=zombie.regularAnimationSpeed;
                enemyAnimation.SetTrigger("Attack");
            }
            else{
                enemyAnimation.SetTrigger("Walk");
                agent.speed = zombie.regularSpeed;
            }
            }
        }
    }

    public void decrementHealth(int damageDealt){
      zombie.health=zombie.health-10;
      if(zombie.health<=0 && !zombie.dead){
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        enemyAnimation.speed=zombie.deathAnimationSpeed;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        zombie.dead=true;
      }
    }
}
