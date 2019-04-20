using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    private bool attack;


    private void Update()
    {
        HandleInput();
    }



    void FixedUpdate() // runs fix amount of time compared on Update()
    {

        HandleAttacks();

        ResetValues(); 
    }

    private void HandleMovement(float horizontal)
    {
 
        if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) // 0 is the base layer
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        
    }


    private void HandleAttacks()
    {
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) //#6 && !this never start attack before the orginal attack is done
        {
           
            myRigidbody.velocity = Vector2.zero; //#6 stop sliding while player is attacking
        }
    }

    private void HandleInput() // handle all our input for attacking jumping and so on
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
    }
  

    //ResetValues() will stop the attack animation if its in loop time will only attack once while pressing
    private void ResetValues()
    {
        attack = false;
    }

}
