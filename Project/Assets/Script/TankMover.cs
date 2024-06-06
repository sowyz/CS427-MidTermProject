using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 100;
    public float rotationSpeed = 50;
    public Animator TrackLeft, TrackRight;
    private Vector2 movementVector;

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        TrackLeft = transform.Find("Track_Left").GetComponent<Animator>();
        TrackRight = transform.Find("Track_Right").GetComponent<Animator>();
    }

    private void UpdateTrackAnimations()
    {
        //Calculate the speed based on the movement vector's magnitude
        float speed = movementVector.magnitude;
        TrackLeft.SetFloat("Speed", speed);
        TrackRight.SetFloat("Speed", speed);
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        UpdateTrackAnimations();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
        UpdateTrackAnimations();
    }
}
