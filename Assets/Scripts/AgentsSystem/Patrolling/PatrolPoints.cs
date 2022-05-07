using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _patrolPoints = new List<Transform>();

    private int DrawRandomPatrolPoint()
    {
        int drawedPatrolPoint = RandomGenerator.ReturnRandomInt(0, _patrolPoints.Count);
        return drawedPatrolPoint;
    }

    public Vector3 GetPatrolPointPosition()
    {
        Vector3 drawedPositon = _patrolPoints[DrawRandomPatrolPoint()].position;
        return drawedPositon;
    }
}

