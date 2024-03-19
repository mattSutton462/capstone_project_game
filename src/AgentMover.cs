using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

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
        if (!isDashing)
        {
            Move();
        }
        
    }

    private void Move()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = oldMovementInput * currentSpeed;
    }

    public void Dash()
    {
        if (!canDash) 
            return; 
        isDashing = true;
        canDash = false;

        Vector2 dashDirection = oldMovementInput.normalized;
        if(dashDirection == Vector2.zero)
        {
            dashDirection = rb.velocity.normalized;
        }

        Vector2 dashVelocity = dashDirection * dashingPower;
        rb.velocity = dashVelocity;

        StartCoroutine(Dashing());
        StartCoroutine(DelayDashing());
       
    }
    
    private IEnumerator Dashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }

    private IEnumerator DelayDashing()
    {
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
         
        

}