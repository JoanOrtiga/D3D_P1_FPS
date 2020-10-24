using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartableObject : MonoBehaviour
{
    private Vector3 m_InitialPosition;
    private Quaternion m_InitialRotation;

    protected virtual void Start()
    {
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;

        GameController.instance.restartableObjects.Add(this);
    }

    public virtual void RestartObject()
    {
        transform.position = m_InitialPosition;
        transform.rotation = m_InitialRotation;
    }

    public virtual void SaveStateCheckPoint()
    {
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
    }
}
