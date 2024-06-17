using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;
    public TurretData turretData;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private ObjectPool bulletPool;
    [SerializeField]
    private int bulletPoolSize = 10;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        bulletPool.Initalize(turretData.bulletPrefab, bulletPoolSize);
    }

    private void Update()
    {
        if (!canShoot)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= turretData.reloadDelay)
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
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.rotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initalize(turretData.bulletData);
                foreach (Collider2D collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }
}
