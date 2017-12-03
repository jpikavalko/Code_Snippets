using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramBehaviour : MonoBehaviour {

 public float glitchChance = 0.1f;

    private Renderer hologramRenderer;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
    private WaitForSeconds glitchDuration = new WaitForSeconds(.1f);

    void Awake()
    {
        hologramRenderer = GetComponent<Renderer>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);
            if (glitchTest <= glitchChance)
            {
                StartCoroutine(Glitch());
            }
            yield return glitchLoopWait;
        }
    }

    IEnumerator Glitch()
    {
        glitchDuration = new WaitForSeconds(Random.Range(.05f, .25f));
        hologramRenderer.material.SetFloat("_Amount", 1f);
        hologramRenderer.material.SetFloat("_Speed", Random.Range(1, 10));
        yield return glitchDuration;
        hologramRenderer.material.SetFloat("_Amount", 0f);
    }
}
