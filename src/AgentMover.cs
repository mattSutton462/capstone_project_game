using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed = 2.0f, acceleration = 50.0f, deacceleration = 100.0f;

    [SerializeField]
    private float currentSpeed = 0f;

    private Vector2 oldMovementInput;

    public Vector2 MovementInput { get; set; }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        } else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); 
        rb.velocity = oldMovementInput * currentSpeed;
    }
}
