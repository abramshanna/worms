using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    float lifeTime = 10;
    float damage = 25;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PlayerController hitPlayer = collision.gameObject.GetComponentInParent<PlayerController>();

            //reduce hit player health by damage given
            hitPlayer.TakeDamage(damage);
        } 
    }
}
