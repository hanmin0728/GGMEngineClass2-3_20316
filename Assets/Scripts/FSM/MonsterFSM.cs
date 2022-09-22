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
    //Ÿ�� �Ÿ� üũ�� ���� ����
    //public LayerMask targetLayerMask; //����ȭ�� ���� 
    //public Transform target;
    //Fov Editor�� ��ü

    public float eyeSight;


    public float atkRange;

    private FieldOfView _fov;

    public Transform _target => _fov.FirstTarget;
    /// <summary>
    ///���� ���� ������ �ʱ�ȭ
    /// </summary>
    private void Start()
    {
        fsmManager = new StateMachine<MonsterFSM>(this, new stateRoming()); //�ݽ���
        stateIdle stateIdle = new stateIdle();
        stateIdle.isRomming = true;
        fsmManager.AddStateList(stateIdle);
        fsmManager.AddStateList(new stateMove()); //������ ��� ��Ȥ
        fsmManager.AddStateList(new stateAtk());

        _fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime); //�ð��ٲ㼭 �ຸ��  
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
