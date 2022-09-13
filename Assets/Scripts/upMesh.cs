using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class upMesh : MonoBehaviour
{
    public int meshCost = 3;
    public float spd = 1f;

    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(Upping());
    }

    IEnumerator Upping()
    {
        while(true)
        {
            //getIsMeshUp가 참 일때 까지
            yield return new WaitUntil(() => getIsMeshUp());
            //action
            yield return StartCoroutine(UpAndDown());
        }
    }
    private bool getIsMeshUp()
    {
        //agent is OffMeshLink ? true : false

        if (navMeshAgent.isOnOffMeshLink)
        {
            //currentOffMeshLinkData ? 수동 : 자동
            OffMeshLinkData offMeshLinkData = navMeshAgent.currentOffMeshLinkData;

            if (offMeshLinkData.offMeshLink != null && offMeshLinkData.offMeshLink.area == meshCost)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator UpAndDown()
    {
        //stop navigation
        navMeshAgent.isStopped = true;

        OffMeshLinkData offMeshLinkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 posStart = offMeshLinkData.startPos;
        Vector3 posEnd = offMeshLinkData.endPos;

        //time setting : 거리 = 시간 + 속도 => 시간 + 거리 / 속도

        float upTime = Mathf.Abs(posEnd.y - posStart.y) / spd;
        float settingTime = 0f;
        float rateLength = 0f;

        while(rateLength < 1)
        {
            //deltatime 1초 ==  1 1초후 100퍼
            settingTime += Time.deltaTime;
            rateLength = settingTime / Time.deltaTime;
            transform.position = Vector3.Lerp(posStart, posEnd, rateLength);
            yield return null;
        }
        //end move
        navMeshAgent.CompleteOffMeshLink();
        //navigation start
        navMeshAgent.isStopped = false;
    }
}
