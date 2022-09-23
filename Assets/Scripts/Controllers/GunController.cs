using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    public Transform _spawnPoint;
    public GameObject _projectile;
    [SerializeField] float _shootingSpeed;
    float _fireCooldown = 0;
    private void Update()
    {
        if (_fireCooldown > 0)
        {
            _fireCooldown -= Time.deltaTime;
        }

        if (_playerInput.fireInput && _fireCooldown <= 0)
        {
            _fireCooldown = 2f;
            var _newProjectile = Instantiate(_projectile, _spawnPoint.position, _spawnPoint.rotation);
            _newProjectile.GetComponent<Rigidbody>().AddForce(_spawnPoint.forward * _shootingSpeed, ForceMode.Impulse);
        }
    }
}
