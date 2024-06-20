using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();
    
    public int length {get => patrolPoints.Count;}

    [Header("Gizmos params")]
    public Color pointColor = Color.blue;
    public float pointRadius = 0.5f;
    public Color lineColor = Color.green;

    public struct PatrolPoint
    {
        public int index;
        public Vector2 position;
    }

    public PatrolPoint getClosestPatrolPoint(Vector2 position)
    {
        PatrolPoint closestPoint = new();
        float minDistance = float.MaxValue;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            float distance = Vector2.Distance(patrolPoints[i].position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint.index = i;
                closestPoint.position = patrolPoints[i].position;
            }
        }
        return closestPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = pointColor;
        foreach (Transform point in patrolPoints)
        {
            Gizmos.DrawSphere(point.position, pointRadius);
        }
        Gizmos.color = lineColor;
        for (int i = 0; i < patrolPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
        }
        Gizmos.DrawLine(patrolPoints[patrolPoints.Count - 1].position, patrolPoints[0].position);
    }
}
