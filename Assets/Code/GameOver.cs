using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;

        GameManager.instance.gameOverCanvas = gameObject;
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        GameManager.instance.RestartScene();
        gameObject.SetActive(false);
    }
}
