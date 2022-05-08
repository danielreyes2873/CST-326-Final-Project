using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HospitalToTunnelSound : MonoBehaviour
{

    public GameObject ambianceSound;
    public AudioClip tunnelSystemSound;
    public AudioMixerGroup tunnelMixer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ambianceSound.GetComponent<AmbianceAudio>().isInHospital)
        {
            ambianceSound.GetComponent<AmbianceAudio>().isInHospital = false;
            ambianceSound.GetComponent<AudioSource>().clip = tunnelSystemSound;
            ambianceSound.GetComponent<AudioSource>().outputAudioMixerGroup = tunnelMixer;
            ambianceSound.GetComponent<AudioSource>().Play();
        }
    }
}
