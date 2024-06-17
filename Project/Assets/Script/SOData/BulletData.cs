using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData", order = 1)]
public class BulletData : ScriptableObject
{
    public float speed = 20;
    public int dmg = 5;
    public float maxDistance = 30;
}
