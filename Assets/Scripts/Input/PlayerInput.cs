using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool fireInput { get; private set; }
    public bool adsInput { get; private set; }

    InputActions inputActions;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        inputActions = new InputActions();

        //enable actionmap
        inputActions.Player.Enable();

        //subscribe to action input events
        inputActions.Player.Move.performed += SetMove;
        inputActions.Player.Move.canceled += SetMove;

        inputActions.Player.Look.performed += SetLook;
        inputActions.Player.Look.canceled += SetLook;

        inputActions.Player.Jump.performed += SetJump;
        inputActions.Player.Jump.canceled += SetJump;

        inputActions.Player.Fire.performed += SetFire;
        inputActions.Player.Fire.canceled += SetFire;

        inputActions.Player.Ads.performed += SetAds;
        inputActions.Player.Ads.canceled += SetAds;
    }
    private void OnDisable()
    {
        //unsubscribe to action input events
        inputActions.Player.Move.performed -= SetMove;
        inputActions.Player.Move.canceled -= SetMove;

        inputActions.Player.Look.performed -= SetLook;
        inputActions.Player.Look.canceled -= SetLook;

        inputActions.Player.Jump.performed -= SetJump;
        inputActions.Player.Jump.canceled -= SetJump;

        inputActions.Player.Fire.performed -= SetFire;
        inputActions.Player.Fire.canceled -= SetFire;

        inputActions.Player.Ads.performed -= SetAds;
        inputActions.Player.Ads.canceled -= SetAds;

        //disable actionmap
        inputActions.Player.Disable();
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

    private void SetAds(InputAction.CallbackContext context)
    {
        //read value of context and store bool
        adsInput = context.ReadValue<float>() == 1;
    }
}

