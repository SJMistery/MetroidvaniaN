using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseTime : MonoBehaviour
{
    public List<GameObject> player;
    public Queue<Vector3> trackPos;
    public Transform shadowObj;
    private GameObject shadow;
    private Vector3 temp;
    public GameObject playerP;
    Vector3 startPos;
    float x, y, z;
    Vector3 movement;
    private int count;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        trackPos = new Queue<Vector3>();
        shadowObj.position = playerP.transform.position;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        temp = playerP.transform.position;
        trackPos.Enqueue(temp);
        //Debug.Log(trackPos.Count);
        if (count < 180)
        {
            count++;
        }
        else
        {
            shadowObj.position = trackPos.Dequeue();
        }
        if (Input.GetKeyDown("r"))
        {
            playerP.transform.position = shadowObj.position;
            trackPos.Clear();
            count = 0;
        }

    }
}
