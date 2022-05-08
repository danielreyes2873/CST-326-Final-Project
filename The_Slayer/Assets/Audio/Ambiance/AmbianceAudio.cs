using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceAudio : MonoBehaviour
{

    [Header("Linked to Player UI to grab gameIsOver")]
    public GameObject playerUI;

    private AudioSource myAudioSource;

    public bool isInHospital = true;

    // Start is called before the first frame update
    void Start()
    {
        isInHospital = true;
        myAudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUI.GetComponentInChildren<PlayerStats>().gameIsOver)
        {
            myAudioSource.Stop();
        }
    }
}
