using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    private AgentMover agentMover = new AgentMover(); 

    public Animator animator;

    private WeaponParent weaponParent;

    Vector2 pointerInput, movementInput;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    private void PerformAttack (InputAction.CallbackContext obj)
    {
        if(weaponParent == null)
        {
            Debug.LogError("Weapon parent is null", gameObject);
            return;
        }
     // weaponParent.PerformAnAttack();
    }

    private void Start()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    // Update is called once per frame
    void Update()
    {
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;

        movementInput = movement.action.ReadValue<Vector2>().normalized;

        agentMover.MovementInput = movementInput;

        AnimateCharacter();
    }

    // gets mouse movement
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void AnimateCharacter()
    {
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.magnitude);
    }



}
