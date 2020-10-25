using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachLifeBarToGameManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.enemyLifeBar = transform   ;
    }
}
