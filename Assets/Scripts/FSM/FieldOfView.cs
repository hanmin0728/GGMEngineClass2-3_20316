using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [Header("Sight Elements")]
    public float _eyeRadius = 5f;

    [Range(0, 360)]
    public float _eyeAngle = 90f;

    [Header("Search Elements")]
    public float _delayFindTime = 0.2f;

    public LayerMask _targetLayerMask;
    public LayerMask _blockLayerMask;

    private List<Transform> _targetList = new List<Transform>();
    private Transform _firstTarget;

    private float _distanceTarget = 0f;


    public List<Transform> TargetLists => _targetList;
    public Transform FirstTarget => _firstTarget;
    public float DistanceTarget => _distanceTarget;


    private void Start()
    {
        StartCoroutine(UpdateFindTargets(_delayFindTime));
    }

    private IEnumerator UpdateFindTargets(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindTargets();
        }
    }


    private void FindTargets()
    {
        _distanceTarget = 0f;
        _firstTarget = null;
        _targetList.Clear();

        Collider[] overlapSphereTargets
            = Physics.OverlapSphere(transform.position, _eyeRadius, _targetLayerMask);

        for (int i = 0; i < overlapSphereTargets.Length; ++i)
        {
            Transform target = overlapSphereTargets[i].transform;
            Vector3 lookatTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, lookatTarget) < _eyeAngle / 2)
            {
                float firstTargetDistance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, lookatTarget, firstTargetDistance, _blockLayerMask))
                {
                    _targetList.Add(target);

                    if (_firstTarget == null || _distanceTarget > firstTargetDistance)
                    {
                        _firstTarget = target;
                        _distanceTarget = firstTargetDistance;
                    }
                }
            }
        }
    }

    public Vector3 GetVecByAngle(float degrees, bool flagGlobalAngle)
    {
        if (!flagGlobalAngle)
        {
            degrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(degrees * Mathf.Deg2Rad), 0f, Mathf.Cos(degrees * Mathf.Deg2Rad));
    }

}
