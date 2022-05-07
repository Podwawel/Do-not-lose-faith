using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAgentController : MonoBehaviour
{
    private AgentChunkManager _agentChunkManager;
    private AgentPatrolPointsController _agentPatrolPointsController;
    public void Initialize(ChunksController chunksController, PatrolPoints patrolPoints)
    {
        _agentChunkManager = GetComponent<AgentChunkManager>();
        _agentPatrolPointsController = GetComponent<AgentPatrolPointsController>();

        if (_agentChunkManager != null) _agentChunkManager.Initialize(chunksController);
        if (_agentPatrolPointsController != null) _agentPatrolPointsController.Initialize(patrolPoints);
    }

    public void CustomUpdate()
    {
        if(_agentPatrolPointsController != null) _agentPatrolPointsController.CustomUpdate();
        if(_agentChunkManager != null) _agentChunkManager.CustomUpdate();
    }

    public void Deinitialize()
    {
        if (_agentChunkManager != null) _agentChunkManager.Deinitialize();
        if (_agentPatrolPointsController != null) _agentPatrolPointsController.Deinitialize();
    }
}

