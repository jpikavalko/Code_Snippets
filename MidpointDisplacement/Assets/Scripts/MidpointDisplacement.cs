using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidpointDisplacement : MonoBehaviour
{

    public GameObject pointPrefab;
    public Transform startPoint, endPoint;

    //public int iterations;

    MDSettings settings;

    LineRenderer rend;
    List<Vector3> points = new List<Vector3>();
    

    private void Start()
    {
        rend = GetComponent<LineRenderer>();
        settings = GetComponent<MDSettings>();

        Generate(1);
        
    }

    public void Generate(int length)
    {
        points = new List<Vector3>
        {
            startPoint.position,
            endPoint.position
        };

        GeneratePoints(settings.iterations);
    }

    public void GeneratePoints(int iterations)
    {
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            List<Vector3> temp = new List<Vector3>();
            int itemCount = points.Count - 1;

            for (int i = 0, step = 1; i < itemCount; i++, step += 2)
            {
                Vector3 newPoint = CalculateMidPoint(points[step-1], points[step]);
                points.Insert(step, newPoint);

                if (settings.usePointPrefabs) Instantiate(pointPrefab, newPoint, Quaternion.identity);
            }
        }

        rend.positionCount = points.Count;
        rend.SetPositions(points.ToArray());
    }

    private Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
    {
        Vector3 newPoint = (start + end) / 2;
        float newPointY = Random.Range(settings.displacementValueLow, settings.displacementValueHigh);
        newPoint.y += newPointY;

        return newPoint;
    }
}
