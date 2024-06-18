using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    private Vector2 startPosition;
    private float conquaredistance = 0;
    private Rigidbody2D rb2d;

    public UnityEvent OnHit = new UnityEvent();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initalize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        rb2d.velocity = transform.up * bulletData.speed;
        startPosition = transform.position;
    }

    private void Update()
    {
        conquaredistance = Vector2.Distance(startPosition, transform.position);
        if (conquaredistance >= bulletData.maxDistance)
            DisableObject();
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.name);
        OnHit?.Invoke();
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(bulletData.dmg);
        }
        DisableObject();
    }
}