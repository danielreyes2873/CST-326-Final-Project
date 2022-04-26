using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ghoul : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health=30;
    public float speed=0.3f;
    public int strength=5;
    public bool isDamaged=false;
    private float regularSpeed = 2.0f;
    private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 0.7f;
    public bool dead=false;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    void Start(){
        enemyAnimation=this.GetComponent<Animator>();
    }

    void Update(){
        if(!dead){
        if(GameObject.FindWithTag("Player")!=null){
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        agent.SetDestination(playerPosition);

        if(Vector3.Distance(this.transform.position, playerPosition)<attackDistance){
            Vector3 direction = playerPosition-this.transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
            enemyAnimation.SetTrigger("Attack");
            isDamaged=true;
            
        }
        else{
            enemyAnimation.SetTrigger("Run");
            agent.speed=regularSpeed;
        }
        }
        }
    }
    public void decrementHealth(int weaponStrength){
      health=health-10;
      if(health<=0 &&!dead){
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        enemyAnimation.speed=deathAnimationSpeed;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        dead=true;
      }
    }

    bool AnimationPlaying(string name){
        return enemyAnimation.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}