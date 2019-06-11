using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{

    public GameObject markerCube;
    public int[] triPos;
    private Mesh mesh;
    private Vector3[] verts; // = new Vector3[4];
    private int[] tris; // = new int[6];
    private Vector2[] uv;
    void Start()
    {
        verts = new Vector3[4];
        tris = new int[6];
        uv = new Vector2[verts.Length];

        

        Generate();
    }


    public void Generate()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        

        verts[0] = new Vector3(0f, 0f, 0f); // z1f
        verts[1] = new Vector3(1f, 0f, 0f);
        verts[2] = new Vector3(0f, 2f, 0f); // HUOM 2!
        verts[3] = new Vector3(1f, 1f, 0f); // z1f

        uv[0] = new Vector2(0f, 0f);
        uv[1] = new Vector2(1f, 0f);
        uv[2] = new Vector2(0f, 2f); //HUOM 2!
        uv[3] = new Vector2(1f, 1f);

        mesh.vertices = verts;
        mesh.uv = uv;
        Debug.Log(mesh.vertices[3]);

        tris[0] = triPos[0];
        tris[1] = triPos[1];
        tris[2] = triPos[2];

        tris[3] = triPos[3];
        tris[4] = triPos[4];
        tris[5] = triPos[5];
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        DrawCubes();

    }

    private void DrawCubes()
    {
        for (int i = 0; i < verts.Length; i++)
        {
            GameObject cube = Instantiate(markerCube, verts[i], Quaternion.identity);
            cube.transform.GetChild(0).GetComponent<TextMesh>().text = i + "\n(" + verts[i].x + "," + verts[i].y + "," + verts[i].z + ")";
        }
    }
}
