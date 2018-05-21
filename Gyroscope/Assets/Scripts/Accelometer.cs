using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelometer : MonoBehaviour {

    private Rigidbody rigid;

	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	void Update () {
        Vector3 tilt = Input.acceleration;

        rigid.AddForce(tilt);
	}
}
