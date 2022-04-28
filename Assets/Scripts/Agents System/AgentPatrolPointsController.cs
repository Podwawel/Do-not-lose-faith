using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrolPointsController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private PatrolPoints _patrolPoints;
    [SerializeField]
    private float _timeToMoveIfGlitched = 2f;
    [SerializeField]
    private float _baseTimeOfEachPointBreak = 3f;
    [SerializeField]
    private LayerMask _patrolPointsLayerMask;
    [SerializeField]
    private float _patrolPointsCollistionEnabledDistance;

    private float _timeElapsed;
    private bool isWart;

    public void Initialize(PatrolPoints patrolPoints)
    {
        isWart = false;
        _patrolPoints = patrolPoints;
        _agent = GetComponent<NavMeshAgent>();
    }

    public void CustomUpdate()
    {
        if (isWart) return;

        _timeElapsed += Time.deltaTime;

        if(_timeElapsed >= _timeToMoveIfGlitched)
        {
            _timeElapsed = 0f;
            if(_agent.velocity.magnitude < 0.1f) SetNewDestination();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _patrolPointsLayerMask 
            && Vector3.Distance(_agent.destination, transform.position) <= _patrolPointsCollistionEnabledDistance)
        {
            int doBreak = Random.Range(0, 1);

            if(doBreak == 0)
            {
                SetNewDestination();
            }
            else
            {
                StartWart();
            }
        }
    }

    private void StartWart()
    {
        StartCoroutine(WardCoroutine());
    }    

    private IEnumerator WardCoroutine()
    {
        isWart = true;
        float wardTimeElapsed = 0f;
        float wardTime = Random.Range(_baseTimeOfEachPointBreak / 2, _baseTimeOfEachPointBreak * 2);
        if(wardTimeElapsed >= wardTime)
        {
            yield return new WaitForEndOfFrame();
        }

        isWart = false;
        SetNewDestination();
    }

    private void SetNewDestination()
    {
        _agent.destination = _patrolPoints.GetPatrolPointPosition();
    }

    public void Deinitialize()
    {

    }
}
