using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsFactory : MonoBehaviour, IFactory<GameObject, Vector3>
{
    public GameObject Create(GameObject objectOfType, Vector3 position)
    {
       GameObject newObject = Instantiate(objectOfType, position,Quaternion.identity);

        return newObject;
    }
}
