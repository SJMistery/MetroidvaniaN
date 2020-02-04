using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class CharacterController2D_Mod : MonoBehaviour
{

    private Animator anim;
    private enum State { idle, running, jumping, falling, dead, resting, attacking1, attacking2, attacking3 }
    private State state = State.idle;

    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsEnemies;
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_AttackPoint;

    public float attackRange = 0.4f;
    const float k_GroundedRadius = 0.07f; // Radius of the overlap circle to determine if grounded
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    public GameObject JumpSound; //gameobject que contiene el sonido cuando el Pj salta y otra funcion que lo destruye poco después.
    private Image barImage; //imagen para la barra de vida
    private Color barColor = Color.red; //color de la barra de vida
    private Gradient barGradient = new Gradient();
    private float _currentFraction = 1.0f;
    private TextMeshProUGUI barText; //texto para la barra de vida
    private Vector3 RespawnPoint;
    [SerializeField] private GameObject shadowReset;


    private int fullHP = 0; //barra del PJ
    public int LifeBar;             //Vida del PJ.
    public int hpRecovered = 1;     //Cantidad de vida que recupera cada cura.
    public int maxHeals = 3;        //numero máximo de curas.
    private int healsAvalible;      //curas disponibles.
    public int attackDMG = 1;       //daño de ataque.
    private int noAttack = 0;       //nummero del ataque para el control de que animación de ataque va después
    private float hurtforce = 15;        //potencia del knockback que recibe el PJ.
    float targetFill = 0.0f; //valores para hacer los calculos de la barra de vida.
    float _maxValue = 10.0f; //valores para hacer los calculos de la barra de vida.
    private float h_AirResist = 10f; // variable que controla la resistencia del aire en el eje horizontal.
    public float invencibleTime = 1.5f;


    //BOOLEANS
    private bool isDamaged = false; //sirve para que el PJ no pierda más de 1 vida cuando entra en contacto con el enemigo.
    private bool isDead = false; //sirve para detener el movimiento del PJ cuando muere.
    public bool isResting = false;
    private bool attackPressed = false; //sirve para que la animacion de ataque solo se ejecute 1 sola vez.
    public bool respawnReset;
    public bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool m_AirControl = true;   // Whether or not a player can steer while jumping
    public bool canDoubleJump = false;

    //TIMERS
    private float maxComboDelay = 0f; //Time when last button was clicked.  Delay between attacks for which clicks will be considered as combo.
    private float nextAttackTimer;          //Timer que controla el tiempo hay entre ataque y ataque. Para que no se pueda "spamear" el boton de ataque. Evita problemas, especialemnte con las animaciones.
    private float knockbackTimer = 3f;
    private float deadTimer = 3f;
    private float lastTimeDamaged;
    public float waitCollisionTime;


    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        respawnReset = false;
        RespawnPoint = transform.position;
        fullHP = LifeBar;
        healsAvalible = maxHeals;

        barImage = GameObject.Find("Green_Bar").GetComponent<Image>();
        barText = GameObject.Find("Life_Bar_Text").GetComponent<TextMeshProUGUI>();

    }
    public void Move(float move, bool crouch, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            OnLandEvent.Invoke();
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            Instantiate(JumpSound);
        }
        if (m_Grounded)
        {
            //si el jugador esta en contacto con el suelo, la varible can double jump, se vuelve false, por que sino el PJ salta el doble de alto cuando esta en contacto con  el suelo después de haber hecho el recall.
            canDoubleJump = false;
        }

    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    // NEW O MOVIDO TODO LO QUE HAY A PARTIR DE AQUI.
    public float GetCurrentFraction //funcion para hacer los calculos de la ui de la barra de vida.
    {
        get
        {
            return _currentFraction;
        }
    }
    private void UpdateBarFill(float currentValue, float maxValue)
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
        barImage.fillAmount = targetFill;
    } //funcion que controla como se rellena o se vacia la barra de vida.
    private void UpdateBarText(float currentValue, float maxValue)
    {
        barText.text = currentValue + "/" + maxValue;
    } //MOVIDOO

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !isDamaged)
        {
            LifeBar -= 1;


            if (LifeBar <= 0)
            {
                //state = State.dead;
                Debug.Log("killed");
                state = State.dead;
                isDead = true;
                GetComponent<PlayerMovement>().enabled = false;
                m_Rigidbody2D.velocity = new Vector2(0, 0);
            }
            else
            {
                anim.SetTrigger("isHurt");
                Debug.Log("damaged");
                //state = State.hurt;
                if (collision.gameObject.transform.position.x <= transform.position.x)
                {
                    // enemy is at right so PJ should move to left
                    m_Rigidbody2D.velocity = new Vector2(hurtforce, hurtforce);
                    GetComponent<PlayerMovement>().enabled = false;
                    knockbackTimer = 0.25f;
                }
                else
                {
                    m_Rigidbody2D.velocity = new Vector2(-hurtforce, hurtforce);
                    GetComponent<PlayerMovement>().enabled = false;
                    knockbackTimer = 0.25f;
                }
            }
            isDamaged = true;
            lastTimeDamaged = invencibleTime;
        }
        else if (collision.collider.tag == "DeathTrap")
        {
            isDead = true;
            LifeBar = 0;
            state = State.dead;
            GetComponent<PlayerMovement>().enabled = false;
            m_Rigidbody2D.velocity = new Vector2(0, 0);
        }
    } //CAMBIADO A ON COLLISION ENTRE Y ON COLLISION EXIT. ASI NO HACE FALTA QUE TENGAMOS MÁS DE UN BOXCOLLIDER, PARA QUE SE ACTIVEN LOS TRIGGERS I LAS COLISIONES HAGAN EFECTO.

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "SavePoint" && Input.GetButton("Interact"))
        {
            Debug.Log("saved");
            isResting = true;
            RespawnPoint = other.gameObject.transform.position;
            LifeBar = fullHP;
            state = State.resting;
            healsAvalible = maxHeals;

        }
    } //NEW Y MOVIDO

    private void AnimationState() //NEEW Permite controlar los estados y logica de las animaciones
    {

        if (state == State.resting)
        {
            GetComponent<PlayerMovement>().enabled = false;
            m_Rigidbody2D.velocity = new Vector2(0, 0);
            isResting = true;
            if (Input.GetButton("Cancel"))
            {
                GetComponent<PlayerMovement>().enabled = true;
                isResting = false;
                state = State.idle;

            }
        }
        else if (state == State.dead)
        {
            healsAvalible = 0;
            LifeBar = 0;
            if (deadTimer <= 0) //Contador de 3 segundos que controla que el PJ no haga respawn hasta que se haya finalizado este tiempo.
            {
                deadTimer = 3f;
                transform.position = RespawnPoint;
                shadowReset.transform.position = RespawnPoint;
                respawnReset = true;
                LifeBar = fullHP;
                GetComponent<PlayerMovement>().enabled = true;
                state = State.resting;
                healsAvalible = maxHeals;
            }
        }
        else if (Input.GetButton("Attack") && !attackPressed && nextAttackTimer <= 0)
        {
            maxComboDelay = 0.75f;
            nextAttackTimer = 0.4f;

            //Record time of last button click
            noAttack++;
            if (noAttack == 1)
            {
                state = State.attacking1;
            }
            else if (noAttack == 2)
            {
                state = State.attacking2;
            }
            else if (noAttack == 3)
            {
                state = State.attacking3;
                noAttack = 0;
            }

            attackPressed = true;

        }
        else if (state == State.jumping)
        {
            //saltando
            if (m_Rigidbody2D.velocity.y < 5)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            //cayendo
            if (m_Grounded) //cuando toca el suelo
            {
                state = State.idle;
            }
        }
        //no puedo poner el numero 0, por que por las fuerzas a las que esta sometido al Pj siempre tiene el parametro velocity.x superior a 0
        else if (m_Grounded && Mathf.Abs(m_Rigidbody2D.velocity.x) > 0.1f)
        {
            //Moving
            state = State.running;
            if (!m_Grounded && m_Rigidbody2D.velocity.y < 0)
            {
                state = State.falling;
            }
        }

        else
        {
            //Standing
            state = State.idle;
            if (!m_Grounded && m_Rigidbody2D.velocity.y < 0)
            {
                state = State.falling;
            }
        }
    }


    private void Heal()
    {
        if (healsAvalible > 0)
        {
            LifeBar += hpRecovered; ;
            if (LifeBar >= fullHP)
            {
                LifeBar = fullHP;
            }
        }
        else
        {
            healsAvalible = 0;
        }
        healsAvalible -= 1;

    } //NEEEW

    private void Attack()
    {
        //The attack animation runs from AnimationState()
        //Esperar un breve periodo de tiempo antes de que salte el codigo, para que la animacion y la deteccion sean mas precisas.
        StartCoroutine(ExecuteAfterTime(waitCollisionTime));
    } //funcion que combinada con la corutine Execute after time, permite ejecutar el Ataque.

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay

        //Detect the enemies in range onf the weapon
        Collider2D[] damageEnemies = Physics2D.OverlapCircleAll(m_AttackPoint.position, attackRange, m_WhatIsEnemies);
        foreach (Collider2D enemy in damageEnemies)
        {
            Debug.Log("We hit: " + enemy);
            enemy.GetComponent<EnemyController2D>().TakeDMG(attackDMG);
        }
    } //Coroutine que permite que las colision de ataque quede más ajustada a la animacion del PJ!


    public void DoubleJump()
    {
        //can double jump se vuelve falso para que solo se pueda saltar una vez.
        canDoubleJump = false;
        // se resetea la velocidad a 0 para que se dejen de aplicar las fuerzas que tenia el PJ.
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
        // se añade la fuerza de salto de nuevo i suena el sonido de salto.
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        Instantiate(JumpSound);

    } //funcion que permite al jugador saltar una segunda vez en el aire.


    private void OnDrawGizmosSelected() // funcion que permite ver en el editor la hitbox de la espada.
    {
        Gizmos.DrawWireSphere(m_AttackPoint.position, attackRange);
    }

    private void Timers()
    {
        maxComboDelay -= Time.deltaTime;
        nextAttackTimer -= Time.deltaTime;
        knockbackTimer -= Time.deltaTime;
        lastTimeDamaged -= Time.deltaTime;

        if (maxComboDelay <= 0f) // controla el tiempo maximo que puede pasar entre ataques, para que salte la siguiente animacion de ataque.
        {
            noAttack = 0;
        }
        if (lastTimeDamaged <= 0f)
        {
            isDamaged = false;
        }
        if (knockbackTimer <= 0 && (!isDead || !isResting))
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private void Update()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }

            }
        }

        Timers();

        if (LifeBar <= 0) //contador en segundos que "detiene el juego" durante 3 segundos después d emorir, para que el Respawn en el checkpoint no sea instantaneo.
        {
            deadTimer -= Time.deltaTime;
            //Debug.Log("deadTimer" + deadTimer.ToString());
        }

        //Air resist in the horizontal way
        if (Input.GetButton("Horizontal"))
        {
            if (!m_Grounded && m_FacingRight)
            {
                m_Rigidbody2D.AddForce(Vector2.left * h_AirResist, 0);
            }
            else if (!m_Grounded && !m_FacingRight)
            {
                m_Rigidbody2D.AddForce(Vector2.right * h_AirResist, 0);
            }
        }
        if (Input.GetButtonDown("Heal"))
        {
            Heal();
        }
        if (Input.GetButtonUp("Attack"))
        {
            attackPressed = false;
        }
        if (Input.GetButton("Attack") && !attackPressed && nextAttackTimer <= 0)
        {
            Attack();
        }
        if (Input.GetButton("Jump") && m_Rigidbody2D.velocity.y > Mathf.Abs(Mathf.Epsilon))
        {
            state = State.jumping;

        }
        if (Input.GetButton("Jump") && canDoubleJump)
        {
            DoubleJump();
        }

        AnimationState();
        anim.SetInteger("state", (int)state); //obtiene el valor del integer que tiene state para que las condiciones de las animaciones funcionen correctamente.


        respawnReset = false;
        UpdateBarFill(LifeBar, fullHP);
        UpdateBarText(LifeBar, fullHP);

    }

}