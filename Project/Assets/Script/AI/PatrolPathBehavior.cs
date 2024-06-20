using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPathBehavior : AIBehavior
{
    public PatrolPath patrolPath;
    [Range(0.1f, 1)]
    public float arriveDistance = 0.1f;
    public float waitTime = 0.5f;
    [SerializeField]
    private bool isWaiting = false;
    [SerializeField]
    Vector2 currentPatrolPoint = Vector2.zero;
    bool isInitialized = false;
    private int currentIndex = -1;


    private void Awake()
    {
        if (patrolPath == null)
        {
            patrolPath = GetComponentInChildren<PatrolPath>();
        }
    }

    public override void performAction(TankController tank, Transform target)
    {
        if(!isWaiting)
        {
            if (!isInitialized)
            {
                PatrolPath.PatrolPoint targetPoint = patrolPath.getClosestPatrolPoint(tank.transform.position);
                currentPatrolPoint = targetPoint.position;
                currentIndex = targetPoint.index;
                isInitialized = true;
            }
            if (Vector2.Distance(tank.transform.position, currentPatrolPoint) < arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(Wait());
                return;
            }
            Vector2 direction = currentPatrolPoint - (Vector2)tank.tankMover.transform.position;
            var dotProduct = Vector2.Dot(direction.normalized, tank.tankMover.transform.up);
            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(tank.tankMover.transform.up, direction.normalized);
                int rotationDirection = crossProduct.z > 0 ? -1 : 1;
                tank.HandleMoveBody(new Vector2(rotationDirection, 1));
            }
            else
            {
                tank.HandleMoveBody(Vector2.up);
            }
            tank.HandleTurretMovement(currentPatrolPoint);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        currentIndex++;
        if (currentIndex >= patrolPath.length)
        {
            currentIndex = 0;
        }
        currentPatrolPoint = patrolPath.patrolPoints[currentIndex].position;
        isWaiting = false;
    }
}
