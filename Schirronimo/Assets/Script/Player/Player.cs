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
    private InputAction pauseButton;

    //Movements
    private float directionx;
    [SerializeField]
    private float moveSpeed;
    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Vector2 moveVelocity;
    [HideInInspector]
    public float jumpHeight;
    private SpriteRenderer sprRend;
    private Vector2 m_Velocity = Vector2.zero;
    [SerializeField]
    private float MoveSliding;

    //Colliders
    public GameObject leftCollider;
    public GameObject rightCollider;

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

        pauseButton = gameInputActions.Player.Pause;
        pauseButton.performed += UIMenuManager.s_Singleton.OnPause;
        pauseButton.Enable();
    }

    void Update()
    {
        //flip du sprite
        if(directionx == -1)
        {
            sprRend.flipX = true;
        }
        if(directionx == 1)
        {
            sprRend.flipX = false;
        }

        if(rb2D.velocity.y >= 0)
        {
            sprRend.color = new Color(1f, 1f, 1f, 0.8f);
        }
        else
        {
            sprRend.color = new Color(1f, 1f, 1f, 1f);
        }

        //colliders suivent le y du perso
        leftCollider.transform.position = new Vector2(leftCollider.transform.position.x, transform.position.y);
        rightCollider.transform.position = new Vector2(rightCollider.transform.position.x, transform.position.y);
    }

    void FixedUpdate()
    {
        //On déplace le perso gauche ou droite
        directionx = movement.ReadValue<float>();
        moveVelocity = new Vector2(directionx * moveSpeed, rb2D.velocity.y);
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, moveVelocity, ref m_Velocity, MoveSliding);
    }

    void TakeOff(InputAction.CallbackContext context)
    {
        tapNumber += 1;
    }

    #region Enable/DisableInputs
    public void DisableTakeoffInputs()
    {
        takeoff.Disable();
    }

    public void DisableMovementInputs()
    {
        movement.Disable();
    }

    public void EnableTakeoffInputs()
    {
        takeoff.Enable();
    }

    public void EnableMovementInputs()
    {
        movement.Enable();
    }
    #endregion

    void OnDisable()
    {
        movement.Disable();
        takeoff.Disable();
    }
}
