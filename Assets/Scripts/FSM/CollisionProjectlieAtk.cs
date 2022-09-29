using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProjectlieAtk : MonoBehaviour
{
    public float projectileSpd;
    public GameObject ObjProjectileStartEffect;
    public GameObject ObjProjectileHitEffect;

    public AudioClip projectileStartClip;
    public AudioClip projectileHitClip;

    private bool getFlagProjectileCollid;
    private Rigidbody rigidbody;

    [HideInInspector]
    public AtkBehaviour attackBehaviour;

    [HideInInspector]
    public GameObject projectileParents;

    [HideInInspector]
    public GameObject target;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (projectileParents != null)
        {
            Collider colliderProjectile = GetComponent<Collider>(); // 자신의 콜라이더
            Collider[] collidParents = projectileParents.GetComponentsInChildren<Collider>();
            foreach (Collider collider in collidParents)
            {
                Physics.IgnoreCollision(colliderProjectile, collider);
            }
        }

        if (ObjProjectileStartEffect != null)
        {
            var projectileStartEffect = Instantiate(this.ObjProjectileStartEffect, transform.position, Quaternion.identity);

            projectileStartEffect.transform.forward = gameObject.transform.forward;

            ParticleSystem particleSystem = projectileStartEffect.GetComponent<ParticleSystem>();

            if (particleSystem != null)
            {
                Destroy(projectileStartEffect, particleSystem.main.duration);
            }
            else
            {
                ParticleSystem particleSystemChild = projectileStartEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(projectileStartEffect, particleSystemChild.main.duration);
            }
        }

        if (projectileStartClip != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(projectileStartClip);
        }

        if (target != null)
        {
            Vector3 vecProjectile = target.transform.position;
            vecProjectile.y += 1.5f;
            transform.LookAt(vecProjectile);
        }
    }
    protected virtual void FixedUpdate()
    {
        if (projectileSpd != 0 && rigidbody != null)
        {
            rigidbody.position += (transform.forward) * (projectileSpd * Time.fixedDeltaTime);
        }
    }
    protected virtual void OnProjectileStartCollision(Collision collision)
    {
        if (getFlagProjectileCollid)
        {
            return;
        }
        Collider projectileCollider = GetComponent<Collider>();
        projectileCollider.enabled = false;
        getFlagProjectileCollid = true;

        if (projectileHitClip != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(projectileHitClip);
        }
        projectileSpd = 0;
        rigidbody.isKinematic = true; //물리연산 스탑

        ContactPoint contactPoint = collision.contacts[0];
        Quaternion rotationContact = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        Vector3 vecContact = contactPoint.point;

        if (ObjProjectileHitEffect != null)
        {
            var projectileHitEffect = Instantiate(ObjProjectileHitEffect, vecContact, rotationContact) as GameObject;

            ParticleSystem particleSystem = projectileHitEffect.GetComponent<ParticleSystem>();

            if (particleSystem == null)
            {
                Destroy(projectileHitEffect, particleSystem.main.duration);
            }
            else
            {
                ParticleSystem particleSystemChild = projectileHitEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(projectileHitEffect, particleSystemChild.main.duration);
            }
        }

        IDmageAble iDmgAble = collision.gameObject.GetComponent<IDmageAble>();
        if (iDmgAble != null)
        {
            iDmgAble.setDmg(attackBehaviour?.atkDmg ?? 0, null);
        }

        StartCoroutine(DestroyParticle(0.0f));
    }

    public IEnumerator DestroyParticle(float wattingTime)
    {
        if (transform.childCount > 0 && wattingTime != 0)
        {
            List<Transform> destoryParticleTopChildds = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                destoryParticleTopChildds.Add(t);

            }


            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < destoryParticleTopChildds.Count; i++)
                {
                    destoryParticleTopChildds[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
            yield return new WaitForSeconds(wattingTime);
            Destroy(gameObject);
        }
    }
}
