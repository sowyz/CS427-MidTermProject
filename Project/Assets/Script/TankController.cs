using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public TankMover tankMover;
    public AimTurret aimTurret;
    public Turret turret;

    private void Awake()
    {
        if (tankMover == null)
            tankMover = GetComponentInChildren<TankMover>();
        if (aimTurret == null)
            aimTurret = GetComponentInChildren<AimTurret>();
        if (turret == null)
            turret = GetComponentInChildren<Turret>();
    }

    public void HandleShoot()
    {
        turret.Shoot();
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        tankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        aimTurret.Aim(pointerPosition);
    }
}
