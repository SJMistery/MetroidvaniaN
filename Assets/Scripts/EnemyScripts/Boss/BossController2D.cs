using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class BossController2D : MonoBehaviour
{

    public enum State { Nothing, Walk, Taunt, IddleTaunt }
    public State estado = State.Nothing;

    private Rigidbody2D m_Rigidbody2D;
    public Slider hpBar;        //Variable para hace la barra de vida del boss.
    private Transform player;
    private Transform firepoint;
    private Transform macePoint;
    public float throwSpeed = 500f;
    public float distanceWalls = 5.5f;
    private Animator anim;
    private MaceController maceController;

    bool isFlipped;
    public int maxHP = 3;
    public int currentHP;
    [SerializeField] private float growSize;
    public float dashDistance = 2;
    private int enrageHP = 10;
    Collider2D m_Collider;

    //añadir el gameobject mace, para poder inicializarlo cunado el enemigo entra en taunt.
    public GameObject mace;
    public GameObject maceWave;
    public GameObject throwMace;
    public float jumpForce = 500f;
    public float fallMultiplier = 7;
    public bool jumping = false;
    // bool que se utiliza en el script Boss_Taut i detecta si el enemigo ya ha generado los martillos, para no generarlos de manera infinita, 
    // Tiene que ser publico, porque la boleana se controla desde otro script.
    public bool maceIsSpawned = false;
    //create a list of possible spawns that get filled when a new mace is generated, so they don't end ovelaping with each other.
    public Vector3 spawnPoint;
    public List<Vector3> possibleSpaw = new List<Vector3>();
    private float radiusMace = 1.25f; //radio del collider de la maza para hacer un check del radio en al funcino spawn

    Vector2 targetAttack; //Vector para hacer el ataque normal.;
    Vector2 targetDash;
    public Vector2 targetSkyAttack;
    public float attackSpeed = 400f;
    public float dashSpeed = 2000f;
    public float skyAttackSpeed = 2000f;

    public bool enrage = false;
    private GlobalController globalController;


    //SOUNDS
    public GameObject swordsound;
    public GameObject attackready;
    public GameObject dashsound;
    public GameObject hammerattack;
    public GameObject hammerslam;
    public GameObject tauntscream;
    public GameObject walkingsound1;
    public GameObject walkingsound2;
    public GameObject skyattack;
    public GameObject jumpsound;
    public GameObject swordfall;
    public GameObject macefall;
    public GameObject deathscream;
    public GameObject fallbody;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("SrBeta1").GetComponent<Transform>();
        firepoint = GameObject.Find("Firepoint").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
        macePoint = GameObject.Find("Macepoint").GetComponent<Transform>();
        currentHP = maxHP;
        if (globalController.bossDeafeted == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        hpBar.value = currentHP; //Relaciona la barra de vida del boss con su vida actual.
        anim.SetInteger("State", (int)estado); //obtiene el valor del integer que tiene state para que las condiciones de las animaciones funcionen correctamente.
        if (jumping)
        {
            Jump();
        }
        if (m_Rigidbody2D.velocity.y > 0) //si el pj empieza a bajar se aplica el multiplicador de caida para que baje mas rapido que cuando sube.
        {
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;  //se resta 1 al fallMultiplier por que el propio unity le suma 1 a las variables de gravedad.
        }

    }

    public void TakeDMG(int dmg)
    {
        currentHP -= dmg;

        //si el enemigo tiene 0 hp se muere.
        if (currentHP <= 0)
        {
            Die();
        }
        if (currentHP == 16 || currentHP == 14 || currentHP == 12 || currentHP == 10)
        {
            estado = State.Taunt;
        }
    }

    void Die()
    {
        anim.SetBool("Die", true);
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;    //Congela todas las direcciones para que al morir, para que el PJ no salga disparado
        GetComponent<Collider2D>().enabled = false;                 //Desactiva el boxcollider para que se posible atravessar el cuerpo.
        
        globalController.bossDeafeted = true;
        StartCoroutine(waitToDestroy(5f));
    }

    IEnumerator waitToDestroy(float time)
    {
        //The attack animation runs from AnimationState()
        //Esperar un breve periodo de tiempo antes de que salte el codigo, i hacer desaparecer al enemigo.
        yield return new WaitForSeconds(time);
        globalController.bossDeafeted = true;
        Destroy(this.gameObject);
    }


    /*
    * 
    * 
    * 
    * 
    * CONJUNTO DE FUNCIONES PARA LAS ANIMACIONES O QUE SON LLAMADA DE SUBFUNCIONES DE LA ANIMACIONES DEL ENEMIGO. 
    * PUESTO QUE SE ESTA UTILIZANDO EL ANIMATOR DE UNITY COMO SI FUESE UN MAQUINA DE ESTADOS.
    * 
    * 
    * 
    * 
    */
    //Funcion que es llamada des del un frame concreto de la animacion de ataque, mueve el enemigo a muchisima velocidad a la direccion del PJ.

    public IEnumerator waitSeconds(float time)
    {
        //The attack animation runs from AnimationState()
        //Esperar un breve periodo de tiempo antes de que salte el codigo, i hacer desaparecer al enemigo.
        yield return new WaitForSeconds(time);
        if (currentHP > enrageHP)
        {
            estado = State.IddleTaunt;
        }
        else if (currentHP <= enrageHP)
        {
            anim.SetBool("Enrage", true);
            enrage = true;
        }
    }
    //Mirar al PJ.
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0);
            isFlipped = true;
        }
    }

    //Movimiento del boss ne la ejecucion del ataque
    public void MoveAttack()
    {
        Vector2 newPosition;
        newPosition = Vector2.MoveTowards(m_Rigidbody2D.position, targetAttack, attackSpeed * Time.deltaTime); //utiliza esa variable para hacer que el enemigo se mueva direccion hacia el jugador.
        m_Rigidbody2D.MovePosition(newPosition);                            //creates a new vector with the position of the player
    }

    //Movimiento dle boss en la animacion de dash
    public void MoveDash()
    {
        Vector2 newPosition;
        newPosition = Vector2.MoveTowards(m_Rigidbody2D.position, targetDash, dashSpeed * Time.deltaTime); //utiliza esa variable para hacer que el enemigo se mueva direccion hacia el jugador.
        m_Rigidbody2D.MovePosition(newPosition);
    }

    //Coje la posion del jugador para realizar el ataque hacia adelante.
    public void GetTarget()
    {
        if (player.position.x > m_Rigidbody2D.position.x) targetAttack = new Vector2(player.position.x + 5, m_Rigidbody2D.position.y);                             //creates a new vector with at posistion behind the player, to throw the mace there.
        else if (player.position.x < m_Rigidbody2D.position.x) targetAttack = new Vector2(player.position.x - 5, m_Rigidbody2D.position.y);
    }

    //Coje la posicion justo de delante del jugador, para no hacerle daño pero realizar el dash.
    public void GetTargetDash()
    {
        if (player.position.x > m_Rigidbody2D.position.x) targetDash = new Vector2(player.position.x - dashDistance, m_Rigidbody2D.position.y); //we want the enemy to fall short on the player position. so he gets right in front of him but without damaging.
        else if (player.position.x < m_Rigidbody2D.position.x) targetDash = new Vector2(player.position.x + dashDistance, m_Rigidbody2D.position.y);
    }
    
    //Funcion en desarrollo! Para cuando el jefe se tenga que hacer "TP" hasta la maza en el SkyAttack
    public void SkyAttack()
    {
        Vector2 newPosition;
        newPosition = Vector2.MoveTowards(m_Rigidbody2D.position, targetSkyAttack, skyAttackSpeed * Time.deltaTime); //utiliza esa variable para hacer que el enemigo se mueva direccion hacia el jugador.
        m_Rigidbody2D.MovePosition(newPosition);
    }
    
    public void GetMacePos()
    {

        maceController = GameObject.FindGameObjectWithTag("Mace").GetComponent<MaceController>();
        targetSkyAttack = maceController.target;
    }

    //Funcion que resetea la velocidad a 0 después de que el jefe Ataque, puesto que adquiere muchisima velocidad y puede dar problemas.
    public void ResetVelocity()
    {
        m_Rigidbody2D.velocity = new Vector2(0, 0);
    }

    //Funcion que controla la generacion de las mazas en el estado de Taunt del Jefe, Funciona junto con el script Boss_Taunt
    public void SpawNewMace()
    {
        spawnPoint = new Vector3(Random.Range(16, 46.2F), 58, 0);
        foreach (Vector3 possibleSpaw in possibleSpaw)
        {
            if ((spawnPoint.x >= (possibleSpaw.x - radiusMace)) && (spawnPoint.x <= (possibleSpaw.x + radiusMace)))
            {
                if ((spawnPoint.y >= (possibleSpaw.y - radiusMace)) && (spawnPoint.y <= (possibleSpaw.y + radiusMace)))
                {
                    SpawNewMace(); //If the width or length is near another one of these objects, then it tries again
                    return;
                }
            }
        }
        possibleSpaw.Add(spawnPoint);
        Instantiate(mace, spawnPoint, Quaternion.identity);
    }

    public void ThrowMace()
    {
        spawnPoint = macePoint.position;
        Instantiate(throwMace, spawnPoint, Quaternion.identity);
    }

    //Funcion que vuelve a activar las colisiones y el rb del boss después de hacer el taunt.
    public void ActivateCollidersAgain()
    {
        m_Collider.enabled = true;
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.None;
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //Genera el "proyectil en el ataque 'slam'
    public void SpawWave()
    {
        Instantiate(maceWave, firepoint.position, firepoint.rotation);
    }

    //Activa la segunda fase del boss.
    public void Enrage()
    {
        transform.localScale += new Vector3(growSize, growSize, 1);
        GetComponent<BoxCollider2D>().size = new Vector2(growSize, growSize);
        GetComponent<SpriteRenderer>().color = new Color(255, 142, 142);
    }

    public void Jump()
    {
        m_Rigidbody2D.AddForce(new Vector2(0,jumpForce));
    }

    public void DestroyMace()
    {
        maceController = GameObject.FindGameObjectWithTag("Mace").GetComponent<MaceController>();
        maceController.DetroyThis();
    }

    public void StayStatick()
    {
        m_Rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    public void StayDinamic()
    {
        m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }


    public void SoundSwordAttack()
    {
        Debug.Log("i'm using the sound");
        Instantiate(swordsound);
    }
    public void SoundAttackReady()
    {
        Instantiate(attackready);
    }
    public void SoundDash()
    {
        Instantiate(dashsound);
    }

    public void SoundHammerAttack()
    {
        Instantiate(hammerattack);
    }
    public void SoundHammerSlam()
    {
        Instantiate(hammerslam);
    }
    public void SoundTauntScream()
    {
        Instantiate(tauntscream);
    }
    public void SoundWalking1()
    {
        Instantiate(walkingsound1);
    }
    public void SoundWalking2()
    {
        Instantiate(walkingsound2);
    }
    private void SoundJump()
    {
        Instantiate(jumpsound);
    }
    private void SoundSwordDrop()
    {
        Instantiate(swordfall);
    }
    private void SoundMaceDrop()
    {
        Instantiate(macefall);
    }
    private void SoundDeathScream()
    {
        Instantiate(deathscream);
    }
    private void SoundFallBody()
    {
        Instantiate(fallbody);
    }

    private void MusicStop()
    {

    }
}


