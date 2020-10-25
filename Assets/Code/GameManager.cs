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
            Destroy(this);
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
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        for (int i = 0; i < restartableObjects.Count; i++)
        {
            restartableObjects[i].RestartObject();
        }

        for (int i = 0; i < destroyObjects.childCount; i++)
        {
            Destroy(destroyObjects.GetChild(i));
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

        StartCoroutine(LoadNextScene(levelToLoad));
    }

    IEnumerator LoadNextScene(int levelToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);

        while (!asyncLoad.isDone)
        {
            loadNextSceneBar.UpdateLevelBar(asyncLoad.progress);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        isPaused = false;
        Time.timeScale = 1f;
    }

    /* [Header("UI")]
     public RectTransform m_UI;
     public RectTransform m_LifeBarEnemy;

     void Update()
     {
         if (m_Enemies.Count < 0)
         {
             Vector3 l_ViewportPoint = m_Player.m_Camera.WorldToViewportPoint(m_Enemies[0].transform.position);
             Vector3 l_EnemyPositionOnCanvas = new Vector3(l_ViewportPoint.x * m_UI.sizeDelta.x, l_ViewportPoint.y * m_UI.sizeDelta.y, 0f);
             m_LifeBarEnemy.anchoredPosition = l_EnemyPositionOnCanvas;
             m_LifeBarEnemy.gameObject.SetActive(l_ViewportPoint.z > 0f);
         }
     }

     public void RestartGame()
     {
         m_Player.RestartGame();
         foreach (DroneEnemy l_Enemy in m_Enemies)
             l_Enemy.RestartGame();
     }
    */
}
