using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    GameManager _gameManager;
    public Transform _spawnPoint;
    public GameObject _projectile;
    [SerializeField] float _shootingSpeed;
    float _fireCooldown;
    int _fireShots;
    int _maxShots = 3;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _fireShots = _maxShots;
    }

    private void Update()
    {
        if (_fireCooldown > 0)
        {
            _fireCooldown -= Time.deltaTime;
        }
    }
    public void PlayerFire()
    {
            
        if (_fireCooldown <= 0 && _fireShots > 0)
        {
            _fireShots--;
            _fireCooldown = 1f;
            var _newProjectile = Instantiate(_projectile, _spawnPoint.position, _spawnPoint.rotation);
            _newProjectile.GetComponent<Rigidbody>().AddForce(_spawnPoint.forward * _shootingSpeed, ForceMode.Impulse);
        }
        else if (_fireShots <=0)
        {
            _gameManager.ChangeActivePlayer();
            _fireShots = _maxShots;
        }
            
    }
}
