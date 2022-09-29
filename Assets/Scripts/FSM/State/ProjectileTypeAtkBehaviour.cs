using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeAtkBehaviour : AtkBehaviour
{
    public override void callAtkMotion(GameObject target = null, Transform posAtkStart = null)
    {
        if (target == null && posAtkStart != null)
        {
            return;
        }
        Vector3 vecProjectile = posAtkStart?.position ?? transform.position;

        if (atkEffectPrefab != null)
        {
            GameObject objProjectile = GameObject.Instantiate<GameObject>(atkEffectPrefab, vecProjectile, Quaternion.identity);

            objProjectile.transform.forward = transform.forward;

            CollisionProjectlieAtk projectile = objProjectile.GetComponent<CollisionProjectlieAtk>();

            if (projectile != null)
            {
                projectile.projectileParents = this.gameObject;
                projectile.target = target;
                projectile.attackBehaviour = this;
            }
        }
        nowAtkCoolTime = 0f;

        


    }

}
