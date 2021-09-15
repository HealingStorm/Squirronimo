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
    private Rigidbody2D rb2D;
    private Vector2 velocity;

    //Takeoff
    [SerializeField]
    private int tapNumber;
    [SerializeField]
    private float takeOffTimer;

    private void Awake() 
    {
        gameInputActions = new GameInputActions();
        rb2D = transform.GetComponent<Rigidbody2D>();
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
        velocity = new Vector2(directionx * moveSpeed, 0);
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
    void TakeOff(InputAction.CallbackContext context)
    {
        Debug.Log("je m'envole");
    }

    void OnDisable()
    {
        movement.Disable();
        takeoff.Disable();
    }
}
