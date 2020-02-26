using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{

    public string Text = "Desde tiempos inmemoriales ha existido una leyenda entre la gente de cierto país, dicho rumor habla sobre cierto lugar inhóspito lleno de monstruos que no deberían existir, un lugar donde ni tiempo ni lugar importaban, dicho lugar era llamado “El abismo” que existe debajo de un castillo...";
    public GameObject autoTyping;
    public GameObject button;
    public int numOfText = 1;
    public bool acabado = false;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);/*
        autoTyping.GetComponent<Autotyping_Text>().chain = Text;
        autoTyping.GetComponent<Autotyping_Text>().StartRoutine();*/
        //StartText();
    }

    public void StartText()
    {
        autoTyping.GetComponent<Autotyping_Text>().chain = Text;
        autoTyping.GetComponent<Autotyping_Text>().StartRoutine();

    }

    public void ChangeText()
    {
        ++numOfText;
        autoTyping.GetComponent<TextMeshProUGUI>().text = "";
        if (numOfText == 2)
        {
            Text = "La leyenda dice que si uno llega a la parte mas profunda de este lugar se le concedera un deseo, sin embargo nadie nunca ha podido demostrar la veracidad de esta leyenda ni la existencia del lugar mismo sin embargo todo el mundo sabe de su existencia...";
        }
        if (numOfText == 3)
        {
            Text = "Ese mismo lugar es al que se dirige el protagonista de esta historia, quien, movido por conseguir su deseo mas ansiado, se dispone a lanzarse al lugar mas desconocido de su mundo donde todos pueden entrar, pero nadie ha salido. Solo el destino dira si es capaz de sobrevivir a esta aventura";
        }

        if (numOfText == 4)
        {
            LoadScenes.Instance.LoadLevel1();
        }

        autoTyping.GetComponent<Autotyping_Text>().chain = Text;
        button.SetActive(false);
        StartText();
    }

    // Update is called once per frame
    void Update()
    {

        acabado = autoTyping.GetComponent<Autotyping_Text>().acabado;
        if (autoTyping.GetComponent<Autotyping_Text>().acabado)
        {
            button.SetActive(true);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.P))
        {
            LoadScenes.Instance.LoadLevel1();
        }
    }
}
