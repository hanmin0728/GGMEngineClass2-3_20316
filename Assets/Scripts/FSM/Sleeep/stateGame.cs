using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateGame : State<SleepFSM>
{
    public override void OnAwake()
    {
    }
    public override void OnStart()
    {
    }
    public override void OnUpdate(float deltaTime)
    {
        Debug.Log("AS");
        stateMachineClass.pigondo += Time.deltaTime;
        stateMachineClass.bagopa -= Time.deltaTime;

        if (stateMachineClass.bagopa <= 0f)
        {
            stateMachine.ChangeState<stateEat>();
        }
      else  if (stateMachineClass.pigondo >= 4f)
        {
            stateMachine.ChangeState<stateSleep>();
        }
    }

    public override void OnEnd()
    {
    }
}
