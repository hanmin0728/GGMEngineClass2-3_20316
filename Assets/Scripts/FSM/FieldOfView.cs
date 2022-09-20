using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Sight Elements")]
    public float eyeRadius = 5f;
    [Range(0, 360)]
    public float eyeAngle = 90f;

    [Header("Search Elements")]
    public float delayFindTime = 0.2f;

    public LayerMask targetLayerMask;
    public LayerMask blocktLayerMask;

    private List<Transform> targetLists = new List<Transform>();
    private Transform firstTarget;
    private float distanceTarget = 0f;

    public List<Transform> TargetLists => targetLists;
    public Transform FirstTarget => firstTarget;
    public float DistanceTarget => distanceTarget;  

    private void Start()
    {
        StartCoroutine("UpdateFindTargets", delayFindTime);
    }
    IEnumerator UpdateFindTargets(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindTargets();
    }
    void FindTargets()
    {

        distanceTarget = 0.0f;
        firstTarget = null;
        targetLists.Clear();

        Collider[] overlapSphereTargets = Physics.OverlapSphere(transform.position, eyeRadius, targetLayerMask);

        for (int i = 0; i < overlapSphereTargets.Length; i++)
        {
            Transform target = overlapSphereTargets[i].transform;
            Vector3 LookAtTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, LookAtTarget) < eyeAngle / 2)
            {
                float firstTargetDistance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, LookAtTarget, firstTargetDistance, blocktLayerMask))
                {
                    targetLists.Add(target);
                    if (firstTarget == null || distanceTarget > firstTargetDistance)
                    {
                        firstTarget = target;
                        distanceTarget = firstTargetDistance;
                    }
                }
            }
        }
    }
 
    public Vector3 getVecByAngle(float degrees, bool flagGloablAngle)
    {
        if (!flagGloablAngle)
        {
            degrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(degrees * Mathf.Deg2Rad), 0, Mathf.Cos(degrees * Mathf.Deg2Rad));
    }
}
