using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateRoming : State<msFSM>
{
    private Animator animator;

    private CharacterController characterController;

    private NavMeshAgent nav;

    //private MonsterFSM monsterFSM;
    private msFSM msfsm;

    protected int hashMove = Animator.StringToHash("Move");
    protected int hashMoveSpd = Animator.StringToHash("MoveSpd");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponentInChildren<Animator>();
        nav = stateMachineClass.GetComponent<NavMeshAgent>();
        characterController = stateMachineClass.GetComponent<CharacterController>();

        //monsterFSM = stateMachineClass as MonsterFSM;
        msfsm = stateMachineClass as msFSM;
    }
    public override void OnStart()
    {
        Transform posTarget = null;
        if (stateMachineClass.posTarget == null)
        {
            posTarget = stateMachineClass.SearchNextTargetPosition();
        }

        if (posTarget)
        {
            nav?.SetDestination(posTarget.position);
            animator?.SetBool(hashMove, true);
        }

        nav.stoppingDistance = 0f;
        if (msfsm?.posTarget == null)
        {
            msfsm?.SearchNextTargetPosition();
        }
        if (msfsm?.posTarget != null)
        {
            Vector3 destination = msfsm.posTarget.position;
            nav?.SetDestination(destination);
            animator?.SetBool(hashMove, true);
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
        else
        {
            if (!nav.pathPending && (nav.remainingDistance <= nav.stoppingDistance))
            {
                SearchNextTargetPosition();
                stateMachine.ChangeState<stateIdle>();
            }
            else
            {
                characterController.Move(nav.velocity * Time.deltaTime);
                animator.SetFloat(hashMoveSpd, nav.velocity.magnitude / nav.speed, 1f, Time.deltaTime);
            }
        }

    }
    public override void OnEnd()
    {
        nav.stoppingDistance = stateMachineClass.atkRange;
        animator?.SetBool(hashMove, false);
        nav.ResetPath();
    }
    private void SearchNextTargetPosition()
    {
        Transform posTarget = msfsm.SearchNextTargetPosition();
        if (posTarget != null)
        {
            nav.SetDestination(posTarget.position);
        }
    }
}
