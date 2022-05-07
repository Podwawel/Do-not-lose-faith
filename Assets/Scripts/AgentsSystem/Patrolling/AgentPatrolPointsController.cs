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
    [SerializeField, Range(0,1)]
    private float _probabilityToGuard = 0.5f;
    [SerializeField]
    private LayerMask _patrolPointsLayerMask;
    [SerializeField]
    private float _patrolPointsCollistionEnabledDistance;

    private float _timeElapsed;
    private bool isGuard;

    public void Initialize(PatrolPoints patrolPoints)
    {
        isGuard = false;
        _patrolPoints = patrolPoints;
        _agent = GetComponent<NavMeshAgent>();
    }

    public void CustomUpdate()
    {
        if (isGuard) return;

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
            float randomValue = RandomGenerator.ReturnRandomPropability();

            if(randomValue > _probabilityToGuard)
            {
                SetNewDestination();
            }
            else
            {
                Startguard();
            }
        }
    }

    private void Startguard()
    {
        StartCoroutine(WanderingCoroutine());
    }    

    private IEnumerator WanderingCoroutine()
    {
        isGuard = true;
        float wanderingTimeElapsed = 0f;
        float wanderingTime = RandomGenerator.ReturnRandomFloat(_baseTimeOfEachPointBreak / 2, _baseTimeOfEachPointBreak * 2);
        if(wanderingTimeElapsed >= wanderingTime)
        {
            yield return new WaitForEndOfFrame();
        }

        isGuard = false;
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
