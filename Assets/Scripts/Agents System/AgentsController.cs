using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsController : MonoBehaviour
{
    [SerializeField]
    private AgentsDataStorage _agentsDataStorage;

    [SerializeField]
    private GameObject _agentsFactoryObject;

    private IFactory<GameObject, Vector3> _agentsFactory;

    private PatrolPoints _patrolPoints;

    private List<AgentData> _activeAgentsList = new List<AgentData>();

    [SerializeField]
    private int amountToCreate;

    public void Initialize(PatrolPoints patrolPoints)
    {
        _patrolPoints = patrolPoints;
        _agentsFactory = _agentsFactoryObject.GetComponent<IFactory<GameObject, Vector3>>();
        CreateAgentsInGivenAmount(amountToCreate, AgentType.TestAgent);
    }

    public void CreateNewAgent(AgentType agentType)
    {
        GameObject newAgent = 
        _agentsFactory.Create(_agentsDataStorage.GetAgentPrefab(agentType), _patrolPoints.GetPatrolPointPosition());
        AgentData agentData;
        agentData.AgentObject = newAgent;
        agentData.AgentType = agentType;
        agentData.AgentController = agentData.AgentObject.GetComponent<AgentPatrolPointsController>();
        _activeAgentsList.Add(agentData);
        
        agentData.AgentController.Initialize(_patrolPoints);
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
