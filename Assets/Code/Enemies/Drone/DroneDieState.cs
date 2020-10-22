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

    }

    public override void Exit(DroneMachine entity)
    {

    }
}
