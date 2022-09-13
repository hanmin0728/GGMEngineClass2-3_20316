using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigiControl : MonoBehaviour
{

    private Rigidbody rigidbody;

    public float spd = 5f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.AddForce(movement * spd);
    }
}
