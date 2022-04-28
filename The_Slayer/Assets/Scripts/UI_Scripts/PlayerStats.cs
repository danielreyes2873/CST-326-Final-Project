using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    
    [Header("GameOver Display")] 
    //GameOver Display panel
    public GameObject gameOverDisplay;

    [Header("My Audio Source")] 
    //To play game over song
    private AudioSource myAudioSource;

    public AudioClip gameOverSong;

    [Header("Player Stats - end of game display")]
    //player stats
    public static int totalPlayerScore;
    public static int totalPlayerKills;
    public static int totalPlayerHeadshots;
    public static int totalRoundsSurvived;

    [Header("GUI text elements")]
    //GUI Hookup Elements
    public TextMeshProUGUI displayTotalPlayerScore;
    public TextMeshProUGUI displayTotalPlayerKills;
    public TextMeshProUGUI displayTotalPlayerHeadshots;
    public TextMeshProUGUI displayTotalRoundsSurvived;

    [Header("Disable health and ammo UI after death")]
    //Health and ammo section will be hidden after death
    public GameObject healthSection;
    public GameObject ammoSection;

    [Header("Player's current score(Player's current cash)")]
    //Will count as money
    public int currentPlayerScore;

    //when the player presses 'TAB' display stats
    [Header("Press TAB show stats")] 
    public GameObject tabStatsUI;

    [Header("GUI text elements (FOR TAB)")]
    //GUI Hookup Elements
    public TextMeshProUGUI TABdisplayTotalPlayerScore;
    public TextMeshProUGUI TABdisplayTotalPlayerKills;
    public TextMeshProUGUI TABdisplayTotalPlayerHeadshots;


    private bool gameIsOver;


    private void Start()
    {
        gameOverDisplay.SetActive(false);
        myAudioSource = GetComponent<AudioSource>();

        gameIsOver = false;
    }

    private void Update()
    {
        //if player dies (0 health), run game over function
        if (GameManager.Instance.playerStats.currentHealth <= 0)
        {
            GameOver();
        }
        
        //Display total rounds survived
        displayTotalRoundsSurvived.text = $"You Survived {totalRoundsSurvived} Rounds";

        //Display player score, kills, and headshots
        displayTotalPlayerScore.text = $"{totalPlayerScore}";
        displayTotalPlayerKills.text = $"{totalPlayerKills}";
        displayTotalPlayerHeadshots.text = $"{totalPlayerHeadshots}";
        
        
        //Display player score, kills, and headshots (FOR TAB)
        TABdisplayTotalPlayerScore.text = $"{totalPlayerScore}";
        TABdisplayTotalPlayerKills.text = $"{totalPlayerKills}";
        TABdisplayTotalPlayerHeadshots.text = $"{totalPlayerHeadshots}";
        

        
        //To display TAB PLAYER STATS, if player presses/holds tab
        if (Input.GetKey(KeyCode.Tab) && !gameIsOver)
        {
            tabStatsUI.SetActive(true);
        }
        else
        {
            tabStatsUI.SetActive(false);
        }

        //Testing player getting a kill.
        if (Input.GetKeyDown(KeyCode.I))
        {
            totalPlayerScore += 500;
            totalPlayerKills += 1;
        }
        
    }


    public void GameOver()
    {
        gameIsOver = true;
        
        //hide the health and ammo section
        healthSection.SetActive(false);
        ammoSection.SetActive(false);
        
        //display the gameOver panel
        gameOverDisplay.SetActive(true);
        
        //play game over song
        myAudioSource.PlayOneShot(gameOverSong);


        //Load up main menu after death (35 seconds)
        StartCoroutine(GameIsOverRestarting());
    }


    //Load up mainMenu after 15 seconds have passed after game over is displayed
    IEnumerator GameIsOverRestarting()
    {

        //Can press 'esc' to pass through song, and go straight to main menu.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        //song is ~30 seconds long
        yield return new WaitForSeconds(35f);
        SceneManager.LoadScene("MainMenu");
    }
    
    
    
}
