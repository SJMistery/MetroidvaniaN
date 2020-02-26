using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIntroductionScene : MonoBehaviour
{

    public float alpha;

    public void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
            transform.position = new Vector3(transform.position.x + Vector3.left.x * alpha, transform.position.y);
    }
}
