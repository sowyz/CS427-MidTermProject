using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletImpactGeneratorRandomPositionUtil : MonoBehaviour
{
    public GameObject objectPrefab;
    public Bullet bullet;
    public float radius = 0.2f;

    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * radius + (Vector2)transform.position;
    }

    protected Quaternion Random2DRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public void CreateObject()
    {
        Vector2 position = GetRandomPosition();
        GameObject impactObject = GetObject();
        impactObject.transform.position = position;
        impactObject.transform.rotation = bullet.transform.rotation * Quaternion.Euler(0, 0, 180);
    }

    protected GameObject GetObject()
    {
        return Instantiate(objectPrefab);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
