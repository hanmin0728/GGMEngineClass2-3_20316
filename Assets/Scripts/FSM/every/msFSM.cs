using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class msFSM : MonoBehaviour
{
    protected StateMachine<msFSM> msManager;
    public StateMachine<msFSM> MSManager => msManager;

    protected NavMeshAgent agent;

    protected Animator animator;

    private FieldOfView fov;

    public virtual Transform target => fov?.FirstTarget;

    public Transform[] posTargets;
    public Transform posTarget = null;

    public int posTargetIdx = 0;

    public float atkRange;

    protected virtual void Start()
    {
        msManager = new StateMachine<msFSM>(this, new stateIdle()); //콜싸인
        msManager.AddStateList(new stateMove()); //관리자 목록 등혹
        msManager.AddStateList(new stateAtk());

        fov = GetComponent<FieldOfView>();
    }
    public Transform SearchNextTargetPosition()
    {
        posTarget = null;

        if (posTargets.Length > 0)
        {
            posTarget = posTargets[posTargetIdx];
        }

        posTargetIdx = (posTargetIdx + 1) % posTargets.Length;

        return posTarget;
    }

    protected virtual void Update()
    {
        msManager.Update(Time.deltaTime);
        Debug.Log(msManager.getNowState);
    }

    public virtual Transform SearchMonster()
    {
        return target;
    }

    public virtual bool getFlagAtk
    {
        get
        {
            if (!target)
            {
                return false;
            }
            float distance = Vector3.Distance(transform.position, target.position);

            return (distance <= atkRange);
        }
    }
}
