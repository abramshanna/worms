using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shootingSpeed;
    float fireTime;
    public float fireShots;
    float maxShots = 3;
    private void Start()
    {
        Reload();
    }
    public void PlayerFire()
    {
        if (Time.time - 1 < fireTime) return;

        if (fireShots <= 0) return;

        fireShots--;
        fireTime = Time.time;
        var newProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootingSpeed, ForceMode.Impulse);
    }
    public void Reload()
    {
        fireShots = maxShots;
    }
}
