using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    protected StateMachine<MonsterFSM> fsmManager;
    public StateMachine<MonsterFSM> FsmManager => fsmManager;

    public Transform[] posTargets;
    public Transform posTarget = null;

    public int posTargetIdx = 0;
    //타겟 거리 체크를 위한 변수
    //public LayerMask targetLayerMask; //최적화를 위해 
    //public Transform target;
    //Fov Editor로 대체

    public float eyeSight;


    public float atkRange;

    private FieldOfView _fov;

    public Transform _target => _fov.FirstTarget;
    /// <summary>
    ///몬스터 상태 관리자 초기화
    /// </summary>
    private void Start()
    {
        fsmManager = new StateMachine<MonsterFSM>(this, new stateRoming()); //콜싸인
        stateIdle stateIdle = new stateIdle();
        stateIdle.isRomming = true;
        fsmManager.AddStateList(stateIdle);
        fsmManager.AddStateList(new stateMove()); //관리자 목록 등혹
        fsmManager.AddStateList(new stateAtk());

        _fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime); //시간바꿔서 줘보기  
        Debug.Log(fsmManager.getNowState);
    }
    public Transform SearchEnemy()
    {
        //target = null;

        //Collider[] findTargets = Physics.OverlapSphere(transform.position, eyeSight, targetLayerMask);

        //if (findTargets.Length > 0)
        //{
        //    target = findTargets[0].transform;
        //}

        return _target;
    }

    public bool getFlagAtk
    {
        get
        {
            if (!_target)
            {
                return false;
            }
            float distance = Vector3.Distance(transform.position, _target.position);

            return (distance <= atkRange);
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, eyeSight);


        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, atkRange);
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
}
