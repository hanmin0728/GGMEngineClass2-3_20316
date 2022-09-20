using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenefitBehaviourTree : StateMachineBehaviour
{
    public int idleIDCount = 3;
    public float minBaseIdleTime = 0f;
    public float maxBaseIdleTime = 3f;
    protected float rndBaseIdleTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rndBaseIdleTime = Random.Range(minBaseIdleTime, maxBaseIdleTime);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            //Debug.Log("base");
            animator.SetInteger("Idle_id", -1);
        }

        if (stateInfo.normalizedTime > rndBaseIdleTime && !animator.IsInTransition(0))
        {
            Debug.Log("rand");
            animator.SetInteger("Idle_id", Random.Range(0, idleIDCount));
        }
    }
}
