using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator TrackLeft, TrackRight;
    private Vector2 movementVector;

    public TankMovementData movementData;
    public float currentSpeed = 0;
    public float currentForwardDirection = 1;

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        TrackLeft = transform.Find("Track_Left").GetComponent<Animator>();
        TrackRight = transform.Find("Track_Right").GetComponent<Animator>();
    }

    private void UpdateTrackAnimations()
    {
        float speed = Mathf.Max(movementVector.magnitude, Mathf.Abs(currentSpeed));
        TrackLeft.SetFloat("Speed", speed);
        TrackRight.SetFloat("Speed", speed);
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
        if (movementVector.y > 0)
            currentForwardDirection = 1;
        if (movementVector.y < 0)
            currentForwardDirection = -1;
        UpdateTrackAnimations();
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if(Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += currentForwardDirection * movementData.acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -movementData.maxSpeed, movementData.maxSpeed);
        }
        else
        {
            float deaccelerationDir = 0;

            if (currentSpeed > 0)
                deaccelerationDir = -1;
            else if (currentSpeed < 0)
                deaccelerationDir = 1;

            currentSpeed += deaccelerationDir * movementData.deacceleration * Time.deltaTime; 

            if (deaccelerationDir < 0)
                currentSpeed = Mathf.Clamp(currentSpeed, 0 , movementData.maxSpeed);
            else 
                currentSpeed = Mathf.Clamp(currentSpeed, -movementData.maxSpeed, 0);
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
        UpdateTrackAnimations();
    }
}
