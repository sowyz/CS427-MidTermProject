using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolStaticBehavior : AIBehavior
{
    public float patrolDelay = 2f;
    
    [SerializeField]
    private Vector2 randomDirection = Vector2.zero;
    [SerializeField]
    private float curPatrolDelay = 0f;

    private void Awake()
    {
        randomDirection = Random.insideUnitCircle;
    }
    public override void performAction(TankController tank, Transform target)
    {
        float angle = Vector2.Angle(tank.aimTurret.transform.right, randomDirection);
        if (curPatrolDelay <= 0 && angle < 2)
        {
            randomDirection = Random.insideUnitCircle;
            curPatrolDelay = patrolDelay;
        }
        else
        {
            if(curPatrolDelay > 0)
            {
                curPatrolDelay -= Time.deltaTime;
            }
            else
            {
                tank.HandleTurretMovement((Vector2)tank.aimTurret.transform.position + randomDirection);
            }
        }
    }
}
