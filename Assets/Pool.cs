using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> pooledObjects;
    public GameObject objectToPool;

    public int numberToPool;

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            pooledObjects.Add(transform.GetChild(i).gameObject);
        }

        while(transform.childCount < numberToPool)
        {
            NewObject();
        }

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].SetActive(false);
        }
    }

    public GameObject AskForObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeSelf)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }

        GameObject x = NewObject();
        x.SetActive(true);
        return x;
    }

    private GameObject NewObject()
    {
        GameObject newObjectToPool = Instantiate(objectToPool, transform);
        pooledObjects.Add(newObjectToPool);

        return newObjectToPool;
    }
}
