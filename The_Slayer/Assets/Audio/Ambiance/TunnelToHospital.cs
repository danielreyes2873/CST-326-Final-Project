using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TunnelToHospital : MonoBehaviour
{
    
    
    
    public GameObject ambianceSound;
    public AudioClip hospitalSound;
    public AudioMixerGroup hospitalMixer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !ambianceSound.GetComponent<AmbianceAudio>().isInHospital)
        {
            ambianceSound.GetComponent<AmbianceAudio>().isInHospital = true;
            ambianceSound.GetComponent<AudioSource>().clip = hospitalSound;
            ambianceSound.GetComponent<AudioSource>().outputAudioMixerGroup = hospitalMixer;
            ambianceSound.GetComponent<AudioSource>().Play();
        }
    }
}
