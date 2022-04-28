using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    private int health=50;
    public float speed=0.5f;
    public int strength;
    public int wave;
    public bool dead=false;
    private float regularSpeed = 0.3f;
    private float regularAnimationSpeed = 2.0f;
    private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 1.5f;
    private float activeDistance = 5f;
    public bool active = false;
    public bool gotShot = false;
    public List<GameObject> powerupList;
    public GameObject bloodHead;
    public GameObject headExplode;
    public GameObject bloodExplode;
    
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;
    
    void Start()
    {
        enemyAnimation=this.GetComponent<Animator>();
        agent=this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;
        setStats(wave-1);
    }

    void Update()
    {
        if(!dead){
          Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
          if(Vector3.Distance(this.transform.position,playerPosition)<activeDistance || active || gotShot){
            active=true;
            ActiveEnemy(); 
          }
        }
    }

    public void decrementHealth(int damageDealt){
      if(!dead){
        health=health-damageDealt;
        gotShot=true;
        if(health<=0){
            dead=true;
            Death();
        }
      }
    }

    public void ActiveEnemy(){
            if(GameObject.FindWithTag("Player")!=null){
                Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
                agent.SetDestination(playerPosition);

            if(Vector3.Distance(this.transform.position,playerPosition)<=attackDistance){
                //makes enemy face the player
                Vector3 direction = playerPosition-this.transform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);

                enemyAnimation.speed=regularAnimationSpeed;
                enemyAnimation.SetTrigger("Attack");
            }
            else{
                enemyAnimation.SetTrigger("Walk");
                agent.speed = regularSpeed;
            }
            }
    }

    public void setStats(int wave){
      health +=  wave * 10;
      strength += wave * 10;
      regularSpeed+=0.15f;
      regularAnimationSpeed+=0.15f;
    }

    public bool willSpawn(){
      int willSpawn = Random.Range(0,15);
      if(willSpawn<=5){
        return true;
      }
      else{
        return false;
      }
    }
    public void Death(){
        this.GetComponent<CapsuleCollider>().enabled = false;
        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        if(willSpawn()){
          spawnPowerup();
        }
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        enemyAnimation.speed=deathAnimationSpeed;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,10f);
        dead=true;
    }

    public void Headshot(){
      headExplode.SetActive(true);
      bloodExplode.SetActive(true);
      bloodExplode.transform.parent=null;
      enemyAnimation.SetTrigger("Walk");
      enemyAnimation.SetTrigger("Death");
      Invoke("activate",1f);
    }

    public void activate(){
      bloodHead.SetActive(true);
    }

    public void spawnPowerup(){
        int spawn = Random.Range(0,powerupList.Count);
        Vector3 powerupPosition = new Vector3(0f,0.7f,0f);
        Instantiate(powerupList[spawn],this.transform.position+powerupPosition,this.transform.rotation);
    }
}