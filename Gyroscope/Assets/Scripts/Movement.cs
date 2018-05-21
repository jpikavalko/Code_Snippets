using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    public Button left;
    public Button right;
    public Text test;

	void Start () {
		
	}
	

	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector3 pos = Input.GetTouch(0).deltaPosition;
            Debug.Log(pos);
        }
	}

    public void LeftButton()
    {
        test.text = "left";
        Debug.Log("left");
    }

    public void RightButton()
    {
        test.text = "right";
        Debug.Log("right");
    }

}
