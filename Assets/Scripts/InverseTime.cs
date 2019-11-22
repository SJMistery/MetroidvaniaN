using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseTime : MonoBehaviour
{
    Queue<Vector2> trackPos;
    public Transform shadowObj;
    public GameObject playerP;
    private int count;
    public int frameRet = 180;
    public int frameCoold = 90;
    public Rigidbody2D playerBody;
    //bool jumpSave;
    public int saveForce;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        trackPos = new Queue<Vector2>();
        Vector3 temp = playerP.transform.position;
        temp.z = -1;
        shadowObj.position = temp;
        count = 0;
       // jumpSave = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp2 = playerP.transform.position;
        trackPos.Enqueue(temp2);
        Debug.Log(trackPos.Count);
        Debug.Log(trackPos.Peek());
        if (count < frameRet)
        {
            count++;
        }
        else
        {
            Vector3 temp = trackPos.Dequeue();
            temp.z = -1;
            shadowObj.position = temp;
            Debug.Log(shadowObj.position);
        }
        if ((Input.GetKeyDown("r"))&&(count>frameCoold))
        {
            Vector3 temp = shadowObj.position;
            temp.z = 0;
            playerP.transform.position = temp;
            trackPos.Clear();
            if (Input.GetButton("Jump")){
                playerBody.AddForce(new Vector2(0f, saveForce));
            }
            count = 0;
            //jumpSave = true;
        }/*
        if (((Input.GetKeyDown("w")) && (jumpSave == true)) || ((Input.GetKeyDown("w")) && (jumpSave == true)))
        {

            playerBody.AddForce(new Vector2(0f, saveForce));
            jumpSave = false;
        }
        if ((count > 150) && (jumpSave == true))
        {
            jumpSave = false;
        }*/

    }
}
