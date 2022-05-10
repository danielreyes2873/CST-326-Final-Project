using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class RigisterPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterStats playerStats;
    void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        GameManager.Instance.RigisterPlayer(playerStats);
    }

}
