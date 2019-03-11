using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeatherController : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerController player;
    private bool reached;
    private bool released;
    private bool contact;
    public float distance = 1;
    public float speed;
    public float tetherRange;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        Vector3 targetVelocity;

        if (player.hMove != 0)
            targetVelocity = new Vector2(0.707f * Mathf.Abs(player.hMove) / player.hMove, 0.707f);
        else
            targetVelocity = new Vector2(0.0f, 1.0f);
        body.velocity = targetVelocity * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Teather"))
            released = true;
        if (distance > tetherRange)
            reached = true;

        distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2));
    }

    void FixedUpdate()
    {
        if (released || reached)
        {
            Retract();
        }
        else if (contact)
        {
            // Swing player
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Detected.");
        if (other.gameObject.tag == "Grapple Target")
        {
            body.velocity = Vector2.zero;
            contact = true;
        }
        else if ((reached || released) && other.gameObject.tag == "Player")
        {
            player.tetherOut = false;
            Destroy(gameObject);
        }
    }

    void Retract()
    {
        float xT = (transform.position.x - player.transform.position.x) / Mathf.Abs(transform.position.x - player.transform.position.x);
        float yT = (transform.position.y - player.transform.position.y) / Mathf.Abs(transform.position.y - player.transform.position.y);
        float x = Mathf.Abs(transform.position.x - player.transform.position.x) / distance * xT;
        float y = Mathf.Abs(transform.position.y - player.transform.position.y) / distance * yT;
        body.velocity = new Vector3(-x, -y, 0) * speed;
    }
}
