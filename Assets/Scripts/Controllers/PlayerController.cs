using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    CapsuleCollider _capsuleCollider;
    [SerializeField] PlayerInput _playerInput;
    Vector3 _playerVelocity;
    public bool grounded;
    [SerializeField] private LayerMask layerMask;

    public float jumpHeight;
    public float movementSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void FixedUpdate()
    {
        //groundcheck spherecast returns bool
        grounded = Physics.SphereCast(this.transform.position, _capsuleCollider.radius, Vector3.down, out _, _capsuleCollider.height/2, layerMask);

        //jump
         _playerVelocity.y = _playerInput.jumpInput && grounded ? Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y) : 0;


        Vector3 forward = CameraController._pivotPoint.transform.forward;
        Vector3 right = CameraController._pivotPoint.transform.right;

        //ignore rotation of camera
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * _playerInput.moveInput.y + right * _playerInput.moveInput.x;

        //move
        _playerVelocity = new Vector3(direction.x * movementSpeed, _playerVelocity.y, direction.z * movementSpeed) * _rigidbody.mass;

        //rotate player
        if (direction != Vector3.zero)
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction), 5f);
        }
        
        //add force
        _rigidbody.AddForce(_playerVelocity);

    }


}
