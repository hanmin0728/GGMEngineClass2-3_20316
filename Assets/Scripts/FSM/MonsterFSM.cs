using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    public StateMachine<MonsterFSM> fsmManager;

    //Ÿ�� �Ÿ� üũ�� ���� ����
    public LayerMask targetLayerMask; //����ȭ�� ���� 
    public float eyeSight;
    public Transform target;


    public float atkRange;

    /// <summary>
    ///���� ���� ������ �ʱ�ȭ
    /// </summary>
    private void Start()
    {
        fsmManager = new StateMachine<MonsterFSM>(this, new stateIdle()); //�ݽ���
        fsmManager.AddStateList(new stateMove()); //������ ��� ��Ȥ
        fsmManager.AddStateList(new stateAtk());
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime); //�ð��ٲ㼭 �ຸ��  
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
