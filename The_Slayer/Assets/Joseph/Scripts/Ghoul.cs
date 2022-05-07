using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ghoul : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health=30;
    private float speed=1.0f;
    private int strength=5;
    private float regularSpeed = 3.5f;
    private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 1.5f;
    public bool dead=false;
    private float stopTimer=0.0f;
    private float timeToStop=0.3f;
    private bool Attacking=false;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;
    Vector3 playerPosition;

    void Start(){
        enemyAnimation=this.GetComponent<Animator>();
    }

    void Update(){
        if(!dead){
            
        if(GameObject.FindWithTag("Player")!=null){
           playerPosition = GameObject.FindWithTag("Player").transform.position;
        // agent.SetDestination(playerPosition);

        if(Vector3.Distance(this.transform.position, playerPosition)<attackDistance){
            agent.updatePosition=false;
            agent.SetDestination(playerPosition);
            Vector3 direction = playerPosition-this.transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
            enemyAnimation.SetTrigger("Attack");
            Attacking=true;
        }
        else{
            if(Attacking==true){
                agent.speed=0;
                stopTimer+=Time.deltaTime;
                if(stopTimer>=timeToStop){
                    Attacking=false;
                    stopTimer=0.0f;
                }
            }
            else{
            agent.updatePosition=true;
            agent.SetDestination(playerPosition);
            enemyAnimation.SetTrigger("Run");
            agent.speed=regularSpeed;
            }
        }
        }
        }
    }
    public void decrementHealth(int weaponStrength){
      health=health-10;
      if(health<=0 &&!dead){
        this.GetComponent<CapsuleCollider>().enabled = false;
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        enemyAnimation.speed=deathAnimationSpeed;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        dead=true;
      }
    }
}