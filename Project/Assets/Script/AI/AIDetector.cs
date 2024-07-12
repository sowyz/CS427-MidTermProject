using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius = 11f;

    [SerializeField]
    private float detectionDelay = 0.1f;

    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private LayerMask visibilityLayer;

    [SerializeField]
    private float delayTime = 0.1f;

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
        }
        
    }

    private void detectTarget()
    {

        if (target == null)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
            if (collider != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, viewRadius, visibilityLayer);
                if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    float time = delayTime;
                    while (time > 0)
                    {
                        time -= Time.deltaTime;
                    }
                    target = hit.collider.transform;
                }
            }
        }
        else if (target.gameObject.activeSelf == false || Vector2.Distance(transform.position, target.position) > viewRadius + 1f)
        {
            target = null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, viewRadius, visibilityLayer);
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                target = hit.collider.transform;
            }
            else
            {
                target = null;
            }
        }
    }

    private void Start()
    {
        StartCoroutine(detectionCoroutine());
    }

    IEnumerator detectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        detectTarget();
        StartCoroutine(detectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
