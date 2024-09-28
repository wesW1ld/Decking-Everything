using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Singleton for easy ref in other scripts.
    public static InputManager instance;

    // Player input ref.
    public static PlayerInput playerInput;

    // Input action values that will be called in other scripts to utilize the inputs.
    public Vector2 moveInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool attackInput { get; private set; }

    // Input actions in the controls action map.
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private void Awake()
    {
        // Set singleton ref.
        if (instance == null)
        {
            instance = this;
        }

        // Player input ref.
        playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInputActions();
    }

    // Setup the input actions by assigning the corresponding actions in the controls action map.
    private void SetupInputActions()
    {
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
    }

    // Constantly update the values of the input actions.
    private void UpdateInputActions()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        // Triggered is the same thing as WasPressedThisFrame().
        jumpInput = jumpAction.triggered;
        attackInput = attackAction.triggered;
    }


}
