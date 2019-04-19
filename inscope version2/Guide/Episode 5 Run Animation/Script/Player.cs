using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float movementSpeed;

    private Animator myAnimator; //#ep5

    private bool facingRight; //#ep4

   
    void Start()
    {
        facingRight = true;//#4
        myRigidbody = GetComponent<Rigidbody2D>(); // Referencing on the Rigidbody2D
        myAnimator = GetComponent<Animator>();//#5 Referencing on the Animator
    }

    
    void FixedUpdate() // runs fix amount of time compared on Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);//#4
    }

    private void HandleMovement(float horizontal)
    {

        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));//#5 horizontal must be resulting to 0 or 1
        
    }
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
}
