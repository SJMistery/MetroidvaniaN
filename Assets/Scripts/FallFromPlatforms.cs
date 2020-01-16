using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFromPlatforms : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    public float recoverTime;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        recoverTime = 0.5f;
    }


    void Update()
    {
        if (Input.GetButtonUp("Down"))
        {
            waitTime = 0f;
        }

        if (Input.GetButton("Down"))
        {
            recoverTime = 0f;
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.3f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }



        if (recoverTime >= 0.2f)
        {
            effector.rotationalOffset = 0f;
            recoverTime = 0.2f;
        }
        else
        {
            recoverTime += Time.deltaTime;
        }


        //Programar que después de pulsar el boton hacia abajo el PJ no haga colision con la plataforma que ha pasado hasta que no la haya sobrepasado completamente.


    }
}
