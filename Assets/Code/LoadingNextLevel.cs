using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingNextLevel : MonoBehaviour
{
    Image loadingBar;

    private void OnEnable()
    {
        GameManager.instance.loadNextSceneBar = this;
        loadingBar = GetComponent<Image>();
    }

    public void UpdateLevelBar(float percentatge)
    {
        if(loadingBar != null)
            loadingBar.fillAmount = percentatge;
    }
}
