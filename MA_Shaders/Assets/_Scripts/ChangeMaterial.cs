using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

    Renderer rend;
    Material[] mats;
    int matIndex = 0;

    void Start() {
        rend = GetComponent<Renderer>();
        mats = rend.materials;
        Debug.Log(mats[0]);
    }

	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mats[0].renderQueue += 1;
        }

	}
}
