using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public int dmg = 5;
    public float maxDistance = 30;

    private Vector2 startPosition;
    private float conquaredistance = 0;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initalize()
    {
        rb2d.velocity = transform.up * speed;
        startPosition = transform.position;
    }

    private void Update()
    {
        conquaredistance = Vector2.Distance(startPosition, transform.position);
        if (conquaredistance >= maxDistance)
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
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(dmg);
        }
        DisableObject();
    }
}