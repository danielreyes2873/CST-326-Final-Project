using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [Header("Main Menu Canvas")]
    public Canvas mainMenuCanvas;
    
    [Header("Levels To Load")] 
    //Name of the first level scene
    public string startNewGame;
    public AudioClip startNewGameGunSound;
    
    
    private AudioSource myAudioSource;
    
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        //Start background music
        myAudioSource.Play();
    }

    //Start button function
    public void StartGame()
    {
        //Stops background music
        myAudioSource.Stop();
        //plays "gunshot soundClip"
        myAudioSource.PlayOneShot(startNewGameGunSound);
        //removes canvas and reveals black screen
        mainMenuCanvas.gameObject.SetActive(false);

        //load up first level
        StartCoroutine(LoadNextScene());

        // SceneManager.LoadScene(startNewGame);
    }


    IEnumerator LoadNextScene()
    {
        //wait 4 seconds, this is enough time for the "gunshot clip" to finish
        yield return new WaitForSeconds(4);
        //load up first level
        SceneManager.LoadScene(startNewGame);
    }



    public void ExitGame()
    {
        Application.Quit();
    }
    


}
