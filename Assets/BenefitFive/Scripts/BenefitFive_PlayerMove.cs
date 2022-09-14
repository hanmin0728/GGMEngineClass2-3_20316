using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BenefitFive_PlayerMove : MonoBehaviour
{
    private CharacterController _chararcterController;
    private NavMeshAgent _nav;
    private Animator _animator;

    public Rigidbody obstacleRigidbody;
    public GameObject obstacle;
    private void Start()
    {
        _chararcterController = GetComponent<CharacterController>();
        _nav = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _nav.updatePosition = false;
        _nav.updateRotation = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _nav.SetDestination(hit.point);
            }
        }

        if (_nav.remainingDistance > _nav.stoppingDistance)
        {
            _animator.SetBool("Walk", true);
            _chararcterController.Move(_nav.velocity * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("Walk", false);
            _chararcterController.Move(Vector3.zero);
        }
    }

    private void LateUpdate()
    {
        transform.position = _nav.nextPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obs"))
        {
            Debug.Log("Ãæµ¹");
            obstacleRigidbody.useGravity = true;
        }
        if (other.gameObject.CompareTag("obsUP"))
        {
            obstacleRigidbody.useGravity = false;
            obstacle.transform.position = new Vector3(8.9f, 2.7f, -6.9f);
        }
    }
}
