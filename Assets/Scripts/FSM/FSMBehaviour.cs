using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBehaviour : StateMachineBehaviour
{
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.transform.parent.GetComponent<stateAtkController>().EventStateAtkStart();
    //}
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.transform.parent.GetComponent<stateAtkController>().EventStateAtkEnd();
    //}

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.GetComponent<msFSM>().MSManager.ChangeState<stateIdle>();
        //animator.transform.parent.GetComponent<MonsterFSM>().FsmManager.ChangeState<stateIdle>();
    }
}
