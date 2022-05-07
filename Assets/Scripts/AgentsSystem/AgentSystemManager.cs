using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSystemManager : MonoBehaviour
{
    [SerializeField]
    private List<AgentsByTypeControllers> _agentsByTypesControllers;

    [SerializeField]
    private ChunksController _chunksController;

    [SerializeField]
    private AgentsDataStorage _agentsDataStorage;

    public void Initialize()
    {
        for(int i = 0; i < _agentsByTypesControllers.Count; i++)
        {
            _agentsByTypesControllers[i].Controller.Initialize(_agentsDataStorage, _chunksController);
        }
    }

    public void CustomUpdate()
    {
        for (int i = 0; i < _agentsByTypesControllers.Count; i++)
        {
            _agentsByTypesControllers[i].Controller.CustomUpdate();
        }
    }

    public void Deinitialize()
    {
        for (int i = 0; i < _agentsByTypesControllers.Count; i++)
        {
            _agentsByTypesControllers[i].Controller.Deinitialize();
        }
    }
}

[System.Serializable]
public struct AgentsByTypeControllers
{
    public AgentsController Controller;
    public AgentType MyType;
}