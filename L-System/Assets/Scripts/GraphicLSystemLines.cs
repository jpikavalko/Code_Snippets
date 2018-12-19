using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//F -> FF++F-F-F]-[-F+F+F]
public class GraphicLSystemLines : ConsoleLSystem
{
    List<GameObject> joints = new List<GameObject>();
    public GameObject treeJointPrefab;
    Vector3 posA;
    Vector3 posB;
    int iterations = 0;
    bool a = false;
    bool b = false;

    public override void Start()
    {
        posA = new Vector3(1f, 1f, 0f);
        posB = new Vector3(1f, 1f, 0f);
        base.Start();
    }

    public override void Generate()
    {
        base.Generate();
        DrawLines(sentence);
    }

    void DrawLines(string sentence)
    {
        joints = new List<GameObject>();
        DeleteList();
        iterations++;
        //posA.y = iterations;
        //posB.y = iterations;
        
        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] == 'A')
            {
                joints.Add(Instantiate(treeJointPrefab, posA, Quaternion.identity));
                LineRenderer rend = joints[i].GetComponent<LineRenderer>();
                rend.SetPosition(0, posA);
                posA.y++;
                rend.SetPosition(1, posA);
            }
            else if (sentence[i] == 'B')
            {
                joints.Add(Instantiate(treeJointPrefab, posB, Quaternion.identity));
                LineRenderer rend = joints[i].GetComponent<LineRenderer>();
                rend.SetPosition(0, posB);
                posB.x++;
                posB.y++;
                rend.SetPosition(1, posB);
            }
        }
        //posA.y++;
        //posB.y++;
        //posB.x++;
    }

    void DeleteList()
    {
        for (int i = 0; i < joints.Count; i++)
        {
            Destroy(joints[i]);
        }
        joints.Clear();
    }
}
