using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridGenerator : MonoBehaviour
{
    public int segments = 10;
    public int cylinderLength = 5;
    public float cylinderRadius = 5f;
    public bool animateRender = true;
    public float waitTime = 0.01f;

    private Vector3[] vertices;
    private Mesh mesh;

    public Vector2[] UV_MaterialDisplay = new Vector2[]
{
         new Vector2(0,0),new Vector2(1,0),new Vector2(0,1),new Vector2(1,1) // 4 UV with all directions! (Plane has 4 uvMaps)
};

    private void Start()
    {
        StartCoroutine(Generate());
    }


    private IEnumerator Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(segments + 1) * (cylinderLength + 1)]; //5 * 22 = 110
        Vector2[] uv = new Vector2[vertices.Length];

        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        float angle = 20f;

        for (int i = 0, z = 0; z <= cylinderLength; z++)
        {
            for (int a = 0; a <= segments; a++, i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * cylinderRadius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * cylinderRadius;

                //Debug.Log(z + " " + (float)z / cylinderLength);
                vertices[i] =   new Vector3(x, y, z);
                Debug.Log(vertices[i]);
                DrawCubes(x, y, z);
                uv[i] =         new Vector2((float)a / segments, (float)z / cylinderLength);
                tangents[i] =   tangent;

                angle += (360f / segments);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv; //UV_MaterialDisplay;
        mesh.tangents = tangents;

        // TODO: There's a bug here that makes diamonds instead of rectangles (out of tris)
        // Create triangles. ti = triangle index, vi = vertex index
        int[] triangles = new int[segments * cylinderLength * 6];
        for (int ti = 0, vi = 0, y = 0; y < cylinderLength; y++, vi++)
        {
            for (int x = 0; x < segments; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + segments + 1;
                triangles[ti + 5] = vi + segments + 2;

                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                if(animateRender) yield return new WaitForSeconds(waitTime);
            }
        }
    }

    private void DrawCubes(float x, float y, int z)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(x, y, z);
        cube.transform.localScale *= 0.2f;
        cube.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

}
