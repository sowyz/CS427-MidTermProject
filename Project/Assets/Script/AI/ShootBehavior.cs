using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBehavior : AIBehavior
{
    public float fieldOfVision = 60f;

    public override void performAction(TankController tank, Transform target)
    {
        if(TargetInFOV(tank, target))
        {
            tank.HandleMoveBody(Vector2.zero);
            tank.HandleShoot();
        }
        else
        {
            tank.HandleMoveBody((target.position - tank.transform.position).normalized);
        }
        tank.HandleTurretMovement(target.position);
    }

    private bool TargetInFOV(TankController tank, Transform target)
    {
        float angle = Vector2.Angle(tank.aimTurret.transform.right, target.position - tank.aimTurret.transform.position);
        return angle < fieldOfVision;
    }
}   
