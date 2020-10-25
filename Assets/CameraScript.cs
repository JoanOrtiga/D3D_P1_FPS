using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Awake()
    {
        print("CAMERA LOADED");

        print(GameManager.instance.mainCamera);

        GameManager.instance.mainCamera = Camera.main;
    }
}
