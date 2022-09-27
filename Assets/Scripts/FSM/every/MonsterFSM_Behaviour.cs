using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM_Behaviour : msFSM, IAtkAble, IDmageAble
{
    public LayerMask _targetLayerMask;
    public Collider _atkItemCollider;
    private GameObject _atkEffectPrefab = null;

    public Transform _launchWeaponTransform;    // 원거리 발사체 위치
    public Transform _weaponHitTransform;       // 데미지 입는 위치


    public AtkBehaviour nowAtkBehaviour
    {
        get;
        private set;
    }

    [SerializeField]
    private List<AtkBehaviour> attackBehaviours = new List<AtkBehaviour>(); //공격행동들을 담는거
    private int maxHp = 100;
    public int MAXHp => maxHp;

    public int Hp
    {
        get;
        private set;
    }

    public bool getFlagLive => Hp > 0;

    protected override void Start()
    {
        base.Start();
        msManager.AddStateList(new stateMove());
        msManager.AddStateList(new stateAtk());
        msManager.AddStateList(new stateDie());

        OnAwakeAtkBehaviour();
    }

    protected override void Update()
    {
        OnCheckAtkBehaviour();
        base.Update();
    }

    public override bool getFlagAtk
    {
        get
        {
            if (!base.target)
            {
                return false;
            }
            float distance = Vector3.Distance(transform.position, base.target.position);
            return (distance <= atkRange);
        }
    }

    public override Transform SearchMonster()
    {
        return base.target;
    }

    public J ChangeState<J>() where J : State<msFSM>
    {
        return msManager.ChangeState<J>();
    }

    public void EnableAttackCollider()
    {
        Debug.Log("Check Attack Event");
        if (_atkItemCollider)
        {
            _atkItemCollider.enabled = true;
        }

        StartCoroutine("DisableAttackCollider");
    }

    IEnumerator DisableAttackCollider()
    {
        yield return new WaitForFixedUpdate();

        if (_atkItemCollider)
        {
            _atkItemCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1<< other.gameObject.layer) & _targetLayerMask) != 0)
        {
            Debug.Log("Attack Trigger :" + other.name);
        }
        if (((1 << other.gameObject.layer) & _targetLayerMask) == 0)
        {
            //It wasnt in an ignore layer
        }
    }
    private void OnAwakeAtkBehaviour()
    {
        foreach (AtkBehaviour behaviour in attackBehaviours)
        {
            if (nowAtkBehaviour == null)
            {
                nowAtkBehaviour = behaviour;
            }
            behaviour.targetLayerMask = _targetLayerMask;
        }
    }
    private void OnCheckAtkBehaviour()
    {
        if (nowAtkBehaviour == null || !nowAtkBehaviour.flagReady)
        {
            nowAtkBehaviour = null;

            foreach (AtkBehaviour behaviour in attackBehaviours)
            {
                if (behaviour.flagReady)
                {
                    if ((nowAtkBehaviour == null) || 
                        (nowAtkBehaviour.importanceAtkNo < behaviour.importanceAtkNo))
                    {
                        nowAtkBehaviour = behaviour;
                    }
                }
            }
        }
    }

    public void OnExecuteAttack(int atkIdx)
    {
        if (nowAtkBehaviour != null && base.target != null)
        {
            nowAtkBehaviour.callAtkMotion(base.target.gameObject, _launchWeaponTransform);
        }
    }

    public void setDmg(int dmg, GameObject prefabEffect)
    {
        if (!getFlagLive)
        {
            return;
        }
        Hp -= dmg;
        if (_atkEffectPrefab)
        {
            Instantiate(_atkEffectPrefab, _weaponHitTransform);
        }

        if (getFlagLive)
        {
            animator?.SetTrigger("hitTriggerHash");
        }
        else
        {
            msManager.ChangeState<stateDie>();
        }
    }
}
