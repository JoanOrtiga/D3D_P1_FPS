using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDieState : State<DroneMachine>
{
    public static DroneDieState Instance { get; private set; }
    static DroneDieState()
    {
        Instance = new DroneDieState();
    }


    public override void Enter(DroneMachine entity)
    {
        entity.timer = 0.0f;
    }

    public override void Execute(DroneMachine entity)
    {
        float graphValue = entity.fadeOut.Evaluate(entity.timer);
        entity.timer += Time.deltaTime;

        FadeOut(entity, graphValue);
    }

    public override void Exit(DroneMachine entity)
    {

    }

    private void FadeOut(DroneMachine entity, float graphValue)
    {
        for (int i = 0; i < entity.droneRenderer.Length; i++)
        {
            entity.droneRenderer[i].material.color = new Color(entity.droneRenderer[i].material.color.r, entity.droneRenderer[i].material.color.g, entity.droneRenderer[i].material.color.b, graphValue);
        }

        if (entity.droneRenderer[0].material.color.a <= 0)
        {
            GameObject drop = CalculateDropChance(entity);

            if(drop!=null)
                GameObject.Instantiate(drop, entity.transform.position + Vector3.down * 2.3f, new Quaternion(), GameManager.instance.destroyObjects);

            entity.gameObject.SetActive(false);
        }
    }

    private GameObject CalculateDropChance(DroneMachine entity)
    {
        int randomNumber = Random.Range(0, 100);

        int minChance = 0;

        for (int i = 0; i < entity.droppingItems.Count; i++)
        {
            if (randomNumber > minChance && randomNumber < entity.droppingItems[i].droppingChance + minChance)
            {
                return entity.droppingItems[i].item;
            }

            minChance += entity.droppingItems[i].droppingChance;
        }

        return null;
    }
}
