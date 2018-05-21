using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oculars : MonoBehaviour {

    private GameObject leftLens, rightLens;
    private bool leftTargetCorrect, rightTargetCorrect;
    public List<Material> materials = new List<Material>();
    public List<Material> lensMaterials = new List<Material>();
    public GameObject[] cubes;
    public int[] color;
    public Text leftText, rightText, niceJob;
    //TRANSFORMILLA OFFSETIT. TEE OFFSET STARTISSA! JEAA!
	void Start ()
    {
        cubes = GameObject.FindGameObjectsWithTag("Target");
        color = new int[cubes.Length];
        for (int i = 0; i < color.Length; i++)
        {
            color[i] = Random.Range(0, color.Length);
        }

        
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<Renderer>().material = materials[color[i]];
        }
        leftLens = transform.GetChild(0).gameObject;
        rightLens = transform.GetChild(1).gameObject;
        leftLens.GetComponent<Renderer>().material = lensMaterials[color[0]];
        rightLens.GetComponent<Renderer>().material = lensMaterials[color[1]];
    }
	

	void Update () {

        RaycastHit leftHit, rightHit;

        if (Physics.Raycast(new Vector3(-0.5f, 1f, -1f), transform.rotation * Vector3.forward * 10f, out leftHit))
        {
            leftText.text = leftHit.collider.GetComponent<Renderer>().material.name.ToString();
            if (leftHit.collider.GetComponent<Renderer>().material.name == leftLens.GetComponent<Renderer>().material.name)
            {
                leftText.text = "CORRECT";
                leftTargetCorrect = true;
            }
            else
                leftTargetCorrect = false;
        }
        else
            leftTargetCorrect = false;
        if (Physics.Raycast(new Vector3(0.5f, 1f, -1f), transform.rotation * Vector3.forward * 10f, out rightHit))
        {
            rightText.text = rightHit.collider.GetComponent<Renderer>().material.name.ToString();
            if (rightHit.collider.GetComponent<Renderer>().material.name == rightLens.GetComponent<Renderer>().material.name)
            {
                rightText.text = "CORRECT";
                rightTargetCorrect = true;
            }
            else
                rightTargetCorrect = false;
        }
        else
            rightTargetCorrect = false;

        if (leftTargetCorrect && rightTargetCorrect)
        {
            niceJob.gameObject.SetActive(true);
            Debug.Log("VOITTO!");
        }
        
        Debug.DrawRay(transform.localPosition, transform.rotation * Vector3.forward * 10f, Color.red);
        Debug.DrawRay(transform.localPosition + new Vector3(0.5f, 1f, -1f), transform.rotation * Vector3.forward * 10f, Color.red);
    }

    void DrawRays()
    {

    }
}
