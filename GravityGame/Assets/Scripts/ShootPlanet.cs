using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlanet : MonoBehaviour {

    Rigidbody rb;
    Attractor attractor;

    public Transform target;
    public float moveForce;

    void Start () {
        attractor = GetComponent<Attractor>();
        rb = GetComponent<Rigidbody>();
	}
	

	void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveForce += 10000;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveForce -= 10000;
        }

        transform.LookAt(target);

		if (Input.GetKeyDown(KeyCode.Space))
        {
            attractor.enabled = true;
            rb.AddForce(transform.forward * moveForce);
            Debug.Log("Space pressed");
        }
	}
}
