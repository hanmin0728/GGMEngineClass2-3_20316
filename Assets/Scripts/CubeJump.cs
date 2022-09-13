using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeJump : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float spd = 5f;
    public Vector3 vecJump = Vector3.zero;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        float jump = Input.GetAxis("Jump");
        vecJump = new Vector3(0, jump, 0);
    }
    private void FixedUpdate()
    {
        rigidbody.AddForce(vecJump * spd, ForceMode.Impulse);
    }
}
