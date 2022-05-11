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

    public static event Action ChangeWeapon;
    public static void CallChangeWeapon()
    {
        ChangeWeapon?.Invoke();
    }

    public static event Action Reloading;
    public static void CallReloading()
    {
        Reloading?.Invoke();
    }

    public static event Action OpenInventory;
    public static void CallInventory()
    {
        OpenInventory?.Invoke();
    }

    public static event Action CloseInventory;
    public static void CallCloseInventory()
    {
        CloseInventory?.Invoke();
    }

    public static event Action PlayerDead;
    public static void CallPlayerDead()
    {
        PlayerDead?.Invoke();
    }

    public static event Action AfterPlayerRigister;
    public static void CallPlayerRigister()
    {
        AfterPlayerRigister?.Invoke();
    }
}
