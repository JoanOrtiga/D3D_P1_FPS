using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartableObject : MonoBehaviour
{
    protected Vector3 initialPosition;
    protected Quaternion initialRotation;

    protected virtual void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        GameManager.instance.restartableObjects.Add(this);
    }

    public virtual void RestartObject()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public virtual void SaveStateCheckPoint()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
}
