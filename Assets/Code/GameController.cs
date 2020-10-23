using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Transform destroyObjects;

    public bool lockCursor = true;

    [HideInInspector] public int levelToLoad;

    public List<RestartableObject> restartableObjects;

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

    }

    private void Start()
    {
        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            RestartScene();
        }
    }

    public void RestartScene()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        for (int i = 0; i < restartableObjects.Count; i++)
        {
            restartableObjects[i].RestartObject();
        }
    }

    public void LoadNextLevel(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
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
