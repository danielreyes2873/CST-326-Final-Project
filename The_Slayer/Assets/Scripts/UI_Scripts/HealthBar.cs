using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
// using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


    //Health Bar slider
    [Header("Health Bar Fill")]
    public Slider myHealthSlider;
    //Health Bar Text Value
    [Header("Health Bar Text Value")]
    public TextMeshProUGUI myHealthValue;

    private void Start()
    {

    }

    private void OnEnable()
    {
        EventHandler.AfterPlayerRigister += OnPlayerRigister;
    }

    private void OnDisable()
    {
        EventHandler.AfterPlayerRigister -= OnPlayerRigister;
    }

    private void OnPlayerRigister()
    {
        // //checking if character is set up
        // if (GameManager.Instance.playerStats.characterData == null)
        // {
        //     Debug.Log("the character data is not set up! (GameManager.Instance.playerStats");
        // }
        // //Set current player's health to max player's health at the start of the game
        // GameManager.Instance.playerStats.currentHealth = GameManager.Instance.playerStats.maxHealth;
        //
        // //Set slider to player's max health at start of game.
        // SetMaxHealth(GameManager.Instance.playerStats.maxHealth);
        //
        // //Have HealthTextValue set to player's current health(maxHealth) at start of game.
        // myHealthValue.text = $"{GameManager.Instance.playerStats.currentHealth}";
    }

    private void Update()
    {
        //Updating Health text value 
        //If health is above 0 display the value, else display "Dead"
        if (GameManager.Instance.playerStats.currentHealth > 0)
        {
            myHealthValue.text = $"{GameManager.Instance.playerStats.currentHealth}";
        }
        else
        {
            myHealthValue.text = "Dead";
        }


        //--------UNCOMMENT TO TEST PLAYER TAKING DAMAGE HERE-------------

        //testing player takes damage or enemy, to then update health bar with correct value.
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDamage(10);
        // }
    }


    //Set slider's maxValue
    public void SetMaxHealth(int health)
    {
        myHealthSlider.maxValue = health;
        myHealthSlider.value = health;
    }

    //Set slider's current value
    public void SetHealth(int health)
    {
        myHealthSlider.value = health;
    }


    //Todo: Remove this function below. Put the function below into "Player" script.
    //Testing player taking damage, update to UI
    private void TakeDamage(int damage)
    {
        GameManager.Instance.playerStats.currentHealth -= damage;
        SetHealth(GameManager.Instance.playerStats.currentHealth);
    }
}
