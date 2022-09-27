using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateIdle : State<msFSM>
{
    private Animator animator;

    private CharacterController characterController;

    protected int hashMove = Animator.StringToHash("Move");
    protected int hashMoveSpd = Animator.StringToHash("MoveSpd");

    public bool isRomming = false;

    private float minIdleTime = 0f;
    private float maxIdleTime = 0f;
    private float retIdleTime = 0f;

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponentInChildren<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
    }
    public override void OnStart()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetFloat(hashMoveSpd, 0);
        characterController?.Move(Vector3.zero);

        if (isRomming)
        {
            retIdleTime = (Random.Range(minIdleTime, maxIdleTime));
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchMonster();
    
        if (target)
        {
            if (stateMachineClass.getFlagAtk)
            {
                stateMachine.ChangeState<stateAtk>();
            }
            else
            {
                stateMachine.ChangeState<stateMove>();
            }
        }
        else if (isRomming && stateMachine.getStateDurationTime > retIdleTime)
        {
            stateMachine.ChangeState<stateRoming>();
        }
    }
    public override void OnEnd() { }

}
