using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform destroyObjects;

    public bool lockCursor = true;

    [HideInInspector] public int levelToLoad;

    public List<RestartableObject> restartableObjects;

    public GameObject gameOverCanvas;

    public bool isPaused = false;

    public Transform enemyLifeBar;

    public Camera mainCamera { get; set; }



    public LoadingNextLevel loadNextSceneBar;
    public GameObject LoadSceneCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


        restartableObjects = new List<RestartableObject>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;

        isPaused = true;
        Time.timeScale = 0;

        gameOverCanvas.SetActive(true);
    }

    public void RestartScene()
    {
        for (int i = 0; i < restartableObjects.Count; i++)
        {
            if(restartableObjects[i] != null)
                restartableObjects[i].RestartObject();
        }

        for (int i = 0; i < destroyObjects.childCount; i++)
        {
            Destroy(destroyObjects.GetChild(i).gameObject);
        }

        Time.timeScale = 1;
        isPaused = false;

        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;    
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadNextLevel(int levelToLoad)
    {
        isPaused = true;
        Time.timeScale = 1f;

        restartableObjects.Clear();
        LoadSceneCanvas.SetActive(true);

        restartableObjects.Clear();

        StartCoroutine(LoadNextScene(levelToLoad));
    }

    IEnumerator LoadNextScene(int levelToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);

        while (!asyncLoad.isDone)
        {
            if (loadNextSceneBar != null)
                loadNextSceneBar.UpdateLevelBar(asyncLoad.progress);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        isPaused = false;
        Time.timeScale = 1f;
    }
}
