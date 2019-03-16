using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    //true = right, false = left
    public bool shootVertical;
    public bool shootHorizontal;
    public bool vertical = true;

    private Rigidbody2D body;

    public float time;
    private float timer;

    public GameObject shooter;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        //shoot direction
        if (shootVertical && vertical)
        {
            body.velocity = transform.up * speed;
        }
        else
        {
            //if crouch in air
            if (!shootVertical && vertical)
            {
                body.velocity = transform.up * -1.0f * speed;
            }
            else
            {
                //if facing right
                if (shootHorizontal)
                {
                    body.velocity = transform.right * speed;
                }
                //if facing left
                else
                {
                    body.velocity = transform.right * -1.0f * speed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //instantiate small animationg
        if (collision.gameObject.tag == "Environment") { Destroy(gameObject); }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > time) { Destroy(gameObject); }
    }
}