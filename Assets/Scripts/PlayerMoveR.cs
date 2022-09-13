using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveR : MonoBehaviour
{
    Rigidbody rigidbody = null;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpValue = 2f;
    [SerializeField] private float dashValue = 5f;

    Vector3 directionValue = Vector3.zero;

    public LayerMask layerGround;
    private bool flagOnGrouned = true;
    private float defaultGroundDistance = 0.2f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGroundStatus();

        directionValue = Vector3.zero;
        directionValue.x = Input.GetAxis("Horizontal");
        directionValue.z = Input.GetAxis("Vertical");

        if (directionValue != Vector3.zero)
        {
            transform.forward = directionValue;
        }

        if (Input.GetButtonDown("Jump") && flagOnGrouned)
        {
            rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpValue * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            float posDashEnd = Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime;
            // Mathf.Log 자연스러운 지연
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashValue * new Vector3(posDashEnd, 0, posDashEnd));

            rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
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

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + directionValue * speed * Time.fixedDeltaTime);
    }

}
