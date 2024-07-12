using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBehavior : AIBehavior
{
    public float fieldOfVision = 60f;

    [Range(1, 15)] 
    public float spacing = 9f;

    public override void performAction(TankController tank, Transform target)
    {
        if(TargetInFOV(tank, target))
        {
            tank.HandleShoot();
        }
        if (target != null && Vector2.Distance(tank.transform.position, target.position) > spacing)
        {
            Vector2 direction = (Vector2)target.position - (Vector2)tank.tankMover.transform.position;
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
        }
        else tank.HandleMoveBody(Vector2.zero);
        tank.HandleTurretMovement(target.position);
    }

    private bool TargetInFOV(TankController tank, Transform target)
    {
        float angle = Vector2.Angle(tank.aimTurret.transform.right, target.position - tank.aimTurret.transform.position);
        return angle < fieldOfVision;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spacing);
    }
}   

