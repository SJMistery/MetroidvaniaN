using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InverseTime : MonoBehaviour
{


    public TextMeshProUGUI cooldnText;
    public GameObject clockBG;//el background de la imagen del clock
    public GameObject clockImage;
    public GameObject cooldown;

    public float targetFill = 0.0f;            //valores para hacer los calculos del cooldown.
    float _maxValue = 25.0f;            //valores para hacer los calculos del cooldown.
    private float _currentFraction = 1.0f;
    public bool respawnReset;
    Queue<Vector2> trackPos;
    private Transform shadowObj;
    [SerializeField] private GameObject playerP;
    public int count;
    public int frameRet = 180;
    public int frameCoold = 90;
    private Rigidbody2D playerBody;
    private CharacterController2D_Mod characterController;

    public GameObject inverseTimeMovement;

    public float tempPercent, tempor;

    public Canvas canvas;

    private void Awake()
    {
        characterController = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
        playerBody = GameObject.Find("SrBeta1").GetComponent<Rigidbody2D>();
        shadowObj = GetComponent<Transform>();
        cooldown = GameObject.FindGameObjectWithTag("ClockCD");
        clockBG = GameObject.Find("Cooldown_background"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        playerP = GameObject.FindGameObjectWithTag("Player");
        respawnReset = false;
        trackPos = new Queue<Vector2>();
        Vector3 temp = playerP.transform.position;
        temp.z = -1;
        shadowObj.position = temp;
        count = 0;
        cooldnText = GameObject.Find("cooldown").GetComponent<TextMeshProUGUI>();
        cooldnText.gameObject.SetActive(false);
        clockImage = GameObject.Find("clock");
        clockImage.SetActive(false);
        cooldown.gameObject.SetActive(false);
    }
    private void UpdateCDFill(float currentValue, float maxValue)
    {
        // Fix the value to be a percentage.
        _currentFraction = currentValue / maxValue;

        // If the value is greater than 1 or less than 0, then fix the values to being min/max.
        if (_currentFraction < 0 || _currentFraction > 1)
            _currentFraction = _currentFraction < 0 ? 0 : 1;

        // Store the target amount of fill according to the users options.
        targetFill = _currentFraction;

        // Store the values so that other functions used can reference the maxValue.
        _maxValue = maxValue;

        // Then just apply the target fill amount.
        cooldown.GetComponent<Image>().fillAmount = 1 - targetFill;
    } //funcion que controla como se rellena o se vacia la barra de vida.

    // Update is called once per frame
    void Update()
    {
        if (canvas != null)
            canvas.worldCamera = Camera.main;

        if (respawnReset)
        {
            count = 0;
            trackPos.Clear();
            respawnReset = false;
        }
        Vector3 tempInt = playerP.transform.position;
        trackPos.Enqueue(tempInt);
       
        if(Time.timeScale > 0)
        {
            if (count < frameRet && !GlobalController.Instance.moveIT)
            {
                count++;
            }
            else if ((count >= frameRet) && (count < frameCoold) && !GlobalController.Instance.moveIT)
            {

                count++;
                Vector3 temp = trackPos.Dequeue();
                temp.z = -1;
                shadowObj.position = temp;
            }
            else if ((count >= frameRet) && (count >= frameCoold) && !GlobalController.Instance.moveIT)
            {
                Vector3 temp = trackPos.Dequeue();
                temp.z = -1;
                shadowObj.position = temp;
            }
            if ((Input.GetKeyDown("r")) && (count >= frameCoold))
            {
                //cuando se ejecuta el recall la variable m_grounded se vuelve falsa para que el jugador pueda saltar inmediatamente en el aire, sino no deja saltar el juego.
                characterController.GetComponent<CharacterController2D_Mod>().m_Grounded = false;
                Vector3 temp = shadowObj.position;//obtain up to 5 intervals

                /* Vector3 temp1, temp2, temp3, temp4, temp5;
                for(int i = 0; i < 36; i++)
                {
                   temp2 = trackPos.Dequeue();
                    count--;
                }
                if (count > 36)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        temp3 = trackPos.Dequeue();
                        count--;
                    }
                }
                if (count > 36)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        temp4 = trackPos.Dequeue();
                        count--;
                    }
                }
                if (count > 36)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        temp5 = trackPos.Dequeue();
                        count--;
                    }
                }*/
                temp.z = -2;
                //playerP.transform.position = temp;
                inverseTimeMovement.GetComponent<InverseTimeMovement>().StartMovingIT();
                
                trackPos.Clear();
                // la variable can double jump se activa para que el jugador pueda aplicar el doble salto.
                characterController.GetComponent<CharacterController2D_Mod>().canDoubleJump = true;
                count = 0;
            }

            if (frameCoold - count > 0)
            {
                cooldnText.gameObject.SetActive(true);
                cooldown.SetActive(true);
            }
            else
            {
                cooldnText.gameObject.SetActive(false);
                cooldown.SetActive(false);
            }
            if (frameCoold - count <= 0)
                clockImage.SetActive(true);
            else
                clockImage.SetActive(false);

            /* tempor = count / frameCoold;

             if (tempor < 0 || tempor > 1)
                 tempor = tempor < 0 ? 0 : 1;

             tempPercent = 1 - tempor;

             cooldnText.text = tempPercent.ToString();*/

            UpdateCDText(count, frameCoold);
            UpdateCDFill(count, frameCoold);
        }
    }

    private void UpdateCDText(float currentValue, float maxValue)
    {
        // Fix the value to be a percentage.
        _currentFraction = currentValue / maxValue;

        // If the value is greater than 1 or less than 0, then fix the values to being min/max.
        if (_currentFraction < 0 || _currentFraction > 1)
            _currentFraction = _currentFraction < 0 ? 0 : 1;

        // Store the target amount of fill according to the users options.
        targetFill = _currentFraction;
        int fill = Mathf.RoundToInt(targetFill * 100);

        // Store the values so that other functions used can reference the maxValue.
        _maxValue = maxValue;

        // Then just apply the target fill amount.
        cooldnText.text = fill.ToString();
    } //funcion que controla como se rellena o se vacia la barra de vida.
}
