using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float movementSpeed;

    private Animator myAnimator; //#ep5

    private bool attack;//#6

    private bool facingRight; //#ep4

   
    void Start()
    {
        facingRight = true;//#4
        myRigidbody = GetComponent<Rigidbody2D>(); // Referencing on the Rigidbody2D
        myAnimator = GetComponent<Animator>();//#5 Referencing on the Animator
    }

    //#6 Start
    private void Update()
    {
        HandleInput();
    }
    //#6 End


    void FixedUpdate() // runs fix amount of time compared on Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);//#4

        HandleAttacks();//#6

        ResetValues(); //#6
    }

    private void HandleMovement(float horizontal)
    {
        //#6 START this condition will stop the movement while attacking 17:00 mins ep 6
        if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) // 0 is the base layer
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        //# END

        

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));//#5 horizontal must be resulting to 0 or 1
        
    }

    //#6 Start
    private void HandleAttacks()
    {
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) //#6 && !this never start attack before the orginal attack is done
        {
            myAnimator.SetTrigger("attack");

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
    //#6 End

    //#4
    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            //*this code will flip
            Vector3 theScale = transform.localScale; //localScale is the reference of transform for the x,y,z

            theScale.x *= -1;

            transform.localScale = theScale; // the value of the scale x = -1 or x = 1
        }
    }

    //#6 START ResetValues() will stop the attack animation if its in loop time will only attack once while pressing
    private void ResetValues()
    {
        attack = false;
    }

    //#6 END
}
