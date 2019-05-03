using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGenerator : MonoBehaviour
{
    // Outter shell is at radius1 + radius2 / 2, inner shell at radius1 - radius2 / 2
    public float bottomRadius1 = .5f;
    public float bottomRadius2 = .15f;
    public float topRadius1 = .5f;
    public float topRadius2 = .15f;

    public float height = 1f;
    public int nbSides = 24;

    private MeshFilter filter;
    private Mesh mesh;

    private void Awake()
    {
        filter = gameObject.AddComponent<MeshFilter>();
    }

    private void Start()
    {
        GenerateCylinder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateCylinder();
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) GenerateCylinder();
    }

    public void GenerateCylinder()
    {
        mesh = filter.mesh;
        mesh.Clear();

        int nbVerticesCap = nbSides * 2 + 2;
        int nbVerticesSides = nbSides * 2 + 2;

        #region Vertices

        Vector3[] vertices = new Vector3[nbVerticesCap * 2 + nbVerticesSides * 2];

        int vert = 0;
        int sideCounter = 0;

        float _2pi = Mathf.PI * 2f;

        while (vert < nbVerticesCap * 2 + nbVerticesSides)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;
            float cos = Mathf.Cos(r1);
            float sin = Mathf.Sin(r1);
            float wildCard = Random.Range(0f, 0.1f);
            vertices[vert] = new Vector3(cos * (topRadius1 + topRadius2 * .5f + wildCard), height, sin * (topRadius1 + topRadius2 * .5f + wildCard));
            vertices[vert + 1] = new Vector3(cos * (bottomRadius1 + bottomRadius2 * .5f + wildCard), 0, sin * (bottomRadius1 + bottomRadius2 * .5f +wildCard));
            vert += 2;
        }
        #endregion

        #region Normales

        Vector3[] normales = new Vector3[vertices.Length];

        vert = 0;
        sideCounter = 0;

        while (vert < nbVerticesCap * 2 + nbVerticesSides)
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;

            float r1 = (float)(sideCounter++) / nbSides * _2pi;

            normales[vert] = new Vector3(Mathf.Cos(r1), 0f, Mathf.Sin(r1));
            normales[vert + 1] = normales[vert];
            vert += 2;
        }
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Length];

        vert = 0;
        sideCounter = 0;

        while (vert < nbVerticesCap * 2 + nbVerticesSides)
        {
            float t = (float)(sideCounter++) / nbSides;
            uvs[vert++] = new Vector2(t, 0f);
            uvs[vert++] = new Vector2(t, 1f);
        }
        #endregion

        #region Triangles
        int nbFace = nbSides * 4;
        int nbTriangles = nbFace * 2;
        int nbIndexes = nbTriangles * 3;
        int[] triangles = new int[nbIndexes];

        int i = 0;
        sideCounter = 0;

        while (sideCounter < nbSides * 3)
        {
            int current = sideCounter * 2 + 4;
            int next = sideCounter * 2 + 6;

            triangles[i++] = current;
            triangles[i++] = next;
            triangles[i++] = next + 1;

            triangles[i++] = current;
            triangles[i++] = next + 1;
            triangles[i++] = current + 1;

            sideCounter++;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }

    private void GetColor()
    {
        Debug.Log("d");
    }
}
