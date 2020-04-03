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
    public bool start = false;
    public int lenght = 0;

    void Start()
    {
        if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE && !GlobalController.Instance.firstCutsceneEnded)
        {
            start = true;
        }
        else if(GlobalController.Instance.actualLevel == GlobalController.Level.INTRO)
            start = true;

        if (start == true)
            StartCoroutine(WriteChain(chain));
    }

    public void StartRoutine()
    {
        StartCoroutine(WriteChain(chain));
    }

    void Update()
    {
        lenght = chain.Length;

        if (Interface.maxVisibleCharacters < chain.Length && parar == false)
        {
            GlobalController.Instance.cutsceneActive = true;
            acabado = false;
        }
        else
            acabado = true;
    }

    public IEnumerator WriteChain(string chain)
    {
        Interface.text = chain;
        Interface.maxVisibleCharacters = 0;
        while (Interface.maxVisibleCharacters <= chain.Length && parar == false)
        {
            Interface.maxVisibleCharacters += 1; //Write first letter in chain.
            //chain = chain.Substring(1, chain.Length - 1); //We take the rest in chain, less the first letter.
            yield return new WaitForSeconds(velocity);
        }
        acabado = true;
    }
}