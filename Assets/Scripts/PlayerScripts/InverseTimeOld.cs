using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InverseTimeOld : MonoBehaviour
{

    
    [SerializeField] private TextMeshProUGUI cooldnText;
    [SerializeField] private GameObject clockImage;

    private CharacterController2D_Mod secondJump;

    public bool respawnReset;
    Queue<Vector2> trackPos;
    public Transform shadowObj;
    public GameObject playerP;
    private int count;
    public int frameRet = 180;
    public int frameCoold = 90;
    public Rigidbody2D playerBody;
    public int saveForce;


    private void Awake()
    {
        secondJump = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();

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
        if ((Input.GetKeyDown("r"))&&(count>frameCoold))
        {
            Debug.Log("recalling");
           Vector3 temp = shadowObj.position;//obtain up to 5 intervals

            temp.z = -2;
            playerP.transform.position = temp;
            trackPos.Clear();
            if (Input.GetButton("Jump")){
                playerBody.AddForce(new Vector2(0f, saveForce));
            }
            count = 0;
        }


        if (frameCoold-count > 0)
            cooldnText.gameObject.SetActive(true);
        else
            cooldnText.gameObject.SetActive(false);
        if (frameCoold-count <= 0)
            clockImage.SetActive(true);
        else
            clockImage.SetActive(false);
        cooldnText.text = count.ToString();
    }
}
