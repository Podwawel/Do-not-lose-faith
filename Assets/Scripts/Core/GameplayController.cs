using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private AgentSystemManager _agentSystemManager;

    private void Awake()
    {
       
    }

    private void Start()
    {
        _agentSystemManager.Initialize();
    }

    private void Update()
    {
        _agentSystemManager.CustomUpdate();   
    }
}
