using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  SCRIPT QUE PERMITE AL JUGADOR MANIPULAR LA CANTIDAD DE SALTO DEL PERSONAJE EN FUNCION DE 
    LA CANTIDAD DE TIEMPO QUE MANITIENE PULSADO EL BOTON DE SALTO. CON SIMPLEMENTE PULSAR EL 
    BOTON Y SALTAR EL JUGADOR OBTENDRA UN SALTO DE BAJA ALTURA, MIENTRAS QUE SI MANTIENE 
    PULSADO EL BOTON DE SALTO DURANTE UN BREVE PERIODO DE TIEMPO, EL JUGADOR PODRA SALTAR MAS ALTO.*/
public class BetterJumpControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private float fallMultiplier = 4f;
    private float lowJump = 7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //El jugador puede mantener pulsado el boton de salto, para alargar un poco más el salto. 
    // Además se puede variar la velocidad de la caida en funcion de si el jugador tiene pulsado el boton de salto o de si lo ha soltado antes de llegar a arriba del todo. 
    private void Update()
    {
        if ( rb.velocity.y < 0) //si el pj empieza a bajar se aplica el multiplicador de caida para que baje mas rapido que cuando sube.
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;  //se resta 1 al fallMultiplier por que el propio unity le suma 1 a las variables de gravedad.
        }
        else if ( rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime;
        }
    }




}
