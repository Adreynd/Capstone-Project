using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D body;

    public float jumpspeed;
    public float jumpheight;
    public float jumpCD;

    private float timer;
    private float jumpleft;
    private float jumpright;
    //for stopping
    private float height;

    //true = right;
    private bool facing;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        jumpright = jumpspeed;
        jumpleft = jumpspeed * -1.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the player gets close enough and the enemy can jump
        if(player != null && (Vector2.Distance(body.transform.position, player.transform.position) < 12 && timer > jumpCD))
        {
            //reset timer
            timer = 0;
            //If the enemy is to the right of the player face left(default) and inverse jump speed
            if (body.transform.position.x > player.transform.position.x)
            {
                jumpspeed = jumpleft;
                //turn if you jump the other direction
                if (facing) { body.transform.Rotate(0, 180, 0, 0); facing = false; }
            }
            else
            {
                jumpspeed = jumpright;
                //turn if you jump the other direction
                if (!facing) { body.transform.Rotate(0, 180, 0, 0); facing = true; }
            }
            body.AddForce(new Vector2(jumpspeed, jumpheight));
        }

        //if it has been long enough since a jump and the enemy isn't moving much vertically (sliding) stop
        if(height < body.transform.position.y + 0.001 && timer > 0.8f)
        {
            body.velocity = new Vector2(0, 0);
        }
        height = body.transform.position.y;
    }
}
