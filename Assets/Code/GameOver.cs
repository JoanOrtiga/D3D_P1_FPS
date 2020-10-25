using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        GameController.instance.RestartScene();
        gameObject.SetActive(false);
    }
}
