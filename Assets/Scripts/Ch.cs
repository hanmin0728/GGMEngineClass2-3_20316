    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(CharacterController))]
public class Ch : MonoBehaviour
{
    // private Rigidbody _rb;
    private CharacterController _cc;
    // private float _spd = 5f;

    private float _jumpValue = 2f;
    private float _dashValue = 5f;
    // private Vector3 _dirValue = Vector3.zero;

    // private float _gravity = -9.81f;
    public Vector3 _drags;
    private Vector3 _calcVelocity = Vector3.zero;

    private NavMeshAgent _agent;

    public LayerMask layerGround;
    private bool _flagOnGrounded = true;
    private float _defaultGroundDistance = 0.2f;
    private Animator animator;
    public Rigidbody obstacleRigidbody;
    private void Start()
    {
        // _rb = GetComponent<Rigidbody>();
        _cc = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

    private void Update()
    {
        // CheckGroundStatus();
        // _flagOnGrounded = _cc.isGrounded;

        // if (_flagOnGrounded && _calcVelocity.y < 0)
        // {
        //     _calcVelocity.y = 0.0f;
        // }
        /*
        _dirValue = Vector3.zero;
        _dirValue.x = Input.GetAxis("Horizontal");
        _dirValue.z = Input.GetAxis("Vertical");

        if(_dirValue != Vector3.zero)
        {
            transform.forward = _dirValue;
        }
        */
        //_dirValue = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                //_dirValue = new Vector3(hit.point.x, 0f, hit.point.z);
                _agent.SetDestination(hit.point);
            }
        }

        // if (_dirValue != Vector3.zero)
        // {
        //     transform.forward = _dirValue;
        // }

        if (Input.GetButtonDown("Jump") && _flagOnGrounded)
        {
            // _rb.AddForce(Vector3.up * Mathf.Sqrt(_jumpValue * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            _calcVelocity.y += Mathf.Sqrt(_jumpValue * -2f * Physics.gravity.y);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // float posDashEnd = Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1f)) / -Time.deltaTime;
            float posDashEndX = Mathf.Log(1f / (Time.deltaTime * _drags.x + 1f)) / -Time.deltaTime;
            float posDashEndZ = Mathf.Log(1f / (Time.deltaTime * _drags.z + 1f)) / -Time.deltaTime;

            Vector3 dashVelocity = Vector3.Scale(transform.forward, _dashValue * new Vector3(posDashEndX, 0f, posDashEndZ));

            // _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
            _calcVelocity += dashVelocity;
        }

        // _calcVelocity.y += _gravity * Time.deltaTime;

        // _calcVelocity.x /= 1 + _drags.x * Time.deltaTime;
        // _calcVelocity.y /= 1 + _drags.y * Time.deltaTime;
        // _calcVelocity.z /= 1 + _drags.z * Time.deltaTime;

        // _cc.Move((_dirValue + _calcVelocity) * Time.deltaTime * _spd);

        // if (Vector3.Distance(transform.position, _dirValue) > 0.5f)
        // {
        //     _cc.Move(_dirValue.normalized * Time.deltaTime);
        // }

            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                _cc.Move(_agent.velocity * Time.deltaTime);
                animator.SetBool("Walk", true);
            }
            else
            {
                _cc.Move(Vector3.zero);
                animator.SetBool("Walk", false);
            }
    }

    private void LateUpdate()
    {
        transform.position = _agent.nextPosition;
    }

    // private void CheckGroundStatus()
    // {
    //     RaycastHit hitInfo;

    //     Vector3 posOrigin = transform.position + (Vector3.up * 0.1f);

    //     Debug.DrawLine(posOrigin, transform.position + (Vector3.up * 0.1f) + (Vector3.down * _defaultGroundDistance), Color.red);

    //     if (Physics.Raycast(posOrigin, Vector3.down, out hitInfo, _defaultGroundDistance, layerGround))
    //     {
    //         _flagOnGrounded = true;
    //     }
    //     else
    //     {
    //         _flagOnGrounded = false;
    //     }
    // }

    // private void FixedUpdate() {
    //     _rb.MovePosition(_rb.position + _dirValue * _spd * Time.fixedDeltaTime);
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obs"))
        {
            Debug.Log("Ãæµ¹");
            obstacleRigidbody.useGravity = true;
        }
    
    }
}
