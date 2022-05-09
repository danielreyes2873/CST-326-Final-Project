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
    public int additionalZombies=2;
    public int currentWave=1;
    private int enemiesSpawnable;
    public float timeBetweenWaves=8f;
    public float timeBetweenSpawns=2f;
    public int maxZombies=20;
    public List<GameObject> enemyList;
    public TextMeshProUGUI zombiesLeft;
    public TextMeshProUGUI wave;
    // Start is called before the first frame update
    void Start()
    {
        enemiesSpawnable=3;
        wave.text=currentWave.ToString();
        zombiesLeft.text=zombieCount.ToString();
        foreach (GameObject spawnpoint in GameObject.FindGameObjectsWithTag("Spawnpoint")){
           spawnPoints.Add(spawnpoint.GetComponent<Transform>());    
        }
        StartCoroutine(Spawn(WaveCount));
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
        if(zombieCount<=0){
            setNextWave();
        }
    }

    public void setNextWave(){
            PlayerStats.totalRoundsSurvived++;
            WaveCount = Mathf.Clamp(WaveCount + additionalZombies, 0, maxZombies);
            zombieCount=WaveCount;
            zombiesLeft.text=zombieCount.ToString();
            currentWave++;
            wave.text=currentWave.ToString();
            if(currentWave<3){
                enemiesSpawnable=3;
            }
            else{
                enemiesSpawnable=4;
            }
            StartCoroutine(Spawn(WaveCount));
    }
}
