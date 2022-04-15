using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CharacterStats playerStats;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void RigisterPlayer(CharacterStats player)
    {
        playerStats = player;
    }
}
