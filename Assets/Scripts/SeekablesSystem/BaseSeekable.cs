using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSeekable : MonoBehaviour
{
    [SerializeField]
    private SeekableType _myType;
    public SeekableType MyType { get { return _myType; } }

    public void Initialize()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AgentChunkManager>())
             Debug.Log("£IIII");
    }

    public void PerformAction()
    {

    }    
}
