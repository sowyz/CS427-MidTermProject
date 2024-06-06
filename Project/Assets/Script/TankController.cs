using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private Vector2 movementVector;
    public float maxSpeed = 100;
    public float rotationSpeed = 25;
    public float turretRotationSpeed = 50;
    public Animator TrackLeft, TrackRight;
    public Transform turretParent;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        TrackLeft = transform.Find("Track_Left").GetComponent<Animator>();
        TrackRight = transform.Find("Track_Right").GetComponent<Animator>();
    }

    public void HandleShoot()
    {
        Debug.Log("Shooting");
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        UpdateTrackAnimations();
    }

    private void UpdateTrackAnimations()
    {
        // Calculate the speed based on the movement vector's magnitude
        float speed = movementVector.magnitude;
        TrackLeft.SetFloat("Speed", speed);
        TrackRight.SetFloat("Speed", speed);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationStep);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        UpdateTrackAnimations();
    }
}
