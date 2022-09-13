using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class PlayerMoveC: MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpValue = 2f;
    [SerializeField] private float dashValue = 5f;


    Vector3 directionValue = Vector3.zero;

    private float gravity = -9.8f;
    public Vector3 drags;
    private Vector3 calcVelocitty = Vector3.zero;

    public LayerMask layerGround;
    private bool flagOnGrouned = true;
    private float defaultGroundDistance = 0.2f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGroundStatus();

        flagOnGrouned = characterController.isGrounded;

        if (flagOnGrouned && calcVelocitty.y < 0)
        {
            calcVelocitty.y = 0.0f; //가속도가 0이면 중력도 0이다
        } //물리엔진아니니까 업데이트

        directionValue = Vector3.zero;
         directionValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //if (Input.GetMouseButton(1))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        //    {
        //        directionValue = new Vector3(hit.point.x, 0, hit.point.z);
        //    }
        //}
        var _dirction = directionValue - transform.position;

        if (directionValue != Vector3.zero)
        {
            transform.forward = _dirction;
        }

        if (Input.GetButtonDown("Jump") && flagOnGrouned)
        {
            //물리엔진 대체자
            //rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpValue * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            calcVelocitty.y += Mathf.Sqrt(jumpValue * -2f * Physics.gravity.y);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //float posDashEnd = Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime;
            float posDashEndX = Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime;
            float posDashEndZ = Mathf.Log(1f / (Time.deltaTime * drags.z + 1)) / -Time.deltaTime;

            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashValue * new Vector3(posDashEndX, 0, posDashEndZ));

            //rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
            calcVelocitty += dashVelocity;
        }

        calcVelocitty.y += gravity * Time.deltaTime;
        calcVelocitty.x /= 1 + drags.x * Time.deltaTime;
        calcVelocitty.y /= 1 + drags.y * Time.deltaTime;
        calcVelocitty.z /= 1 + drags.z * Time.deltaTime;

        if (Vector3.Distance(transform.position, directionValue) > 0.5f)
        {
           // characterController.Move(_dirction.normalized * Time.deltaTime * speed);
        }
        characterController.Move((directionValue + calcVelocitty) * Time.deltaTime * speed);
    }

    private void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        Vector3 posOrigin = transform.position + (Vector3.up * 0.1f);

        Debug.DrawLine(posOrigin, transform.position + (Vector3.up * 0.1f) + (Vector3.down * defaultGroundDistance));

        if (Physics.Raycast(posOrigin, Vector3.down, out hitInfo, defaultGroundDistance, layerGround))
        {
            flagOnGrouned = true;
        }
        else
        {
            flagOnGrouned = false;
        }
    }

}
