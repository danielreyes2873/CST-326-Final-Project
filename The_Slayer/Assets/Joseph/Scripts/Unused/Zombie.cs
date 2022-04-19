using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie", menuName = "Zombie")]
public class Zombie : ScriptableObject{
    
    public new string name;
    public int health;
    public int strength;
    public bool dead;
    public float regularSpeed;
    public float regularAnimationSpeed;
    public float attackSpeed;
    public float deathAnimationSpeed;
    public float attackDistance;


    public void IncreaseHealth(){
        health = health + 10;
    }

    public void IncreaseStrength(){
        strength = strength + 5;
    }
}
