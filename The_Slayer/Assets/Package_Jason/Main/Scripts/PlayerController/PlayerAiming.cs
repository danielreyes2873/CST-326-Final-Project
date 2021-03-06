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

    [Header("Camera Movement")]
    public Transform playerCamera;
    public float mouseSensitivity = 2.8f;
    private float cameraPitch = 0.0f;

    public bool mouseSmoothing = false;
    [Range(0.0f, 0.5f)] public float mouseSmoothTime = 0.03f;

    public bool lockCursor = true;

    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    [Space(10)]
    [Tooltip("Range for Raycast")]
    public int range = 50;
    [Tooltip("Key Input")]
    public KeyCode fireKey = KeyCode.Mouse0;
    [Tooltip("Fire rate and reload delay is implemented using a timestamp")]
    private float timeStamp = 0.0f;

    [Header("Components")]
    [SerializeField] private AmmoSection _ammoSection;
    private Gun mainGunComponent;
    private CameraRecoil _cameraRecoil;
    private bool reload = false;
    private RaycastTest _raycastTest;
    private bool dead = false;

    private void Awake()
    {
        _raycastTest = GetComponentInChildren<RaycastTest>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnEnable()
    {
        EventHandler.ChangeWeapon += OnAfterWeaponChange;
        EventHandler.Reloading += OnReloading;
        EventHandler.OpenInventory += OnInventoryOpen;
        EventHandler.CloseInventory += OnInventoryClose;
        EventHandler.PlayerDead += OnPlayerDead;
    }



    private void OnDisable()
    {
        EventHandler.ChangeWeapon -= OnAfterWeaponChange;
        EventHandler.Reloading -= OnReloading;
        EventHandler.OpenInventory -= OnInventoryOpen;
        EventHandler.CloseInventory -= OnInventoryClose;
        EventHandler.PlayerDead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        dead = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnInventoryClose()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnInventoryOpen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnReloading()
    {
        StartCoroutine(StartReload());
    }

    IEnumerator StartReload()
    {
        reload = true;
        yield return new WaitForSeconds(GameManager.Instance.playerStats.currentWeapon.reloadDelay);
        reload = false;
    }

    private void OnAfterWeaponChange()
    {
        mainGun = GameManager.Instance.playerStats.currentWeapon.weaponPrefab;
        mainGunComponent = mainGun.GetComponent<Gun>();
        _cameraRecoil = FindObjectOfType<CameraRecoil>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;

        if (Input.GetKey(fireKey) && Time.time > timeStamp && GameManager.Instance.playerStats.currentWeapon.currentMag > 0 && !reload)
        {
            timeStamp = Time.time + GameManager.Instance.playerStats.currentWeapon.fireRate;
            Shoot();
            _ammoSection.Shoot();
            if (GameManager.Instance.playerStats.currentWeapon.currentMag - 1 < 0)
            {
                timeStamp = Time.time + GameManager.Instance.playerStats.currentWeapon.reloadDelay;
            }
        }

        UpdateMouseLook();
        //WeaponSway();
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
            // Environment particle effect wont play when shooting anything that isn't the environment
            if (hit.transform.tag != "Enemy" && hit.transform.tag != "Limb" && hit.transform.tag != "Head" && hit.transform.tag != "HitBox")
            {
                GameObject impact = Instantiate(mainGunComponent.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impact.GetComponent<ParticleSystem>().Play();
                Destroy(impact, 2f);
            }

            //_raycastTest.Hit(hit);

            GameObject.FindObjectOfType<RaycastTest>().Hit(hit);


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
