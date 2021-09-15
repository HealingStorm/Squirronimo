using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private GameInputActions gameInputActions;
    private InputAction movement;
    private InputAction takeoff;

    private float directionx;
    [SerializeField]
    private float moveSpeed;

    private void Awake() 
    {
        gameInputActions = new GameInputActions();
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
        transform.Translate(new Vector3(directionx,0,0));
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
