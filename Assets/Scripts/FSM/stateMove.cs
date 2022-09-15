using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateMove : State<MonsterFSM>
{
    private Animator animator;

    private CharacterController characterController;

    protected int hashMove = Animator.StringToHash("Move");
    protected int hashMoveSpd = Animator.StringToHash("MoveSpd");

    private NavMeshAgent nav;
    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        nav = stateMachineClass.GetComponent<NavMeshAgent>();
    }
    public override void OnStart()
    {
        nav?.SetDestination(stateMachineClass.target.position);
        animator?.SetBool(hashMove, true);
    }

    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
        if (target)
        {
            nav.SetDestination(stateMachineClass.target.position);

            if (nav.remainingDistance > nav.stoppingDistance)
            {
                characterController.Move(nav.velocity * Time.deltaTime);
                animator.SetFloat(hashMoveSpd, nav.velocity.magnitude / nav.speed, 0.1f, Time.deltaTime);
                return;
            }
            
            stateMachine.ChangeState<stateIdle>();
        }
    }

    public override void OnEnd()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetFloat(hashMoveSpd, 0);

        nav.ResetPath();
    }
}
