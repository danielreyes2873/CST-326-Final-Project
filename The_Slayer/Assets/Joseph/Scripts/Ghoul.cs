using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ghoul : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health;
    public int maxHealth;
    public float speed=0.3f;
    public int strength;
    public bool isDamaged=false;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;
    // public Slider slider;

    void Start(){
        health=30;
        maxHealth=30;
        enemyAnimation=this.GetComponent<Animator>();
        enemyAnimation.speed=1.0f;
    }

    void Update(){
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        agent.SetDestination(GameObject.FindWithTag("Player").transform.position);

        // if(health<=maxHealth/2 && !isDamaged){
        //     enemyAnimation.SetTrigger("Damaged");
        //     enemyAnimation.speed=1.3f;
        //     isDamaged=true;
        //     Invoke("changeSpeed",1.5f);
        // }

        if(Vector3.Distance(this.transform.position, playerPosition)<0.7f){
            enemyAnimation.SetTrigger("Attack");
            enemyAnimation.speed=1.0f;
            isDamaged=true;
            
        }
        else{
            // if(!isDamaged){
            // // enemyAnimation.SetTrigger("Walk");
            // agent.speed = 0.2f;
            // }
            // else{
                enemyAnimation.SetTrigger("Run");
                agent.speed=3.0f;
            // }
        }
    }
    public void decrementHealth(int weaponStrength){
      health=health-10;
      if(health<=0){
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        Destroy(this.gameObject,0.3f);
      }
    }

    bool AnimationPlaying(string name){
        return enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}