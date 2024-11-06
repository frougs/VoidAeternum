using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public PlayerInput _pInput;
    [HideInInspector] public InputAction look;
    [HideInInspector] public InputAction move;

    public float thrust = 10f; 
    public float maxSpeed = 20f; 

    private Rigidbody2D rb;

    private void Start()
    {
        _pInput = GetComponent<PlayerInput>();
        look = _pInput.actions["Look"];
        move = _pInput.actions["Move"];
        
        rb = GetComponent<Rigidbody2D>();
        //rb.useGravity = false; 
        //rb.drag = 1f; 
    }

    private void FixedUpdate()
    {
        Vector2 inputDirection = move.ReadValue<Vector2>();

        if (inputDirection != Vector2.zero)
        {
            Vector2 mDirection = new Vector3(inputDirection.x, inputDirection.y).normalized;

            rb.AddForce(mDirection * thrust);

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
