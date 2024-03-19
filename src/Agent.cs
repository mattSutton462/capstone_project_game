using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentMover agentMover; 

    public Animator animator;

    private WeaponParent weaponParent;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }

    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public void PerformAttack ()
    {
        weaponParent.Attack();
    }

    public void PerformDash()
    {
        agentMover.Dash();
    }

    private void Start()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    // Update is called once per frame
    void Update()
    {

        weaponParent.PointerPosition = PointerInput;
        agentMover.MovementInput = MovementInput;

        AnimateCharacter();
    }

    

    private void AnimateCharacter()
    {
        animator.SetFloat("Horizontal", MovementInput.x);
        animator.SetFloat("Vertical", MovementInput.y);
        animator.SetFloat("Speed", MovementInput.magnitude);
    }





}
