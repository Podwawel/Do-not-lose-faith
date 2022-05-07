using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsController : MonoBehaviour
{
    [SerializeField]
    private AgentType _myAgentsType;
    [SerializeField]
    private GameObject _agentsFactoryObject;

    private IFactory<GameObject, Vector3> _agentsFactory;

    [SerializeField]
    private PatrolPoints _patrolPoints;

    private ChunksController _chunksController;

    private AgentsDataStorage _agentsDataStorage;

    private List<AgentData> _activeAgentsList = new List<AgentData>();

    [SerializeField]
    private int amountToCreate;

    public void Initialize(AgentsDataStorage agentsDataStorage, ChunksController chunksController)
    {
        _chunksController = chunksController;
        _agentsDataStorage = agentsDataStorage;
        _agentsFactory = _agentsFactoryObject.GetComponent<IFactory<GameObject, Vector3>>();
        CreateAgentsInGivenAmount(amountToCreate, _myAgentsType);
    }

    public void CreateNewAgent(AgentType agentType)
    {
        GameObject newAgent = 
        _agentsFactory.Create(_agentsDataStorage.GetAgentPrefab(agentType), _patrolPoints.GetPatrolPointPosition());
        AgentData agentData;
        agentData.AgentObject = newAgent;
        agentData.AgentType = agentType;
        agentData.AgentController = agentData.AgentObject.GetComponent<SingleAgentController>();
        _activeAgentsList.Add(agentData);

        agentData.AgentController.Initialize(_chunksController, _patrolPoints);
    }

    public void CreateAgentsInGivenAmount(int amount, AgentType agentType)
    {
        for(int i = 0; i < amount; i++)
        {
            CreateNewAgent(agentType);
        }
    }

    private void UpdateAllActiveAgents()
    {
        for (int i = 0; i < _activeAgentsList.Count; i++)
        {
            _activeAgentsList[i].AgentController.CustomUpdate();
        }
    }

    public void CustomUpdate()
    {
        UpdateAllActiveAgents();
    }

    public void Deinitialize()
    {

    }

}

[System.Serializable]
public struct AgentData
{
    public AgentType AgentType;
    public GameObject AgentObject;
    public SingleAgentController AgentController;
}