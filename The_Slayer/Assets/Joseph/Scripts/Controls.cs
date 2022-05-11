using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
        public GameObject controlPanel;

    void Start(){
        // controlPanel = GameObject.Find("Panel");
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.H))
        {
            controlPanel.SetActive(true);
        }
        else
        {
            controlPanel.SetActive(false);
        }
    }
}
