using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RigidBodyBehavior : MonoBehaviour
{
    public float forces = 1000f;
    public float velocity = 50f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(forces, Vector3.forward, velocity);
    }
}
