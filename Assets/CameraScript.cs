using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.mainCamera = Camera.main;
    }
}
