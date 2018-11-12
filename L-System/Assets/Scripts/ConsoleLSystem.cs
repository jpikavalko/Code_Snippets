using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// axiom = A
/// rules(A -> AB), (B -> A)
/// </summary>
public class ConsoleLSystem : MonoBehaviour {

    protected string axiom = "A";
    protected string sentence;

    Dictionary<char, string> Rule = new Dictionary<char, string>();

    public virtual void Start()
    {
        sentence = axiom;
        Rule.Add('A', "AB");
        Rule.Add('B', "A");
        Generate();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        }
    }

    public virtual void Generate()
    {
        string nextSentence = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            string current = sentence[i].ToString();
            if (current == "A")
                nextSentence += Rule['A'];
            else if (current == "B")
                nextSentence += Rule['B'];
            else
                nextSentence += current;
        }

        sentence = nextSentence;
        Debug.Log(sentence);
    }
}
