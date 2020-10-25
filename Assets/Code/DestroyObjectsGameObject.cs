using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsGameObject : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.destroyObjects = transform;
    }
}
