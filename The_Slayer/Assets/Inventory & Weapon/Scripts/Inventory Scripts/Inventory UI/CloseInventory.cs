using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory : MonoBehaviour
{
    public void CloseBackpack()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
