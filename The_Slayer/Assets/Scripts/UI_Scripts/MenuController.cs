using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [Header("BlackScreen")] 
    public GameObject blackScreen;

    [Header("Levels To Load")]
    //Name of the first level Scene
    public string startNewGame;
    //Name of credits scene
    public string creditsScene;
    
    public AudioClip startNewGameGunSound;

    private AudioSource myAudioSource;


        // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        //Start background music
        myAudioSource.Play();
    }


    public void StartGame()
    {
        //Stops background music
        myAudioSource.Stop();
        //plays "gunshot soundClip"
        myAudioSource.PlayOneShot(startNewGameGunSound);
        //removes canvas and reveals black screen
        blackScreen.SetActive(true);
        
        //load up first level
        StartCoroutine(LoadNextScene());
        
    }


    IEnumerator LoadNextScene()
    {
        //wait 4 seconds, this is enough time for the "gunshot clip" to finish
        yield return new WaitForSeconds(4);
        //load up first level
        SceneManager.LoadScene(startNewGame);
    }


    public void LoadCreditScene()
    {
        SceneManager.LoadScene(creditsScene);
    }


    public void ExitGame()
    {
        Debug.Log("You have exited the game");
        Application.Quit();
    }

}
