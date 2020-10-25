using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.gameOverCanvas = gameObject;
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        GameManager.instance.RestartScene();
        gameObject.SetActive(false);
    }
}
