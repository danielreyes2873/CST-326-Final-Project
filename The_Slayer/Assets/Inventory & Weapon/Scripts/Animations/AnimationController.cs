using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator anim;
    private AnimationIK animIK;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        animIK = GetComponent<AnimationIK>();
    }

    // Update is called once per frame
    void Update()
    {
        layerControl();
        walkAnimation();
        AimAndFireAnim();

    }

    private void AimAndFireAnim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isFire", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isFire", false);
        }

       if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("isAim", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isAim", false);
        }
    }

    private void layerControl()
    {
        var currentWeapon = GameManager.Instance.playerStats.currentWeapon;
        if (currentWeapon != null) //if player do not have weapon, play base layer
        {
            if (currentWeapon.gunType == GunType.Rifle)
            {
                anim.SetLayerWeight(1, 0);
            }
            else
            {
                anim.SetLayerWeight(1, 1);
            }
        }
    }
    private void walkAnimation()
    {
        if (playerMovement.inputX != 0 || playerMovement.inputZ != 0)
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("InputX", playerMovement.inputX);
            anim.SetFloat("InputZ", playerMovement.inputZ);
            animIK.enabled = true;
        }
        else
        {
            anim.SetBool("isMoving", false);
            animIK.enabled = false;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("run");
        Debug.Log("runrunur");
    }



}
