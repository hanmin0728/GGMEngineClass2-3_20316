using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateAtk : State<msFSM>
{
    private Animator animator;
    private stateAtkController stateAtkCtrl;
    private IAtkAble iAtkAble;

    protected int atkTriggerHash = Animator.StringToHash("Atk");
    protected int atkIndexHash = Animator.StringToHash("AtkIdx");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponentInChildren<Animator>();
        stateAtkCtrl = stateMachineClass.GetComponent<stateAtkController>();

        iAtkAble = stateMachineClass.GetComponent<IAtkAble>();
    }

    public override void OnStart()
    {
        if (iAtkAble == null || iAtkAble.nowAtkBehaviour == null)
        {
            stateMachine.ChangeState<stateIdle>();
        }

        stateAtkCtrl.stateAtkControllerStartHandler += delegateAtkStateStart;
        stateAtkCtrl.stateAtkControllerEndHandler += delegateAtkStateEnd;

        animator?.SetInteger(atkIndexHash, iAtkAble.nowAtkBehaviour.aniMotionIdx);
        animator?.SetTrigger(atkTriggerHash);
    }
    public void delegateAtkStateEnd()
    {
        Debug.Log("delegateAtkStateEnd()");
        stateMachine.ChangeState<stateIdle>();
    }
    public void delegateAtkStateStart()
    {
        Debug.Log("delegateAtkStateStart()");
    }

    public override void OnUpdate(float deltaTime) 
    {
        
    }

    public override void OnEnd()
    {

    }
}
