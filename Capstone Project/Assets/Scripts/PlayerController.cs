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

    [SerializeField] private float speed;   // Character ground speed

    private bool tetherOut;                 // Grappling hook deployed?
    private bool crouch;                    // Crouch key input
    private bool grounded;                  // On the ground as opposed to in the air?
    private bool camFollow;                 // Camera is in follow mode?
    private bool jump;                      // Jump key input
    private float hMove = 0.0f;             // Ground movement
    private GameObject GrappleHook;         // Active Grappling Hook Object
    private Animator animate;

    void Start()
    {
        SetInitialState();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
    }

    void FixedUpdate()
    {
        controller.Move(hMove * speed * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void SetInitialState()      // Sets variables 
    {
        camFollow = true;
    }

    void CastTether()           // Currently non functional
    {
        if (tetherOut == false)
        {
            tetherOut = true;
            Instantiate(hook, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);   // Needs to be instantiated at the location of the player's correct hand with the correct Quaternion value
            GrappleHook = transform.GetChild(0).gameObject;
        }
    }
}
