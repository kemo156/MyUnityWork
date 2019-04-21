using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool slide;//#7

    private void HandleMovement(float horizontal)
    {
       
        //#7 !myAnimator.GetBool("slide") 
        if(!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) // 0 is the base layer
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        //# END

        // #7 START
        if(slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) //!this.myAnimator not playing the slide animation
        {
            myAnimator.SetBool("slide", true);
        }
        else if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }
        // #7 END 
        
    }

    private void HandleInput() // handle all our input for attacking jumping and so on
    {

        // #7 START
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }

        // #7 END
    }

    private void ResetValues()
    {
 
        slide = false; // #7
    }

}
