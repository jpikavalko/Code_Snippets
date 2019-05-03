using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleDraw : MonoBehaviour
{
    private readonly float thetaScale = 0.1f;  //Set lower to add more points
    private readonly float circleRadius = 3f;
    private readonly List<Vector3> positions = new List<Vector3>();

    private LineRenderer lineRenderer;
    private Vector3 pos;
    private int size;


    private void Awake()
    {
        float sizeValue = (1 / thetaScale + 1);
        size = (int)sizeValue;
        size++;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = size;

        DrawCircle();
    }

    private void DrawCircle()
    {
        
        float theta = 0f;
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * thetaScale);
            Debug.Log("Theta = " + theta + " (" + (2.0f * Mathf.PI * thetaScale) + ")");
            float x = circleRadius * Mathf.Cos(theta);
            float y = circleRadius * Mathf.Sin(theta);
            Debug.Log("x: " + x.ToString(".00") + ", y: " + y.ToString(".00"));
            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;

            // Draw circle around these positions
            pos = new Vector3(x, y, 0);
            positions.Add(pos);
            //Debug.Log(pos);
            lineRenderer.SetPosition(i, pos);
        }
    }

    public List<Vector3> GetPositions()
    {
        return positions;
    }

    public Vector3 GetPosition(int index)
    {
        return positions[index];
    }

}