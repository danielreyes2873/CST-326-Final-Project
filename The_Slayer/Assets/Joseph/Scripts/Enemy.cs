using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    private int health=40;
    public float speed=0.5f;
    public int strength;
    public int wave;
    public bool dead=false;
    private bool isRunning = false;
    private bool isJumping=false;

    private int headshotPoints = 20;
    private int normalPoints = 10;

    private float regularSpeed = 0.9f;
    private float runningSpeed = 2.6f;
    private float regularAnimationSpeed = 2.0f;
    private float regularAttackAnimationSpeed = 2.0f;
    // private float runningAnimationSpeed = 2.0f;
    // private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 1.5f;

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
    public AudioClip land;

    public List<AudioClip> attackClips;
    public List<AudioClip> breathingClips;

    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;

    Vector3 playerPosition;

    private float jumpC=2.8f;
    
    void Start()
    {
        setKin(true);
        breathTime=Random.Range(5f,15f);
        enemyAnimation=this.GetComponent<Animator>();
        agent=this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;
        setStats(wave-1);

        if(wave>=5){
          isRunning=true;
          attackDistance=2.5f;
          agent.baseOffset=0.0f;
        }
        else if(wave>=3){
          if(Random.Range(0,5)>=2){
            isRunning=true;
            agent.baseOffset=0.0f;
          }
        }
    }

    void Update()
    {
        if (!dead)
        {
            breath += Time.deltaTime;

          if(breath>breathTime){
            playBreath();
            breath=0.0f;
          }
          if(GameObject.FindWithTag("Player")!=null){
          playerPosition = GameObject.FindWithTag("Player").transform.position;
          agent.SetDestination(playerPosition);
          ActiveEnemy(); 
          }
        }
    }

    public void decrementHealth(int damageDealt){
      if(!dead){
        PlayerStats.totalPlayerScore+=normalPoints;
        health=health-damageDealt;
        source2.pitch=Random.Range(0.9f,1.1f);
        source2.clip=bodyshot;
        source2.Play();
        if(health<=0){
            dead=true;
            Death();
        }
    }

    public void ActiveEnemy(){
            if(GameManager.Instance.playerStats.currentHealth>0){
                if(Vector3.Distance(this.transform.position,playerPosition)<=attackDistance){
                    Vector3 direction = playerPosition-this.transform.position;
                    direction.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
                    enemyAnimation.speed=regularAttackAnimationSpeed;
                    enemyAnimation.SetTrigger("Attack");
                }
                else{
                    if(agent.isOnOffMeshLink){
                      Jump();
                    }
                    else if(isJumping==true && agent.isOnOffMeshLink!=true){
                      Landed();
                    }
                    else if(isRunning){
                      enemyAnimation.SetTrigger("Run");
                      agent.SetDestination(playerPosition);
                      agent.speed = runningSpeed;
                      enemyAnimation.speed=1.0f;
                    }
                    else if(!isRunning){
                      enemyAnimation.SetTrigger("Walk");
                      agent.speed = regularSpeed;
                      enemyAnimation.speed=regularAnimationSpeed;
                    }
              }
            }
            else{
              enemyAnimation.SetTrigger("Walk");
              enemyAnimation.speed=1.0f;
            }
            
    }

    public void setStats(int wave){
      health +=  wave * 7;
      strength += wave * 7;

      if(wave>=5){

      }
      else{
      regularSpeed+= wave * 0.1f;
      regularAnimationSpeed+= wave * 0.1f;
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
            // cCollider.enabled = false;
            cCollider.isTrigger = false;
        }
        // >>>>>>> main

        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // enemyAnimation.speed=deathAnimationSpeed;
        // enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        dead=true;

        EnableRagdoll();
    }

    public void Headshot(){
      
      PlayerStats.totalPlayerScore+=headshotPoints;
      PlayerStats.totalPlayerHeadshots++;
      playHeadLimb();

        headExplode.SetActive(true);
        bloodExplode.SetActive(true);
        Destroy(bloodExplode, 3f);
        bloodExplode.transform.parent = null;

      enemyAnimation.SetTrigger("Walk");
      enemyAnimation.SetTrigger("Death");
      // Invoke("activate",1f);
    }

    public void activate()
    {
        bloodHead.SetActive(true);
    }

    public void playFootstep(){
      if(!dead){
      source1.pitch=Random.Range(0.9f,1.1f);
      source1.clip=footstep;
      source1.Play();
      }
    }

    public void playAttack(){
      if(!dead){
      int clip = Random.Range(0,attackClips.Count);
      source2.pitch=Random.Range(0.9f,1.1f);
      source2.clip=attackClips[clip];
      source2.Play();
      }
    }
    public void playBreath(){
      if(!dead){
      source2.pitch=Random.Range(0.9f,1.1f);
      breathTime=Random.Range(10f,15f);
      int clip = Random.Range(0,breathingClips.Count);
      source2.clip=breathingClips[clip];
      source2.Play();
      }
    }

    public void playHeadLimb(){
      if(!dead){
      source2.pitch=Random.Range(0.8f,0.9f);
      source2.clip=headshot;
      source2.Play();
      }
    }

    public void Jump(){
      if(!dead){
      isJumping=true;
      agent.speed=jumpC;
      enemyAnimation.SetTrigger("Jump");
      enemyAnimation.speed=1.0f;
      }
    }

    public void Landed(){
      if(!dead){
      isJumping=false;
      source2.clip=land;
      source2.Play();
      agent.speed=jumpC;
      }
    }

      public void playerDie(){
        source2.enabled=false;
        source1.enabled=false;
        dead=true;
    }

    public void EnableRagdoll(){
      enemyAnimation.enabled=false;
      setKin(false);
    }

    void setKin(bool boolean){
      Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
      foreach (Rigidbody rb in rbs)
      {
         rb.isKinematic = boolean;
      }
    }
}