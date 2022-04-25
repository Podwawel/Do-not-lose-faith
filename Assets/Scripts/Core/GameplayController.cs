using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private PatrolPoints _patrolPoints;
    [SerializeField]
    private AgentsController _agentController;

    private void Awake()
    {
       
    }

    private void Start()
    {
        _agentController.Initialize(_patrolPoints);
    }

    private void Update()
    {
        _agentController.CustomUpdate();   
    }
}
