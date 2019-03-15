using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //public GameObject tether;
    [SerializeField] private GameObject hook;           // The "hook" in the grappling hook
    [SerializeField] private GameObject shot;           // Our player's projectile
    [SerializeField] private GameObject attack;         // A hitbox representing a melee weapon
    [SerializeField] private GameObject cam;            // The primary virtual camera
    [SerializeField] private CharacterController2D controller;      // The script that processes our movement inputs

    [SerializeField] private float speed = 0.0f;   // Character ground speed

    private float timer;
    
    private bool fire;                      // Attack key input
    public float fireRate;                  // How fast you can shoot
    private float nextFire;                 //counter for fire rate
    private GameObject settingshot;

    private KeyCode jumpKey = KeyCode.Z;
    private KeyCode fireKey = KeyCode.X;
    private bool teather;                   // Teather key input
    private bool crouch;                    // Crouch key input
    private bool jump;                      // Jump key input
    private bool facing;                    // True = right, False = left
    private bool grounded;                  // On the ground as opposed to in the air?
    private bool camFollow;                 // Camera is in follow mode?
    [System.NonSerialized] public float hMove = 0.0f;             // Ground movement
    [System.NonSerialized] public bool tetherOut;                 // Grappling hook deployed?
    private GameObject GrappleHook;         // Active Grappling Hook Object
    private Animator animate;
    private Rigidbody2D body;

    void Start()
    {
        facing = true;
        body = GetComponent<Rigidbody2D>();
        SetInitialState();
    }

    void SetInitialState()      // Sets variables 
    {
        camFollow = true;
    }

    // Update is called once per frame
    void Update()
    {
        //timer
        timer += Time.deltaTime;

        hMove = Input.GetAxisRaw("Horizontal");
        // animate.SetBool("Moving", hMove != 0);
        // animate.SetBool("Crouch", Input.GetButtonDown("Crouched");

        #region Keys
        if (Input.GetKeyDown(jumpKey) && grounded)
        {
            // animate.SetTrigger("Jumping");
            jump = true;
        }

        else if (Input.GetKeyDown(jumpKey) && !grounded)
        {
            if (body.velocity.y > 0)
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * .5f);
        }

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;

        if (Input.GetButtonDown("Attack"))
        {
            fire = true;
        }
        
        if(Input.GetButtonUp("Attack"))
        {
            fire = false;
        }

        if (Input.GetButtonDown("Teather"))
        {
            teather = true;
        }
        #endregion
    }

    void FixedUpdate()
    {
        controller.Move(hMove * speed * Time.fixedDeltaTime, crouch, jump);

        //direction facing
        if (hMove > 0) { facing = true; }
        else if (hMove < 0) { facing = false; }

        grounded = controller.m_Grounded;

        if (fire && timer > fireRate)
        {
            Attack();
            timer = 0.0f;
        }

        if (teather)
            CastTether();

        jump = false;
        teather = false;
    }

    void CastTether()           // Currently non functional
    {
        if (!tetherOut)
        {
            tetherOut = true;
            GrappleHook = Instantiate(hook, new Vector3(transform.position.x + .2f, transform.position.y + .2f, transform.position.z), Quaternion.identity);
        }
    }

    void Attack()
    {
        Vector3 attackSpawn = body.position;
        Quaternion placeholderRotation = new Quaternion();

        //change spawn location based on facing direction
        if(facing){ attackSpawn.x++; }
        else if(!facing){ attackSpawn.x--; }

        //shoot the shot
        settingshot = Instantiate(shot, attackSpawn, placeholderRotation);

        //set shot direction
        settingshot.GetComponent<ShotController>().direction = facing;
    }
}
