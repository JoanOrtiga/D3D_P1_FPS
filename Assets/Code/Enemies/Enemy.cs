using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : RestartableObject
{
    [Header("HP BAR")]
    public GameObject hpBarPrefab;
    protected Image hpBar;
    public Vector3 hpBarOffSet = Vector3.up * 0.5f;

    protected override void Start()
    {
        GameObject bar = Instantiate(hpBarPrefab, GameController.instance.enemyLifeBar);

        hpBar = bar.GetComponent<Image>();

        base.Start();
    }

    public override void RestartObject()
    {
        gameObject.SetActive(true);

        base.RestartObject();

        
    }
}
