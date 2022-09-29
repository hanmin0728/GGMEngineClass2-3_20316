using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTypeAtkBehaviour : AtkBehaviour
{
    public CollisionMeleeAtk collisionMeleeAtk;
    public override void callAtkMotion(GameObject target = null, Transform posAtkStart = null)
    {
        Collider[] colliders = collisionMeleeAtk?.CheckcOverlapBox(targetLayerMask);

        foreach (Collider col in colliders)
        {
            col.gameObject.GetComponent<IDmageAble>()?.setDmg(atkDmg, atkEffectPrefab);
        }
    }

}
