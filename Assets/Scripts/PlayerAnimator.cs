using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator animator;

    // Update is called once per frame

    private void Awake()
    {
        //animator.SetBool("AlreadyDead", false);
    }

    void FixedUpdate()
    {
        if (PlayerGPMechanics.playerDead)
        {
            animator.SetTrigger("IsDead");
            animator.SetBool("PlayerDead", true);
            //animator.SetBool("AlreadyDead", true);
        }

        if (!PlayerGPMechanics.playerDead)
        {
            animator.SetBool("PlayerDead", false);
        }

        /*
        public void AnimationControl()
        {
            if (!Input.GetButton("Fire3"))
            {
                animator.SetLayerWeight(1, 0);
            }
            else
            {
                animator.SetLayerWeight(1, 1);
            }
        }
        */
    }
}
