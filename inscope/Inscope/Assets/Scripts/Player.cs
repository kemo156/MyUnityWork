using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    private Rigidbody2D myRigidbody;

    private Animator myAnimator; //#5

    [SerializeField]
    private float movementSpeed;

    private bool attack;//#6

    private bool slide;//#7

    private bool facingRight;


    //Start Ep 9
    [SerializeField] 
    private Transform[] groundPoints;

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

    
    //End Ep 9

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;

        myRigidbody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>(); // #5
    }

    //#6
    private void Update()
    {
        HandleInput();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();//#ep9
        
        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks(); //#6

        HandleLayers();//#10

        ResetValues();//#6
    }

    private void HandleMovement(float horizontal)
    {
        //#10 Start
        if(myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }

        //#10 End
        if(!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
            //#6 //#7 added !myAnimator.GetBool("slide")   //#9 added && (isGrounded || airControl))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); // #naa ni siya previous episode gibalhin sa episode 6
        }

        /*Episode 9*/
        if(isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");//#10
        }

        /*End Episode 9*/

        /*Episode 7*/

        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) //#7
        {
            myAnimator.SetBool("slide", true);
        }
        else if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }

        /*End Episode 7*/

        

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal)); // #5
    }


    //#6
    private void HandleAttacks()
    {
        if(attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;//reset velocity when attacking

        }
    }

    //#6
    private void HandleInput()
    {
        /*Episode 9*/

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        /*End Episode 7*/

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }

        /*Episode 7*/

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }

        /*End Episode 7*/


    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;

        }
    }

    private void ResetValues()
    {
        attack = false;
        slide = false;//#7
        jump = false; //#9
    }

    private bool IsGrounded()//#9
    {
        if(myRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
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

    //#EP10

    private void HandleLayers()
    {
        if(!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
