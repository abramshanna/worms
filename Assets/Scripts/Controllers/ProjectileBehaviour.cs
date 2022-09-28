using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    GameManager _gameManager;
    PlayerController _hitPlayer;
    float _lifeTime = 10;
    public int _damageGiven = 25;
    
    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
        Destroy(gameObject, _lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _hitPlayer = collision.gameObject.GetComponentInParent<PlayerController>();

            //reduce hit player health by damage given
            _hitPlayer._playerHealth -= _damageGiven;

            //disable hit player if no health
            if (_hitPlayer._playerHealth <= 0)
            {
                _hitPlayer.gameObject.SetActive(false);
            }
        } 
    }
}
