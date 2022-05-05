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

    private int headshotPoints=20;
    private int normalPoints=10;

    private float regularSpeed = 0.3f;
    private float regularAnimationSpeed = 2.0f;
    private float regularAttackAnimationSpeed = 2.0f;
    private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 1.5f;

    // private float activeDistance = 5f;

    // public bool active = false;
    // public bool gotShot = false;

    public List<GameObject> powerupList;
    public GameObject bloodHead;
    public GameObject headExplode;
    public GameObject bloodExplode;

    [Header("Sound")]
    public float breathTime;
    public float breath;

    public AudioSource source1;
    public AudioSource source2;
    public AudioClip footstep;
    public AudioClip bodyshot;
    public AudioClip headshot;

    public List<AudioClip> attackClips;
    public List<AudioClip> breathingClips;

    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;
    
    void Start()
    {
        breathTime=Random.Range(5f,15f);
        enemyAnimation=this.GetComponent<Animator>();
        agent=this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;
        setStats(wave-1);
    }

    void Update()
    {
        if(!dead){
          breath+=Time.deltaTime;

          if(breath>breathTime){
            playBreath();
            breath=0.0f;
          }
          Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
          // if(Vector3.Distance(this.transform.position,playerPosition)<activeDistance || active || gotShot){
          //   active=true;
            ActiveEnemy(); 
          // }
        }
    }

    public void decrementHealth(int damageDealt){
      if(!dead){

        PlayerStats.totalPlayerScore+=normalPoints;

        health=health-damageDealt;
        // gotShot=true;
        source2.pitch=Random.Range(0.9f,1.1f);
        source2.clip=bodyshot;
        source2.Play();
        if(health<=0){
            dead=true;
            Death();
        }
      }
    }

    public void ActiveEnemy(){
            if(GameManager.Instance.playerStats.currentHealth>0){
                Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
                agent.SetDestination(playerPosition);

            if(Vector3.Distance(this.transform.position,playerPosition)<=attackDistance){
                //makes enemy face the player
                Vector3 direction = playerPosition-this.transform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
                enemyAnimation.speed=regularAttackAnimationSpeed;
                enemyAnimation.SetTrigger("Attack");
            }
            else{
                enemyAnimation.SetTrigger("Walk");
                agent.speed = regularSpeed;
                enemyAnimation.speed=regularAnimationSpeed;
            }
            }
            else{
              enemyAnimation.SetTrigger("Walk");
              enemyAnimation.speed=1.0f;
            }
    }

    public void setStats(int wave){
      health +=  wave * 10;
      strength += wave * 10;

      regularSpeed = Mathf.Clamp(regularSpeed+= wave*0.5f,0.3f,1.5f);
      regularAnimationSpeed = Mathf.Clamp(regularAnimationSpeed+= wave*0.5f,2.0f,2.3f);
      // regularSpeed+= wave * 1.5f;
      // regularAnimationSpeed+= wave * 1.5f;
    }

    public bool willSpawn(){
      int willSpawn = Random.Range(0,100);
      if(willSpawn<=5){
        return true;
      }
      else{
        return false;
      }
    }
    public void Death(){
        this.GetComponent<CapsuleCollider>().enabled = false;

// <<<<<<< HEAD
//         Collider[] coChildren = GetComponentsInChildren<Collider>();
//          foreach (var cCollider in coChildren)
//          {
//              cCollider.enabled = false;
//          }
// =======
        // Disables all of the relative colliders within the enemy prefab
        Collider[] coChildren = GetComponentsInChildren<Collider>();
        foreach (var cCollider in coChildren)
        {
            cCollider.enabled = false;
        }
// >>>>>>> main

        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        if(willSpawn()){
          spawnPowerup();
        }
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        enemyAnimation.speed=deathAnimationSpeed;
        enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,10f);
        dead=true;
    }

    public void Headshot(){
      PlayerStats.totalPlayerScore+=headshotPoints;
      PlayerStats.totalPlayerHeadshots++;
      source2.pitch=Random.Range(0.9f,1.1f);
      source2.clip=headshot;
      source2.Play();

      headExplode.SetActive(true);
      bloodExplode.SetActive(true);
      Destroy(bloodExplode,3f);
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

    public void playFootstep(){
      source1.pitch=Random.Range(0.9f,1.1f);
      source1.clip=footstep;
      source1.Play();
    }

    public void playAttack(){
      int clip = Random.Range(0,attackClips.Count);
      source2.pitch=Random.Range(0.9f,1.1f);
      source2.clip=attackClips[clip];
      source2.Play();
    }
    public void playBreath(){
      source2.pitch=Random.Range(0.9f,1.1f);
      breathTime=Random.Range(10f,15f);
      int clip = Random.Range(0,breathingClips.Count);
      source2.clip=breathingClips[clip];
      source2.Play();
    }
}