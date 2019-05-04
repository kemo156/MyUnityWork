using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{

    private BoxCollider2D playerCollider;

    [SerializeField]
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D platformTrigger; //Do this after step 11

    
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true); //do this once you are done on step 11
    }

    private void OnTriggerEnter2D(Collider2D other)//11:29
    {
        if(other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }

    void OnTriggerExit2D(Collider2D other) //makalayat siya kung ang player naas tunga2 sa collider sa babaw
    {
        if(other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }
}
