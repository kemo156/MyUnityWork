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
        platformCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true); //do this once you are done on step 11
    }

    private void OnTriggerEnter2D(Collider2D other)//11:29
    {
        
    }
}
