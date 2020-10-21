using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public FPS_CharacterController player;
    public Transform destroyObjects;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
