using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    Animator UIAnimation;
    void Start(){
        UIAnimation=this.GetComponent<Animator>();
    }
    public void playHitEffect(){
        UIAnimation.SetTrigger("Hit");
        Invoke("empty",0.3f);
    }
    public void empty(){
        UIAnimation.SetTrigger("Empty");
    }
}
