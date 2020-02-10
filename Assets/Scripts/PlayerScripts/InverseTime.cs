using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InverseTime : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI cooldnText;
    [SerializeField] private GameObject clockImage;


    public bool respawnReset;
    Queue<Vector2> trackPos;
    public Transform shadowObj;
    public GameObject playerP;
    public int count;
    public int frameRet = 180;
    public int frameCoold = 100;
    public Rigidbody2D playerBody;
    public int saveForce;
    private CharacterController2D_Mod characterController;



    private void Awake()
    {
        characterController = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
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
            if (count < frameRet)
            {
                count++;
            }
            else
            {
                Vector3 temp = trackPos.Dequeue();
                temp.z = -1;
                shadowObj.position = temp;
            }
        }
        else if ((count >= frameRet) && (count < frameCoold))
        {

            count++;
            Vector3 temp = trackPos.Dequeue();
            temp.z = -1;
            shadowObj.position = temp;
        }
        else if ((count >= frameRet) && (count >= frameCoold))
        {
            Vector3 temp = trackPos.Dequeue();
            temp.z = -1;
            shadowObj.position = temp;
        }
        if ((Input.GetKeyDown("r")) && (count >= frameCoold))
        {
            Debug.Log("recalling");
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
            playerP.transform.position = temp;
            trackPos.Clear();
            // la variable can double jump se activa para que el jugador pueda aplicar el doble salto.
            characterController.GetComponent<CharacterController2D_Mod>().canDoubleJump = true;
            count = 0;
        }



        if (frameCoold - count > 0)
            cooldnText.gameObject.SetActive(true);
        else
            cooldnText.gameObject.SetActive(false);
        if (frameCoold - count <= 0)
            clockImage.SetActive(true);
        else
            clockImage.SetActive(false);
        cooldnText.text = count.ToString() + '%';
    }
}
