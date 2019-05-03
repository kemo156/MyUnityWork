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

    private bool slide;//#7

    private bool facingRight; //#ep4

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

        isGrounded = IsGrounded();//#9

        HandleMovement(horizontal);

        Flip(horizontal);//#4

        HandleAttacks();//#6

        HandleLayers();//#10

        ResetValues(); //#6
    }

    private void HandleMovement(float horizontal)
    {
        //#10 Start
        if(myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        //#10 End

        //#6 START this condition will stop the movement while attacking 17:00 mins ep 6


        //#7 !myAnimator.GetBool("slide")                                                                  //#9  && isGrounded || airControl
        if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl)) // 0 is the base layer
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        //# END

        //#9 START
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");//#10 16:00
        }

        //#9 END

        // #7 START
        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) //!this.myAnimator not playing the slide animation
        {
            myAnimator.SetBool("slide", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }
        // #7 END 



        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));//#5 horizontal must be resulting to 0 or 1

    }

    //#6 Start
    private void HandleAttacks()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) //#6 && !this never start attack before the orginal attack is done
        {
            myAnimator.SetTrigger("attack");

            myRigidbody.velocity = Vector2.zero; //#6 stop sliding while player is attacking
        }
    }

    private void HandleInput() // handle all our input for attacking jumping and so on
    {

        //#9 Start
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        //#9 END
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
        // #7 START
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }

        // #7 END
    }
    //#6 End

    //#4
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
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
        slide = false; // #7
        jump = false;//#9
    }

    //#6 END

    //#9 START
    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");//#10
                        myAnimator.SetBool("land", false);//#10
                        return true;
                    }
                }
            }
        }
        return false;
    }
    //#9 END

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
