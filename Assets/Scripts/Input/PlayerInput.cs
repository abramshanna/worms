using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool AdsInput { get; private set; }

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
        MoveInput = context.ReadValue<Vector2>();
    }
    private void SetLook(InputAction.CallbackContext context)
    {
        //read value of context and store vector2
        LookInput = context.ReadValue<Vector2>();
    }

    private void SetJump(InputAction.CallbackContext context)
    {
        //read value of context and store bool
        JumpInput = context.ReadValue<float>() == 1;
    }

    private void SetFire(InputAction.CallbackContext context)
    {
        //read value of context and store bool
        FireInput = context.ReadValue<float>() == 1;
    }

    private void SetAds(InputAction.CallbackContext context)
    {
        //read value of context and store bool
        AdsInput = context.ReadValue<float>() == 1;
    }
}

