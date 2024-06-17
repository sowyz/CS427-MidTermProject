using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTankMovementData", menuName = "Data/TankMovementData", order = 1)]
public class TankMovementData : ScriptableObject
{
    public float maxSpeed = 100;
    public float acceleration = 55;
    public float deacceleration = 60;
    public float rotationSpeed = 60;
}
