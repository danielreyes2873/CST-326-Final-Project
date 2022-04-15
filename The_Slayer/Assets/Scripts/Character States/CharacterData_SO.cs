using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Characters States")]
public class CharacterData_SO : ScriptableObject
{
    //if you do not need any state, just leave it as 0

    [Header("State Info")]
    public int currentHealth;
    public int maxHealth;

    public int defense;

    [Header("Enemy Only")]
    public int killPoint;
    public int attack;

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;

    public int currentExp;
    public int maxExp;


    //TODO 
    public void levelUp()
    {
        //to make sure will not level up after getting maxlevel
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
    }

    public void UpdateExp(int exp)
    {
        currentExp += exp;
        if(currentExp > maxExp)
        {
            levelUp();
        }
    }
}
