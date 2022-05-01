using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraRecoil : MonoBehaviour
{
    [Header("Gun Component")]
    public Gun mainGun;
    
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
        recoilX = mainGun.recoilX;
        recoilY = mainGun.recoilY;
        recoilZ = mainGun.recoilZ;

        snappiness = mainGun.snappiness;
        returnSpeed = mainGun.returnSpeed;
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}