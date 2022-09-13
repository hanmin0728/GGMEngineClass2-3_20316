using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTopDownCamera : MonoBehaviour
{
    public float camHeight = 5f;
    public float camDistance = 10f;
    public float camAngle = 45f;
    public float _camFollowTime = 0.5f;

    public float targetHeight = 2f;

    private Vector3 refVelocity;

    public Transform camTarget;


    //private Transform cameraTransform = null;
    //public GameObject objTarget;
    //private Transform objTargetTransform = null;
    //public float distance = 6f;


    ////추가된 높이
    //public float height = 1.75f;

    ////높이 Damp
    //public float heightDamp = 2f;

    ////각도 Damp
    //public float rotateDamp = 3f;

    void Start()
    {
        //cameraTransform = transform;

        //if (objTarget != null)
        //{
        //    objTargetTransform = objTarget.transform;
        //}
    }
    private void LateUpdate()
    {
        //ThirdCamera();
        TopDownFollowCamera();
    }

    public void TopDownFollowCamera()
    {
        if (!camTarget)
        {
            return;
        }
        Vector3 posCam = (Vector3.forward * -camDistance) + (Vector3.up * camHeight);
        Debug.DrawLine(camTarget.position, posCam, Color.red);

        Vector3 posRotated = Quaternion.AngleAxis(camAngle, Vector3.up) * posCam;
        Debug.DrawLine(camTarget.position, posRotated, Color.green);

        Vector3 posCamTarget = camTarget.position;
        posCamTarget.y += targetHeight;

        Vector3 lastPosCam = posCamTarget + posRotated;
        Debug.DrawLine(camTarget.position, lastPosCam, Color.blue);

        transform.position = Vector3.SmoothDamp(transform.position, lastPosCam, ref refVelocity, _camFollowTime);
        transform.LookAt(camTarget.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (camTarget)
        {
            Vector3 posEnd = camTarget.position;
            posEnd.y += targetHeight;

            Gizmos.DrawLine(transform.position, posEnd);
            Gizmos.DrawSphere(posEnd, 0.25f);
        }
        Gizmos.DrawSphere(transform.position, 0.25f);
    }

    void ThirdCamera()
    {
        //float objTargetRotationAngle = objTargetTransform.eulerAngles.y;//목표로 하는 오브젝트의 y각
        //float objHeight = objTargetTransform.position.y + height;


        //float nowRotationAngle = cameraTransform.eulerAngles.y; //현재 카메라의 y
        //float nowHeight = cameraTransform.position.y;

        //nowRotationAngle = Mathf.LerpAngle(nowRotationAngle, objTargetRotationAngle, rotateDamp * Time.deltaTime);

        //nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamp * Time.deltaTime);

        //Quaternion nowRotation = Quaternion.Euler(0f, nowRotationAngle, 0f);

        //cameraTransform.position = objTargetTransform.position;

        ////                              ↓↓↓ 방향벡터 ↓↓↓              ↓거리↓
        //cameraTransform.position -= nowRotation * Vector3.forward * distance;

        //cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);

        //cameraTransform.LookAt(objTargetTransform);
    }
}
