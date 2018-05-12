using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{

    public enum AxisList { yAxis, zAxis, xAxis }
    public AxisList axis;

    Vector3 rotateAroundThis;
    public Transform target; //You rotate around this point
    public float speed = 20; //Rotating speed

    void Update()
    {

        switch (axis)
        {
            case AxisList.xAxis:
                rotateAroundThis = Vector3.right;
                break;
            case AxisList.yAxis:
                rotateAroundThis = Vector3.up;
                break;
            case AxisList.zAxis:
                rotateAroundThis = Vector3.forward;
                break;
        }

        transform.RotateAround(target.position, rotateAroundThis, speed * Time.deltaTime);
    }
}
