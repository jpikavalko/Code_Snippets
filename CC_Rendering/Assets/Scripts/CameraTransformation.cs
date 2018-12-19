using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformation : Transformation {

    public float focalLength = 2;

    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(focalLength, 0f, 0f, 0f));
            matrix.SetRow(1, new Vector4(0f, focalLength, 0f, 0f));
            matrix.SetRow(2, new Vector4(0f, 0f, 0f, 0f)); 
            matrix.SetRow(3, new Vector4(0f, 0f, 1f, 0f));
            return matrix;
        }
    }
}

//without focal length and orthographic projection matrix would be:
// 1 0 0 0
// 0 1 0 0
// 0 0 1 0
// 0 0 0 1
