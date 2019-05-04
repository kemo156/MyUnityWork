using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    private Transform target; // 5 mins mark
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;   
    }

    // Update is called once per frame
    void LateUpdate() // 5 mins mark LateUpdate to be used to make sure the player has moved before we move the camera towards the player
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }
}
