using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveStop : MonoBehaviour
{
    bool left;
    bool stopT;
    Vector3 startPos;
    float x, y, z;
    int changeD;
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        stopT = false;
        left = true;
        startPos = transform.position;
        x = 0.2f;
        y = 0;
        changeD = 0;
        z = 0;
       
        movement = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        changeD++;
        if (changeD > 240)
        {
            left = !left;
            changeD = 0;
        }
        if (Input.GetKey("z"))
        {
            stopT = true;
        }
        else stopT = false;
        if (stopT == false)
        {
           if (left==true) transform.position = transform.position + movement;
           else transform.position = transform.position - movement;
        }
    }
}
