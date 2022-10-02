using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GunController gunController;
    [SerializeField] private LayerMask layerMask;
    new Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;
    Vector3 playerVelocity;
    public bool grounded;

    public float Health { get; private set; }
    public bool finishedTurn { get
        {
            return gunController.fireShots <= 0;
        }
    }

    [SerializeField] float MaxHealth;
    [SerializeField] float JumpHeight;
    [SerializeField] float MovementSpeed;

    private void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        Health = MaxHealth;
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.activePlayer != this)
        {
            return;
        }
        if (GameManager.instance.gameState != GameManager.GameState.Turn && GameManager.instance.gameState != GameManager.GameState.Postturn)
        {
            return;
        }
        //groundcheck spherecast returns bool
        grounded = Physics.SphereCast(this.transform.position, capsuleCollider.radius, Vector3.down, out _, capsuleCollider.height / 2, layerMask);

        //jump
        playerVelocity.y = PlayerInput.instance.jumpInput && grounded ? Mathf.Sqrt(JumpHeight * -3.0f * Physics.gravity.y) : 0;

        //get camera direction
        Vector3 forward = CameraController.instance.pivotPoint.transform.forward;
        Vector3 right = CameraController.instance.pivotPoint.transform.right;

        //ignore rotation of camera
        forward.y = 0;
        right.y = 0;

        //normalize vector
        forward.Normalize();
        right.Normalize();

        //set direction
        Vector3 direction = forward * PlayerInput.instance.moveInput.y + right * PlayerInput.instance.moveInput.x;

        if (PlayerInput.instance.adsInput)
        {
            //rotate player
            this.transform.rotation = Quaternion.LookRotation(forward);
        }
        else
        {
            //rotate player
            if (direction != Vector3.zero)
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction), 5f);
            }
        }

        //move
        playerVelocity = new Vector3(direction.x * MovementSpeed, playerVelocity.y, direction.z * MovementSpeed) * rigidbody.mass;

        //add force
        rigidbody.AddForce(playerVelocity);

        if (PlayerInput.instance.fireInput)
        {
            gunController.PlayerFire();
        }
    }
    public void StartTurn()
    {
        gunController.Reload();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
