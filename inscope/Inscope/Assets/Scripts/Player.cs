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


    private bool facingRight;

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
        
        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks(); //#6

        ResetValues();//#6
    }

    private void HandleMovement(float horizontal)
    {

        if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))//#6
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); // #naa ni siya previous episode gibalhin sa episode 6
        }       

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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
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
    }
}
