using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private bool facingRight; //#ep4
   
    void Start()
    {
        facingRight = true;//#4
    }

    
    void FixedUpdate() // runs fix amount of time compared on Update()
    {
        Flip(horizontal);//#4
    }

    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);      
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
