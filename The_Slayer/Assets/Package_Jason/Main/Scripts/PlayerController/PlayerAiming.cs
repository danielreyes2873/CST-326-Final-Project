using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAiming : MonoBehaviour
{
    [Header("Primary")]
    public GameObject mainGun;
    [Header("Secondary")]
    public GameObject secondaryGun;
    
    [Header("Weapon Sway")]
    public float smooth;
    public float swayMultiplier;
    
    [Header("Rotations")]
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [Header("Components")]
    private Gun mainGunComponent;
    private CameraRecoil _cameraRecoil;

    [Space(10)]
    public Transform playerCamera;
    public float mouseSensitivity = 2.8f;
    private float cameraPitch = 0.0f;

    public bool mouseSmoothing = false;
    [Range(0.0f, 0.5f)] public float mouseSmoothTime = 0.03f;
    
    public bool lockCursor = true;
    
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    public int range = 50;
    public KeyCode fireKey = KeyCode.Mouse0;

    // Start is called before the first frame update
    void Start()
    {
        mainGunComponent = mainGun.GetComponent<Gun>();
        _cameraRecoil = GetComponentInChildren<CameraRecoil>();
        
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            Shoot();
        }
        
        UpdateMouseLook();
        WeaponSway();
    }

    void WeaponSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;
        
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;
        
        mainGun.transform.localRotation = Quaternion.Slerp(mainGun.transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    void Shoot()
    {
        _cameraRecoil.RecoilFire();
        mainGunComponent.RecoilFire();
        mainGunComponent.PlayMuzzleFlash();
        
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // Environment particle effect wont play when shooting anything that isn't the environment
            if(hit.transform.tag!="Enemy" && hit.transform.tag!="Limb" && hit.transform.tag!="Head" && hit.transform.tag!="HitBox")
            {
                GameObject impact = Instantiate(mainGunComponent.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impact.GetComponent<ParticleSystem>().Play();
                Destroy(impact, 2f);
            }
        }
    }

    void UpdateMouseLook()
    {
        if (mouseSmoothing)
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
            cameraPitch -= currentMouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -88.5f, 88.5f);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }
        else
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            cameraPitch -= mouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -88.5f, 88.5f);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
            transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
        }
        
    }
}
