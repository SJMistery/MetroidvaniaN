using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class CharacterController2D_Mod : MonoBehaviour
{

    private Animator anim;
    public enum State { idle, running, jumping, falling, dead, resting, climb }
    public State state = State.idle;

    [SerializeField] public float m_JumpForce = 1100f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    private PlayerAttack attack;
    [SerializeField] private GameObject PCS; //player-camera-shadow
    //[SerializeField] private BoxCollider2D hitbox;
    private GameObject[] potionCDImage; //imagen para el cooldown de la pocion
    private GameObject[] potionUsedImage; //imagen para el cooldown de la pocion
    private GameObject[] lifeStars; //imagen para el cooldown de la pocion
    private GameObject sombra;
    private PlayerMovement playerMovement;
    const float k_GroundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    public GameObject JumpSound; //gameobject que contiene el sonido cuando el Pj salta y otra funcion que lo destruye poco después.
    private Vector3 RespawnPoint;
    [SerializeField] private GameObject shadowReset; //objeto que manipula la "sombra del pj".

    //BOOLEANS
    private bool isDamaged = false; //sirve para que el PJ no pierda más de 1 vida cuando entra en contacto con el enemigo.
    private bool canAttack = false;
    private bool isDead = false; //sirve para detener el movimiento del PJ cuando muere.
    private bool isResting = false;
    public bool respawnReset;
    public bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool m_AirControl = true;   // Whether or not a player can steer while jumping
    public bool canDoubleJump = false;
    public bool bobinaDelTiempo = false;

    //TIMERS
    private float knockbackTimer = 3f;
    private float deadTimer = 3f;
    private float lastTimeDamaged;      //Timer que controla el tiempo que el jugador es Invencible después de recibir daño.
    private float cantAttackTimer;           //Timer que controla el tiempo que tarda en poder volver a atacar el jugador después de recibir daño.
    float targetFill = 0.0f;            //valores para hacer los calculos de la barra de vida.
    float _maxValue = 25.0f;            //valores para hacer los calculos de la barra de vida.
    private float _currentFraction = 1.0f;


    //OTHER VARIABLES
    private int fullHP = 0;         //vida maxima del PJ
    public int LifeBar;             //Vida actual del PJ.
    public int hpRecovered = 1;     //Cantidad de vida que recupera cada cura.
    public int maxHeals = 3;        //numero máximo de curas.
    public int healsAvalible;      //curas actuales disponibles.
    private float hurtforce = 15;        //potencia del knockback que recibe el PJ.
    private float h_AirResist = 50f;  // variable que controla la resistencia del aire en el eje horizontal.
    private float invencibleTime = 1.5f; //Tiempo para el timer lastTimeDamaged
    private float cantAttackValue = 0.25f;    //Tiempo para el timer cantAttackTimer

    int count = 0;                  //contador para ?? las pociones
    int frameRet = 180;             //contador para ?? las pociones
    int frameCoold = 100;           //contador para ?? las pociones

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    public bool controller = false; //detecta si hay un mando conectado
    //public bool controLock = true; // bloquea los controles de PC cuando hay mandos conectados

    private void Start()
    {
        PCS = GameObject.FindGameObjectWithTag("P-C-S");
        sombra = GameObject.Find("SombrAlpha 1");
        playerMovement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        potionCDImage = new GameObject[maxHeals];
        potionUsedImage = new GameObject[maxHeals];
        potionCDImage = GameObject.FindGameObjectsWithTag("potionCD");
        potionUsedImage = GameObject.FindGameObjectsWithTag("potion");
        lifeStars = GameObject.FindGameObjectsWithTag("LifeStar");
        if (GlobalController.Instance.fromBeginning == true)
        {
            if (PCS != null)
                PCS.transform.position = GlobalController.Instance.actualPos;

            fullHP = GlobalController.Instance.maxHp;
            LifeBar = GlobalController.Instance.hp;
            maxHeals = GlobalController.Instance.maxpotions;
            healsAvalible = GlobalController.Instance.disp_potions;
        }
        else
        {
            fullHP = LifeBar;
        }
    }

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
    }

    private void UpdateHealFill(float currentValue, float maxValue, int i)
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
        potionCDImage[i].GetComponent<Image>().fillAmount = 1 - targetFill;
    } //funcion que controla como se rellena o se vacia la barra de vida.

    // NEW O MOVIDO TODO LO QUE HAY A PARTIR DE AQUI.
    public float GetCurrentFraction //funcion para hacer los calculos de la ui de la barra de vida.
    {
        get
        {
            return _currentFraction;
        }
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

            if (!playerMovement.isClimbing && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) //El PJ no se puede girar cuando esta atacando o subiendo escaleras.
            {
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
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            OnLandEvent.Invoke();
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            Instantiate(JumpSound);
            //Debug.Log("estoy usando el script de jump");
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
        transform.Rotate(0f, 180f, 0f);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && !isDamaged)
        {
            TakeDMG();
            if (LifeBar > 0)
            {
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
        }
        else if (collision.collider.tag == "DeathTrap")
        {
            state = State.dead;
        }
    } //CAMBIADO A ON COLLISION ENTRE Y ON COLLISION EXIT. ASI NO HACE FALTA QUE TENGAMOS MÁS DE UN BOXCOLLIDER, PARA QUE SE ACTIVEN LOS TRIGGERS I LAS COLISIONES HAGAN EFECTO.

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "SavePoint" && ((controller & Input.GetButton("Interact MANDO")) || (!controller && Input.GetButton("Interact"))))
        {
            //Debug.Log("saved");
            isResting = true;
            RespawnPoint = other.gameObject.transform.position;
            LifeBar = fullHP;
            state = State.resting;
            healsAvalible = maxHeals;
        }
        if (other.gameObject.tag == "Enemy" && !isDamaged)
        {
            TakeDMG();
            if (other.gameObject.transform.position.x <= transform.position.x)
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
        if (other.gameObject.tag == "Boss" && !isDamaged)
        {
            TakeDMG();
            if (other.gameObject.transform.position.x <= transform.position.x)
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
        if (other.gameObject.tag == "Mace" && !isDamaged)
        {
            TakeDMG();
            if (other.gameObject.transform.position.x <= transform.position.x)
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
    }

    //NEW Y MOVIDO

    private void AnimationState() //NEEW Permite controlar los estados y logica de las animaciones
    {

        if (state == State.resting)
        {
            GetComponent<PlayerMovement>().enabled = false;
            m_Rigidbody2D.velocity = new Vector2(0, 0);
            isResting = true;
            isDead = false;
            if (Input.anyKeyDown)
            {
                GetComponent<PlayerMovement>().enabled = true;
                isResting = false;
                state = State.idle;

            }
        }
        else if (state == State.dead && GlobalController.Instance.invencible == false)
        {
            isDead = true;
            GetComponent<PlayerMovement>().enabled = false;
            m_Rigidbody2D.velocity = new Vector2(0, 0);

            healsAvalible = 0;
            LifeBar = 0;

            if (deadTimer <= 0 && isDead) //Contador de 3 segundos que controla que el PJ no haga respawn hasta que se haya finalizado este tiempo.
            {
                deadTimer = 3f;
                transform.position = RespawnPoint;
                shadowReset.transform.position = RespawnPoint;
                respawnReset = true;
                LifeBar = fullHP;
                m_Rigidbody2D.velocity = new Vector2(0, 0);
                GetComponent<PlayerMovement>().enabled = true;
                state = State.resting;
                healsAvalible = maxHeals;
            }
        }
        else if (state == State.climb)
        {
            if (!playerMovement.isClimbing)
            {
                state = State.idle;
            }
        }
        else if (state == State.jumping)
        {
            //saltando
            if (m_Rigidbody2D.velocity.y < 5)
            {
                state = State.falling;
            }
            if (playerMovement.isClimbing)
            {
                state = State.climb;
            }
        }
        else if (state == State.falling)
        {
            //cayendo
            if (m_Grounded) //cuando toca el suelo
            {
                state = State.idle;
            }
            if (playerMovement.isClimbing)
            {
                state = State.climb;
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
            if (playerMovement.isClimbing)
            {
                state = State.climb;
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
            if (playerMovement.isClimbing)
            {
                state = State.climb;
            }
        }
    }

    public void TakeDMG()
    {
        LifeBar -= 1;

        if (LifeBar <= 0)
        {
            state = State.dead;
        }
        else
        {
            isDamaged = true;
            canAttack = false;
            anim.SetTrigger("isHurt");

            lastTimeDamaged = invencibleTime;
            cantAttackTimer = cantAttackValue;
        }

    }

    private void Heal()
    {

        //potion system
        if (count < frameRet)
        {
            count++;
        }
        if ((controller && Input.GetButtonDown("Heal MANDO") || ((!controller) && Input.GetButtonDown("Heal"))) && (count > frameCoold))
        {
            count = 0;

            if (LifeBar != fullHP)
            {
                if (healsAvalible > 0 && state != State.dead)
                {
                    LifeBar += hpRecovered;
                    if (LifeBar >= fullHP)
                    {
                        LifeBar = fullHP;
                    }
                    healsAvalible -= 1;
                }
                else
                {
                    healsAvalible = 0;
                }


            }
        }

        for (int i = 0; i < maxHeals; i++)
        {
            if (healsAvalible - 1 >= i)
                UpdateHealFill(count, frameCoold, i);
            else
                potionUsedImage[i].SetActive(true);
        }
    }

    public void DoubleJump()
    {
        //can double jump se vuelve falso para que solo se pueda saltar una vez.
        canDoubleJump = false;
        // se resetea la velocidad a 0 para que se dejen de aplicar las fuerzas que tenia el PJ.
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
        // se añade la fuerza de salto de nuevo i suena el sonido de salto.
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        //Debug.Log("estoy haciendo dobles saltos");
        Instantiate(JumpSound);

    } //funcion que permite al jugador saltar una segunda vez en el aire.

    private void Timers()
    {
        knockbackTimer -= Time.deltaTime;
        lastTimeDamaged -= Time.deltaTime;
        cantAttackTimer -= Time.deltaTime;


        if (lastTimeDamaged <= 0f)
        {
            isDamaged = false;
        }
        if (cantAttackTimer <= 0f)
        {
            canAttack = true;
        }
        if (knockbackTimer <= 0 && (!isDead || !isResting))
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private void Update()
    {

        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected

                    //Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                    controller = true;
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    //Debug.Log("Controller: " + i + " is disconnected.");
                    controller = false;

                }
            }
        }
        else controller = false;

        //HACER QUE CUANDO EL PJ RECIBA DAÑO NO PUEDA ATACAR. PARA ESO SE TIENE QUE MOVER EL CODIGO DE ATAQUE EN UN NUEVO SCRIPT.
        if (!canAttack)
        {
            attack.enabled = false;
        }
        else
        {
            attack.enabled = true;
        }

        if ((controller && Input.GetButtonDown("Heal MANDO") || ((!controller) && Input.GetButtonDown("Heal"))) && LifeBar != fullHP)
        {
            Heal();
        }
        if ((Input.GetButton("Jump") || Input.GetButton("Jump MANDO")) && m_Rigidbody2D.velocity.y > Mathf.Abs(Mathf.Epsilon))
        {
            state = State.jumping;

        }
        if (((controller && Input.GetButtonDown("Jump MANDO")) || ((!controller) && Input.GetButtonDown("Jump"))) && canDoubleJump)
        {
            DoubleJump();
        }

        /*if (bobinaDelTiempo) { 
            sombra.GetComponent<SpriteRenderer>().enabled = true;
            sombra.GetComponent<InverseTime>().enabled = true;
        }
        else if (!bobinaDelTiempo) {
            sombra.GetComponent<SpriteRenderer>().enabled = false;
            sombra.GetComponent<InverseTime>().enabled = false; 
        }*/

        AnimationState();
        anim.SetInteger("state", (int)state); //obtiene el valor del integer que tiene state para que las condiciones de las animaciones funcionen correctamente.


        Timers();

        if (LifeBar <= 0) //contador en segundos que "detiene el juego" durante 3 segundos después d emorir, para que el Respawn en el checkpoint no sea instantaneo.
        {
            deadTimer -= Time.deltaTime;
            //Debug.Log("deadTimer" + deadTimer.ToString());
        }

        respawnReset = false;



        //A PARTIR DE AQUI ESTAN LA COSAS DE DAVID DE LA BARRA DE VIDA.
        for (int i = 0; i <= healsAvalible - 1; i++)
        {
            potionUsedImage[i].SetActive(false);
        }

        if (LifeBar >= 0)
        {
            for (int i = fullHP; i > LifeBar; i--)
            {
                lifeStars[i - 1].SetActive(false);
            }
        }
        for (int i = 0; i <= LifeBar - 1; i++)
        {
            lifeStars[i].SetActive(true);
        }
        if (count < frameRet)
        {
            count++;
        }
        for (int i = 0; i < maxHeals; i++)
        {
            if (healsAvalible - 1 >= i)
                UpdateHealFill(count, frameCoold, i);
            else
                potionUsedImage[i].SetActive(true);
        }

        if (GlobalController.Instance.invencible)
        {
            LifeBar = fullHP;
        }

        bobinaDelTiempo = GlobalController.Instance.inverseTimeActive;

    }

    private void FixedUpdate()
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

        //Air resist in the horizontal wa
        if ((controller && (Mathf.Abs(Input.GetAxis("Horizontal MANDO")) > 0.3)) || ((!controller) && Input.GetButtonDown("Horizontal")))//detecta si esta pulsando hacia arriba/abajo con el mando
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

        if (Input.GetButton("Horizontal") || Input.GetButton("Horizontal MANDO"))
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
    }
}