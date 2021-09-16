using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    //Input Actions
    private GameInputActions gameInputActions;
    private InputAction movement;
    private InputAction takeoff;

    //Movements
    private float directionx;
    [SerializeField]
    private float moveSpeed;
    [HideInInspector]
    public Rigidbody2D rb2D;
    //private Vector2 velocity;
    [HideInInspector]
    public Vector2 moveVelocity;
    [HideInInspector]
    public float jumpHeight;
    private SpriteRenderer sprRend;

    private Vector2 m_Velocity = Vector2.zero;

    [SerializeField]
    private float MoveSliding;

    //Takeoff
    [HideInInspector]
    public int tapNumber;

    private void Awake() 
    {
        gameInputActions = new GameInputActions();
        rb2D = transform.GetComponent<Rigidbody2D>();
        sprRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        tapNumber = 0;
        jumpHeight = 0;
    }
    void OnEnable()
    {
        movement = gameInputActions.Player.Movement;
        movement.Enable();

        takeoff = gameInputActions.Player.Takeoff;
        takeoff.performed += TakeOff;
        takeoff.Enable();
    }

    void FixedUpdate()
    {
        //On déplace le perso gauche ou droite
        directionx = movement.ReadValue<float>();
        //velocity = new Vector2(directionx * moveSpeed, 0);
        //rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
        moveVelocity = new Vector2(directionx * moveSpeed, rb2D.velocity.y);
        //rb2D.velocity = moveVelocity;
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, moveVelocity, ref m_Velocity, MoveSliding);


        //flip du sprite
        if(directionx == -1)
        {
            sprRend.flipX = true;
        }
        if(directionx == 1)
        {
            sprRend.flipX = false;
        }
    }

    void TakeOff(InputAction.CallbackContext context)
    {
        tapNumber += 1;
    }

    #region Enable/DisableInputs
    public void DisableTakeoffInputs()
    {
        takeoff.Disable();
        Debug.Log("takeoff inputs disabled");
    }

    public void DisableMovementInputs()
    {
        movement.Disable();
        Debug.Log("movement inputs disabled");
    }

    public void EnableTakeoffInputs()
    {
        takeoff.Enable();
        Debug.Log("takeoff inputs enabled");
    }

    public void EnableMovementInputs()
    {
        movement.Enable();
        Debug.Log("movement inputs enabled");
    }
    #endregion

    void OnDisable()
    {
        movement.Disable();
        takeoff.Disable();
    }
}
