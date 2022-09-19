using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    public StateMachine<MonsterFSM> fsmManager;

    //타겟 거리 체크를 위한 변수
    public LayerMask targetLayerMask; //최적화를 위해 
    public float eyeSight;
    public Transform target;


    public float atkRange;

    /// <summary>
    ///몬스터 상태 관리자 초기화
    /// </summary>
    private void Start()
    {
        fsmManager = new StateMachine<MonsterFSM>(this, new stateIdle()); //콜싸인
        fsmManager.AddStateList(new stateMove()); //관리자 목록 등혹
        fsmManager.AddStateList(new stateAtk());
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime); //시간바꿔서 줘보기  
    }
    public Transform SearchEnemy()
    {
        target = null;

        Collider[] findTargets = Physics.OverlapSphere(transform.position, eyeSight, targetLayerMask);

        if (findTargets.Length > 0)
        {
            target = findTargets[0].transform;
        }

        return target;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, eyeSight);
    }
    public bool getFlagAtk
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
