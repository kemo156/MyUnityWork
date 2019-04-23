using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 

    //#9
    [SerializeField]
    private Transform[] groundPoints;//#ep9

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    
    //#9
   
 



    void FixedUpdate() // runs fix amount of time compared on Update()
    {


        isGrounded = IsGrounded();//#9

       
    }

    private void HandleMovement(float horizontal)
    {
        //#6 START this condition will stop the movement while attacking 17:00 mins ep 6

        //#7 !myAnimator.GetBool("slide")                                                                  //#9  && isGrounded || airControl
        if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl)) // 0 is the base layer
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        //# END

        //#9 START
        if(isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        //#9 END

        
    }


    private void HandleInput() // handle all our input for attacking jumping and so on
    {

        //#9 Start
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        //#9 END
      
    }
 



    //#6 START ResetValues() will stop the attack animation if its in loop time will only attack once while pressing
    private void ResetValues()
    {
      
        jump = false;//#9
    }


    //#9 START
    private bool IsGrounded()
    {
        if(myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    //#9 END
}
