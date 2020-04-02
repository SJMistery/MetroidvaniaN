using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TextManager : MonoBehaviour
{

    public string Text = "Desde tiempos inmemoriales ha existido una leyenda entre la gente de cierto país, dicho rumor habla sobre cierto lugar inhóspito lleno de monstruos que no deberían existir, un lugar donde ni tiempo ni lugar importaban, dicho lugar era llamado “El abismo” que existe debajo de un castillo...";
    public GameObject autoTyping;
    public GameObject button;
    public GameObject canvas;
    public GameObject noticeBox;
    public GameObject noticeText;
    public int numOfText = 1;
    public bool acabado = false;
    public bool start = false;
    public bool skipScene = false;

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

    public void SkipScene()
    {

        autoTyping.GetComponent<Autotyping_Text>().StopAllCoroutines();
        autoTyping.GetComponent<Autotyping_Text>().parar = true;
        GlobalController.Instance.maxHp = 5;
        GlobalController.Instance.hp = 5;
        GlobalController.Instance.cooldown = 100;
        GlobalController.Instance.maxpotions = 3;
        GlobalController.Instance.disp_potions = 3;
        GlobalController.Instance.fromBeginning = true;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionOutside;
        GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.Afueras de la Torre"));
        
    }
    public void ChangeText()
    {
        ++numOfText;
        autoTyping.GetComponent<TextMeshProUGUI>().text = "";
        if (numOfText == 2)
        {
            Text = "The legend says that if one reaches the deepest part of this place a wish will be granted, however no one has ever been able to prove the truth of this legend or the existence of the place itself however everyone knows about its existence...";
        }
        if (numOfText == 3)
        {
            Text = "That same place is where the protagonist of this story is directed, who, moved to achieve his most desired desire, is preparing to jump into the most unknown place in his world where everyone can enter, but no one has left. Only fate will tell if it is able to survive this adventure";
        }

        if (numOfText == 4)
        {
            LoadScenes.Instance.LoadLevel1();
        }

        autoTyping.GetComponent<Autotyping_Text>().chain = Text;
        button.SetActive(false);
        StartText();
    }

    public void SkipText()
    {
        autoTyping.GetComponent<Autotyping_Text>().StopAllCoroutines();
        GlobalController.Instance.stopAll = false;
        canvas.SetActive(false);
        button.SetActive(false);
    }

    public void ChangeTextFirstCutscene()
    {
        ++numOfText;
        autoTyping.GetComponent<TextMeshProUGUI>().text = "";
        if (numOfText == 2)
        {
            Text = "I hope the information is right, I guess I'll give it a try.";
        }
        if (numOfText == 3)
        {
            Text = "If it's right... it'll only be a matter of time.";
        }

        if (numOfText == 4)
        {
            autoTyping.GetComponent<Autotyping_Text>().StopAllCoroutines();
            GlobalController.Instance.stopAll = false;
            canvas.SetActive(false);
            button.SetActive(false);
        }
        else
        {

            autoTyping.GetComponent<Autotyping_Text>().chain = Text;
            button.SetActive(false);
            StartText();
        }
    }

    public void ChangeTextStorageScene()
    {
        ++numOfText;
        autoTyping.GetComponent<TextMeshProUGUI>().text = "";
        if (numOfText == 2)
        {
            noticeBox.SetActive(true);
            noticeText.GetComponent<TextMeshProUGUI>().text = "You've obtained:" + "'Ancient Journal'";
            //Text = "";
        }
        if (numOfText == 3)
        {
            Text = "This looks like a journal, but it's very bruised. Maybe is from someone who got here before me?";
        }

        if (numOfText == 4)
        {
            autoTyping.GetComponent<Autotyping_Text>().StopAllCoroutines();
            GlobalController.Instance.stopAll = false;
            canvas.SetActive(false);
            button.SetActive(false);
            Cursor.visible = false;
        }
        else
        {

            autoTyping.GetComponent<Autotyping_Text>().chain = Text;
            button.SetActive(false);
            StartText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas!= null)
        {
            if (canvas.activeSelf)
                GlobalController.Instance.stopAll = true;
        }

        acabado = autoTyping.GetComponent<Autotyping_Text>().acabado;

        if (autoTyping.GetComponent<Autotyping_Text>().acabado)
        {
            button.SetActive(true);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.I))
        {
            
            if (!skipScene)
            {
                SkipScene();
                skipScene = true;
            }
            //button.SetActive(false);
        }
    }
}
