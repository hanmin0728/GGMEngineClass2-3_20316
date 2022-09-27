using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateEat : State<SleepFSM>
{
    public override void OnAwake()
    {

    }
    public override void OnStart()
    {
    }
    public override void OnUpdate(float deltaTime)
    {
        stateMachineClass.bagopa += Time.deltaTime;
        if (stateMachineClass.bagopa >= 7f)
        {
            stateMachine.ChangeState<stateGame>();
        }
    }
    public override void OnEnd()
    {
        stateMachineClass.bagopa = 7f;
    }

}
