using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour {

    //Transform targetCube;

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.Translate(new Vector3(-1f,0f,0f));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.Translate(new Vector3(1f, 0f, 0f));
        }

        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.right * Time.deltaTime);

        // ...also rotate around the World's Y axis
       // transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
    }
}
