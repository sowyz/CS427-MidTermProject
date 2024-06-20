using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIBehavior shootBehavior = null, patrolBehavior = null;

    [SerializeField]
    private TankController tank;

    [SerializeField]
    private AIDetector detector;

    private void Awake()
    {
        tank = GetComponentInChildren<TankController>();
        detector = GetComponentInChildren<AIDetector>();
    }

    private void Update()
    {
        if (detector.Target != null)
        {
            shootBehavior.performAction(tank, detector.Target);
        }
        else
        {
            patrolBehavior.performAction(tank, detector.Target);
        }
    }
}
