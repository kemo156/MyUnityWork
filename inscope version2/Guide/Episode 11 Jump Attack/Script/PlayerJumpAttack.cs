using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   

    private bool jumpAttack;//#11




    //#6 Start
    private void HandleAttacks()
    {
        //#11 && isGrounded
        if (attack && isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) //#6 && !this never start attack before the orginal attack is done
        {
            myAnimator.SetTrigger("attack");

            myRigidbody.velocity = Vector2.zero; //#6 stop sliding while player is attacking
        }

        //#11 Start
        if (jumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", true);
        }
        if (!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", false);
        }

        //# End
    }



D

    //#10
    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1); // explain at 14:20
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    //#10 END
}
