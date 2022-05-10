using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ghoul : MonoBehaviour
{
    [Header("Enemy Attributes")]
    private int health=20;
    private int strength=5;
    private float regularSpeed = 3.5f;
    // private float deathAnimationSpeed = 0.7f;
    private float attackDistance = 1.5f;
    public bool dead=false;
    private float stopTimer=0.0f;
    private float timeToStop=0.3f;
    private bool Attacking=false;
    private int wave;
    private bool isJumping=false;
    private int ghoulPoints=20;
    public UnityEngine.AI.NavMeshAgent agent;
    Animator enemyAnimation;
    Vector3 playerPosition;

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

    private float jumpC=2.8f;

    void Start(){
        setKin(true);
        breathTime=Random.Range(5f,15f);
        enemyAnimation=this.GetComponent<Animator>();
        wave = GameObject.Find("SpawnPoints").GetComponent<Spawner>().currentWave;
        setStats(wave-1);
    }

    void Update(){
        if(!dead){

        breath+=Time.deltaTime;

          if(breath>breathTime){
            playBreath();
            breath=0.0f;
        }
            
        if(GameObject.FindWithTag("Player")!=null){
           playerPosition = GameObject.FindWithTag("Player").transform.position;

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

            if(agent.isOnOffMeshLink){
                      Jump();
            }
            else if(isJumping==true && agent.isOnOffMeshLink!=true){
                      Landed();
            }
            else if(Attacking==true){
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
        PlayerStats.totalPlayerScore+=ghoulPoints;
      health=health-weaponStrength;
      if(health<=0 &&!dead){
         Dead();
      }
    }

    public void Dead(){
        this.GetComponent<CapsuleCollider>().enabled = false;
        Collider[] coChildren = GetComponentsInChildren<Collider>();
        foreach (var cCollider in coChildren)
        {
            cCollider.isTrigger = false;
        }

        GameObject.Find("SpawnPoints").GetComponent<Spawner>().zombieKilled();
        agent.speed=0.0f;
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // enemyAnimation.speed=deathAnimationSpeed;
        // enemyAnimation.SetTrigger("Death");
        Destroy(this.gameObject,5f);
        dead=true;
        EnableRagdoll();
    }

    public void playFootstep(){
      source1.pitch=Random.Range(0.9f,1.1f);
      source1.clip=footstep;
      source1.Play();
    }

    public void playAttack(){
      if(!dead){
      int clip = Random.Range(0,attackClips.Count);
      source2.pitch=Random.Range(1.05f,1.1f);
      source2.clip=attackClips[clip];
      source2.Play();
      }
    }
    public void playBreath(){
      source2.pitch=Random.Range(1.2f,1.3f);
      breathTime=Random.Range(10f,15f);
      int clip = Random.Range(0,breathingClips.Count);
      source2.clip=breathingClips[clip];
      source2.Play();
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
      source2.Play();;
      agent.speed=jumpC;
      }
    }

    public void playerDie(){
        source2.enabled=false;
        source1.enabled=false;
        dead=true;
    }


    public void setStats(int wave){
      health +=  (wave-2) * 5;
      strength += (wave-2) * 5;
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