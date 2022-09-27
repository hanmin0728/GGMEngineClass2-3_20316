using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtkBehaviour : MonoBehaviour
{
    public int aniMotionIdx;
    public int importanceAtkNo;
    public int atkDmg;
    public float atkRange = 3f;

    [SerializeField]
    private float atkCoolTime;
    protected float nowAtkCoolTime = 0f;

    public GameObject atkEffectPrefab;
    public bool flagReady => nowAtkCoolTime >= atkCoolTime;
    public LayerMask targetLayerMask;

    protected void Start()
    {
        nowAtkCoolTime = atkCoolTime;
    }

    protected void Update()
    {
        if (nowAtkCoolTime < atkCoolTime)
        {
            nowAtkCoolTime += Time.deltaTime;
        }
    }

    public abstract void callAtkMotion(GameObject target = null, Transform posAtkStart = null);

#if UNITY_EDITOR
    [Multiline]
    public string devComment = "";
#endif 
}
