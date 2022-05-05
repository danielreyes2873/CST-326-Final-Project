using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//I will make a video to explain how to use it
public class EventHandler
{
    public static event Action<int> TakeDamageEvent;
    public static void CallPlayerTakeDamageEvent(int damage)
    {
        TakeDamageEvent?.Invoke(damage);
    }

    public static event Action<int> EnemyDeadEvent;
    public static void CallAfterEnemyDead(int score)
    {
        EnemyDeadEvent?.Invoke(score);
    }
}
