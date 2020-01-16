using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed;
    public float maxdistance;
    public GameObject player;
    Vector3 newPosArriba;
    Vector3 newPosAbajo;
    // Update is called once per frame
    void Update()
    {
        //al pulsar el las flechas abajo y arriba la camara del PJ se desplaza hacia dicha dirección.
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

        //hacer que cuando la camara llegue a la distancia máxima se quede ahí y no se mueva.


        //hacer que cuando se suelta el tecla Up Arrow/DownArrow la camara vuelva a la poscion 0,0,0.
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y, -300), Quaternion.identity);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.SetPositionAndRotation(new Vector3 (player.transform.position.x, player.transform.position.y, -300), Quaternion.identity);
        }

    }
}
