using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    public Rigidbody rb;

    public static List<Attractor> Attractors;

    public bool pull;

    float factor = 1;

    //Gravitational constant
    const float G = 6.67408f; //6.67408 × 10^-11 m^3 kg^-1 s^-2

    private void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    private void OnEnable()
    {
        if(Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0f)
        {
            return;
        }

        if (pull){
            factor = 1;
        } 
        else{
            factor = -1;
        }

        //Here's the physics part F = G * (m1*m2 / r^2)
        float forceMagnitude = factor * G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);

        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
