using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Autotyping_Text : MonoBehaviour
{
    public TextMeshProUGUI Interface; //We are using Unity UI System for write our text.
    public string chain; //Text to show.
    public float velocity = 0.02f; //Velocity between each letter.
    public bool acabado = false;
    public bool parar = false;

    void Start()
    {
        StartCoroutine(WriteChain(chain));
    }

    public void StartRoutine()
    {
        StartCoroutine(WriteChain(chain));
    }

    void Update()
    {
        if (chain.Length > 0 && !parar)
            acabado = false;
        else
            acabado = true;
    }

    public IEnumerator WriteChain(string chain)
    {
        Interface.text = chain;
        Interface.maxVisibleCharacters = 0;
        while (chain.Length > 0 && !parar)
        {
            Interface.maxVisibleCharacters += 1; //Write first letter in chain.
            chain = chain.Substring(1, chain.Length - 1); //We take the rest in chain, less the first letter.
            yield return new WaitForSeconds(velocity);
        }
        acabado = true;
    }
}