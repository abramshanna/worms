using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GameManager _gameManager;
    [SerializeField] GunController _gunController;
    Rigidbody _rigidbody;
    CapsuleCollider _capsuleCollider;
    Vector3 _playerVelocity;
    public bool grounded;
    [SerializeField] private LayerMask layerMask;

    public float _playerHealth;
    public float _maxHealth;
    public float _jumpHeight;
    public float _movementSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _playerHealth = _maxHealth;
    }
    private void FixedUpdate()
    {
        if (_gameManager._activePlayer == this.gameObject)
        {

            //groundcheck spherecast returns bool
            grounded = Physics.SphereCast(this.transform.position, _capsuleCollider.radius, Vector3.down, out _, _capsuleCollider.height / 2, layerMask);

            //jump
            _playerVelocity.y = _playerInput.jumpInput && grounded ? Mathf.Sqrt(_jumpHeight * -3.0f * Physics.gravity.y) : 0;

            //get camera direction
            Vector3 forward = CameraController._pivotPoint.transform.forward;
            Vector3 right = CameraController._pivotPoint.transform.right;

            //ignore rotation of camera
            forward.y = 0;
            right.y = 0;

            //normalize vector
            forward.Normalize();
            right.Normalize();

            //set direction
            Vector3 direction = forward * _playerInput.moveInput.y + right * _playerInput.moveInput.x;

            //move
            _playerVelocity = new Vector3(direction.x * _movementSpeed, _playerVelocity.y, direction.z * _movementSpeed) * _rigidbody.mass;

            //rotate player
            if (direction != Vector3.zero)
            {
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction), 5f);
            }

            //add force
            _rigidbody.AddForce(_playerVelocity);


            if (_playerInput.fireInput)
            {
                _gunController.PlayerFire();
            }
        }

    }

}
