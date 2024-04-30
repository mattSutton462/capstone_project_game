using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack, OnDash, OnPause;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, dash, pause;


    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    // gets mouse movement
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        dash.action.performed += PerformDash;
        attack.action.performed += PerformAttack;
        pause.action.performed += PauseGame;

    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        dash.action.performed -= PerformDash;
        pause.action.performed -= PauseGame;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }

    private void PerformDash(InputAction.CallbackContext obj)
    {
        OnDash?.Invoke();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        Debug.Log("Pause");
        OnPause?.Invoke();
    }
    
}
