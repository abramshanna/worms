using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OnPickup())
            {
                Destroy(gameObject);
            }
        }
    }
    public abstract bool OnPickup();
}