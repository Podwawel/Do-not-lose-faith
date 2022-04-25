using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AgentsDataStorage : ScriptableObject
{
    [SerializeField]
    private List<AgentData> _agentsList;

    public GameObject GetAgentPrefab(AgentType agentType)
    {
        AgentData agentData = GetAgentDataByType(agentType);

        return agentData.AgentObject;
    }

    private AgentData GetAgentDataByType(AgentType agentType)
    {
        for(int i = 0; i < _agentsList.Count; i ++)
        {
            if(_agentsList[i].AgentType == agentType)
            {
                return _agentsList[i];
            }
        }

        AgentData nullData;
        nullData.AgentType = AgentType.None;
        nullData.AgentObject = null;
        nullData.AgentController = null;
        Debug.LogError("CUSTOM ERROR: Agent nout found");
        return nullData;
    }
}

[System.Serializable]
public enum AgentType
{
    None = 0,
    TestAgent = 1,
}
[System.Serializable]
public struct AgentData
{
    public AgentType AgentType;
    public GameObject AgentObject;
    public AgentPatrolPointsController AgentController;
}
