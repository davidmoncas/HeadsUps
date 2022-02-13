using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimations : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }


    private void OnEnable()
    {
        EventManager.StartListening("Start Playing", () => anim.SetTrigger("play"));
        EventManager.StartListening("Ball Hit", () => anim.SetTrigger("hit"));
    }



}
