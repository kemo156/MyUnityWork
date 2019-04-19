using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Animator myAnimator; //#ep5

   
    void Start()
    {
       
        myAnimator = GetComponent<Animator>();//#5 Referencing on the Animator
    }

    
    private void HandleMovement(float horizontal)
    {


        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));//#5 horizontal must be resulting to 0 or 1
        
    }
   
}
