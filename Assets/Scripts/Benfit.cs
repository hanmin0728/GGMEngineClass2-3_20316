using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(CharacterController))]

public class Benfit : MonoBehaviour
{
    private CharacterController _cc;

    public enum playerState
    {
        Idle,
        Walk,
        Run
    }
    public playerState p = playerState.Idle;
    private float _jumpValue = 2f;
    private float _dashValue = 5f;
    
    public Vector3 _drags;
    private Vector3 _calcVelocity = Vector3.zero;

    private NavMeshAgent _agent;

    public LayerMask layerGround;
    private bool _flagOnGrounded = true;
    private float _defaultGroundDistance = 0.2f;
    private Animator animator;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

    private void Update()
    {
      

        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
            }
        }

      
        if (Input.GetButtonDown("Jump") && _flagOnGrounded)
        {
            _calcVelocity.y += Mathf.Sqrt(_jumpValue * -2f * Physics.gravity.y);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            float posDashEndX = Mathf.Log(1f / (Time.deltaTime * _drags.x + 1f)) / -Time.deltaTime;
            float posDashEndZ = Mathf.Log(1f / (Time.deltaTime * _drags.z + 1f)) / -Time.deltaTime;

            Vector3 dashVelocity = Vector3.Scale(transform.forward, _dashValue * new Vector3(posDashEndX, 0f, posDashEndZ));

            _calcVelocity += dashVelocity;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            animator.SetBool("Walk", true);
            _cc.Move(_agent.velocity * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walk", false);
            _cc.Move(Vector3.zero);
        }
    }



    private void LateUpdate()
    {
        transform.position = _agent.nextPosition;
    }
}
