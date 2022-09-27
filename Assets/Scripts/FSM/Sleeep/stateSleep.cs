using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateSleep : State<SleepFSM>
{
    public override void OnAwake()
    {

    }
    public override void OnStart()
    {
    }
    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.pigondo -= Time.deltaTime;
        if (stateMachineClass.pigondo <= 0f)
        {
            stateMachine.ChangeState<stateGame>();
        }
    }

    public override void OnEnd()
    {
        stateMachineClass.pigondo = 0f;
    }
}
