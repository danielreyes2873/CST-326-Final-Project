using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [Header("Particle Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    [Header("Tranforms")]
    public Transform muzzleLocation;

    [Header("Rotations")]
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    
    [Header("Hipfire Recoil")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    [Header("Recoil Settings")]
    public float snappiness;
    public float returnSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        muzzleFlash.transform.position = muzzleLocation.transform.position;
        muzzleFlash.transform.forward = muzzleLocation.transform.forward;
        
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
    
}
