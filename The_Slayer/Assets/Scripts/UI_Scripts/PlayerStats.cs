using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{


    [Header("GameOver Display")]
    //GameOver Display Panel
    public GameObject gameOverDisplay;


    [Header("My Audio Source")]
    //To play game over song
    private AudioSource myAudioSource;

    public AudioClip gameOverSong;

    [Header("Player stats - end of game display")]
    public static int totalPlayerScore;
    public static int totalPlayerKills;
    public static int totalPlayerHeadshots;
    public static int totalRoundsSurvived;


    [Header("GUI text elements")]
    //GUI hookup elements
    public TextMeshProUGUI displayTotalPlayerScore;
    public TextMeshProUGUI displayTotalPlayerKills;
    public TextMeshProUGUI displayTotalPlayerHeadshots;
    public TextMeshProUGUI displayTotalRoundsSurvived;

    [Header("Disable health and ammo UI after death")]
    //Health and ammo section will be hidden after death
    public GameObject healthSection;
    public GameObject ammoSection;
    
    
    //when the player presses 'TAB' display stats
    [Header("Press TAB show stats")] 
    public GameObject tabStatsUI;

    [Header("GUI text elements (FOR TAB)")]
    public TextMeshProUGUI TABdisplayTotalPlayerScore;
    public TextMeshProUGUI TABdisplayTotalPlayerKills;
    public TextMeshProUGUI TABdisplaytotalPlayerHeadshots;

    //game over boolean
    public bool gameIsOver;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverDisplay.SetActive(false);
        myAudioSource = GetComponent<AudioSource>();

        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if player dies (0 health), run game over function
        if (GameManager.Instance.playerStats.currentHealth <= 0 && gameIsOver == false)
        {
            GameOver();
        }
        
        
        //Display player score, kills, and headshots (FOR TAB)
        TABdisplayTotalPlayerScore.text = $"{totalPlayerScore}";
        TABdisplayTotalPlayerKills.text = $"{totalPlayerKills}";
        TABdisplaytotalPlayerHeadshots.text = $"{totalPlayerHeadshots}";
        
        
        //To display TAB PLAYER STATS, if player presses/holds tab.
        if (Input.GetKey(KeyCode.Tab) && !gameIsOver)
        {
            tabStatsUI.SetActive(true);
        }
        else
        {
            tabStatsUI.SetActive(false);
        }
        
        //testing player getting a kill.
        if (Input.GetKeyDown(KeyCode.K))
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


        //Display total stats (update text)
        displayTotalRoundsSurvived.text = $"You Survived {totalRoundsSurvived} Rounds";
        displayTotalPlayerScore.text = $"{totalPlayerScore}";
        displayTotalPlayerKills.text = $"{totalPlayerKills}";
        displayTotalPlayerHeadshots.text = $"{totalPlayerHeadshots}";
        
        
        //display the gameOver panel
        gameOverDisplay.SetActive(true);
        
        
        //play game over song
        myAudioSource.PlayOneShot(gameOverSong);
        
        //Load up main menu after death (35 seconds)
        StartCoroutine(GameIsOverRestarting());

    }
    
    
    
    //Load up main menu after 35 seconds have passed after game over is displayed
    IEnumerator GameIsOverRestarting()
    {
        //can press 'esc' to pass through song and go straight to main menu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        //song is ~30 seconds long
        yield return new WaitForSeconds(35);
        SceneManager.LoadScene("MainMenu");
    }
    
    
    
}
