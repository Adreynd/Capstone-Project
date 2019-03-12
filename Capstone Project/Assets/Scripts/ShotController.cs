using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    //true = right, false = left
    public bool direction;
    private Rigidbody2D body;

    public GameObject shooter;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        //shoot right or left
        if (direction)
        {
            body.velocity = transform.right * speed;
        }
        else
        {
            body.velocity = transform.right * -1.0f * speed;
        }
    }
}