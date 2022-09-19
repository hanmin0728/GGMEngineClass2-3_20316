using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateAtk : State<MonsterFSM>
{
    private Animator animator;

    protected int hashAttack = Animator.StringToHash("Attack");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponentInChildren<Animator>();
    }

    public override void OnStart()
    {
        Debug.Log("╬Нец");
        if (stateMachineClass.getFlagAtk)
        {
            animator?.SetTrigger(hashAttack);
        }
        else
        {
            stateMachine.ChangeState<stateIdle>();
        }
    } 

    public override void OnUpdate(float deltaTime) { }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
