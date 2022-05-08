using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public int zombieCount=5;
    public int WaveCount=5;
    public int currentWave=1;
    public int enemiesSpawnable=1;
    public float timeBetweenWaves=8f;
    public float timeBetweenSpawns=2f;
    public List<GameObject> enemyList;
    public TextMeshProUGUI zombiesLeft;
    public TextMeshProUGUI wave;
    // Start is called before the first frame update
    void Start()
    {
        wave.text = currentWave.ToString();
        zombiesLeft.text=zombieCount.ToString();
        foreach (GameObject spawnpoint in GameObject.FindGameObjectsWithTag("Spawnpoint")){
           spawnPoints.Add(spawnpoint.GetComponent<Transform>());    
        }
        StartCoroutine(Spawn(WaveCount));
    }
    void Update()
    {
        if(zombieCount<=0){
            PlayerStats.totalRoundsSurvived++;
            WaveCount+=5;
            zombieCount=WaveCount;
            zombiesLeft.text=zombieCount.ToString();
            currentWave++;
            wave.text=currentWave.ToString();
            if(currentWave>2){
                enemiesSpawnable=3;
            }
            if(currentWave>4){
                enemiesSpawnable=3;
            }
            StartCoroutine(Spawn(WaveCount));
        }
    }
    IEnumerator Spawn(int zombies){
        yield return new WaitForSeconds(timeBetweenWaves);
        for(int x=0;x<zombies;x++){
          int spawn = Random.Range(0,spawnPoints.Count);
          int zombieType = Random.Range(0,enemiesSpawnable);
          Instantiate(enemyList[zombieType],spawnPoints[spawn].position,spawnPoints[spawn].rotation);
          yield return new WaitForSeconds(timeBetweenSpawns);
        }   
    }
    public void UpdateZombiesLeft(){
        zombiesLeft.text=zombieCount.ToString();
    }    
    public void zombieKilled(){
        PlayerStats.totalPlayerKills++;
        zombieCount--;
        UpdateZombiesLeft();
    }
}
