using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("File in here")]
    public CharacterData_SO characterTemplateData;

    [Header("Leave empty, it will Instantiate after Start")]
    public CharacterData_SO characterData;

    public int maxHealth { get => characterData?.maxHealth ?? 0; set => characterData.maxHealth = value; }
    public int currentHealth { get => characterData?.currentHealth ?? 0; set => characterData.currentHealth = value; }

    void Awake()
    {
        if (characterTemplateData != null)
        {
            characterData = Instantiate(characterTemplateData);
        }
        else
        {
            Debug.Log("You missed characterTemplateData");
        }
    }

    public void TakeDamage(CharacterStats attacker, CharacterStats defender)
    {

    }
}
