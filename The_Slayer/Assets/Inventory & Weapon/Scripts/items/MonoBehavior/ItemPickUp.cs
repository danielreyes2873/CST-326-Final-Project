using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public itemData_SO itemTemp;
    [Header("Leave this empty")]
    public itemData_SO itemData;

    private void Start()
    {
        itemData = Instantiate(itemTemp);
    }
}
