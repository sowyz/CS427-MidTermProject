using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Update()
    {
        if (!canShoot)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= reloadDelay)
            {
                canShoot = true;
            }
        }
    }   

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = 0;

            foreach (Transform barrel in turretBarrels)
            {
                GameObject bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
                bullet.GetComponent<Bullet>().Initalize();
                foreach (Collider2D collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }
}
