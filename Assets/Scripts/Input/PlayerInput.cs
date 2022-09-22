using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //vectors can be accessed but only set privately
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool fireInput { get; private set; }

    InputActions _inputActions;
    
    private void OnEnable()
    {
        _inputActions = new InputActions();

        //enable actionmap
        _inputActions.Player.Enable();

        //subscribe to action input events
        _inputActions.Player.Move.performed += SetMove;
        _inputActions.Player.Move.canceled += SetMove;

        _inputActions.Player.Look.performed += SetLook;
        _inputActions.Player.Look.canceled += SetLook;

        _inputActions.Player.Jump.performed += SetJump;
        _inputActions.Player.Jump.canceled += SetJump;

        _inputActions.Player.Fire.performed += SetFire;
        _inputActions.Player.Fire.canceled += SetFire;
    }
    private void OnDisable()
    {
        //unsubscribe to action input events
        _inputActions.Player.Move.performed -= SetMove;
        _inputActions.Player.Move.canceled -= SetMove;

        _inputActions.Player.Look.performed -= SetLook;
        _inputActions.Player.Look.canceled -= SetLook;

        _inputActions.Player.Jump.performed -= SetJump;
        _inputActions.Player.Jump.canceled -= SetJump;

        _inputActions.Player.Fire.performed -= SetFire;
        _inputActions.Player.Fire.canceled -= SetFire;

        //disable actionmap
        _inputActions.Player.Disable();
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        //read value of context and store vector2
        moveInput = context.ReadValue<Vector2>();
    }
    private void SetLook(InputAction.CallbackContext context)
    {
        //read value of context and store vector2
        lookInput = context.ReadValue<Vector2>();
    }

    private void SetJump(InputAction.CallbackContext context)
    {
        //read value of context and store bool
        jumpInput = context.ReadValue<float>() == 1;
    }

    private void SetFire(InputAction.CallbackContext context)
    {
        //read value of context and store bool
        fireInput = context.ReadValue<float>() == 1;
    }
}

