using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsFactory : MonoBehaviour, IFactory<GameObject>
{
    public GameObject Create(GameObject objectOfType)
    {
       GameObject newObject = Instantiate(objectOfType);

        return newObject;
    }
}
