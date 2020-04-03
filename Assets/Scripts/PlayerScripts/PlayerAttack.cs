using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Transform m_AttackPoint;
    [SerializeField] private Transform m_AttackPointDown;
    [SerializeField] private LayerMask m_WhatIsEnemies;
    [SerializeField] private LayerMask m_WhatIsBoss;


    private Rigidbody2D m_Rigidbody2D;                          //Rigidbody del PJ para poder hacer el bounce después de golpear a un enemigo.
    private CharacterController2D_Mod characterController;      //referencia al script para poder coger el valor de Jumpforce y asi aplicar el bounce
    private Animator anim;
    [SerializeField] private GameObject attackSound1;
    [SerializeField] private GameObject attackSound2;
    [SerializeField] private GameObject hitSound;


    private float attackRange = 0.4f;
    private float downAttackRange = 1f;
    private bool attackPressed = false;         //sirve para que la animacion de ataque solo se ejecute 1 sola vez.
    private float maxComboDelay = 0f;           //Time when last button was clicked.  Delay between attacks for which clicks will be considered as combo.
    private float nextAttackTimer;              //Timer que controla el tiempo hay entre ataque y ataque. Para que no se pueda "spamear" el boton de ataque. Evita problemas, especialemnte con las animaciones.
    private float waitCollisionTime = 0.15f;    //Timer que controla el tiempo desde que se hace la animacion de ataque (forward) hasta que se activa la colision, para que encaje mejor la hitbox con la animacion.
    public int attackDMG = 1;                   //daño de ataque.
    private int noAttack = 0;                   //numero del ataque (1-2) para controlar que animación toca ejecutar después.


    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D_Mod>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void DownAttack()
    {
        //Run the animation.

        nextAttackTimer = 0.40f;
        anim.SetTrigger("attackingDown");
        Instantiate(attackSound2);
        attackPressed = true;
        //get all the enemies in range on the atack.
        StartCoroutine(DownAttack(0.10f));

    }
    void Attack()
    {
        maxComboDelay = 0.80f;
        nextAttackTimer = 0.40f;

        //Record time of last button click
        noAttack++;
        if (noAttack == 1)
        {

            anim.SetTrigger("attacking1");
            Instantiate(attackSound1);

        }
        if (noAttack == 2)
        {
            noAttack = 0;
            anim.SetTrigger("attacking2");
            Instantiate(attackSound2);

        }

        attackPressed = true;

        StartCoroutine(ForwardAttack(waitCollisionTime));
    }

    IEnumerator ForwardAttack(float time)
    {
        //The attack animation runs from AnimationState()
        //Esperar un breve periodo de tiempo antes de que salte el codigo, para que la animacion y la deteccion sean mas precisas.
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        //Detect the enemies in range onf the weapon
        Collider2D[] damageEnemies = Physics2D.OverlapCircleAll(m_AttackPoint.position, attackRange, m_WhatIsEnemies);
        foreach (Collider2D enemy in damageEnemies)
        {
            //Debug.Log("We hit: " + enemy);

            enemy.GetComponent<EnemyController2D>().hurtforceY = 0f;
            enemy.GetComponent<EnemyController2D>().TakeDMG(attackDMG);
            Instantiate(hitSound);
        }
        Collider2D[] damageBoss = Physics2D.OverlapCircleAll(m_AttackPoint.position, attackRange, m_WhatIsBoss);
        foreach (Collider2D boss in damageBoss)
        {
            boss.GetComponent<BossController2D>().TakeDMG(attackDMG);
            Instantiate(hitSound);
        }

    } //Coroutine que permite que las colision de ataque quede más ajustada a la animacion del PJ!
    IEnumerator DownAttack(float time)
    {
        //The attack animation runs from AnimationState()
        //Esperar un breve periodo de tiempo antes de que salte el codigo, para que la animacion y la deteccion sean mas precisas.
        yield return new WaitForSeconds(time);
        // Code to execute after the delay
        //Detect the enemies in range onf the weapon
        Collider2D[] damageDownEnemies = Physics2D.OverlapCircleAll(m_AttackPointDown.position, downAttackRange, m_WhatIsEnemies);
        foreach (Collider2D enemy in damageDownEnemies)
        {
            //Debug.Log("We hit: " + enemy);
            enemy.GetComponent<EnemyController2D>().hurtforceX = 0f;
            enemy.GetComponent<EnemyController2D>().hurtforceY = 2000f;
            enemy.GetComponent<EnemyController2D>().TakeDMG(attackDMG);

            //make the character bounce up.
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            // se añade la fuerza de salto de nuevo i suena el sonido de salto.
            m_Rigidbody2D.AddForce(new Vector2(0f, characterController.m_JumpForce * 1.2f));
            Instantiate(hitSound);
        }
        Collider2D[] damageBoss = Physics2D.OverlapCircleAll(m_AttackPoint.position, attackRange, m_WhatIsBoss);
        foreach (Collider2D boss in damageBoss)
        {
            boss.GetComponent<BossController2D>().TakeDMG(attackDMG);
            //make the character bounce up.
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            // se añade la fuerza de salto de nuevo i suena el sonido de salto.
            m_Rigidbody2D.AddForce(new Vector2(0f, characterController.m_JumpForce * 1.2f));
            Instantiate(hitSound);
        }

    } //Coroutine que permite que las colision de ataque quede más ajustada a la animacion del PJ!


    private void Timers()
    {
        maxComboDelay -= Time.deltaTime;
        nextAttackTimer -= Time.deltaTime;
        if (maxComboDelay <= 0f) // controla el tiempo maximo que puede pasar entre ataques, para que salte la siguiente animacion de ataque.
        {
            noAttack = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(m_AttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(m_AttackPointDown.position, downAttackRange);
    }

    void Update()
    {
        Timers();
        if (characterController.controller)
        {
            if (Input.GetButtonUp("Attack MANDO"))
            {
                attackPressed = false;
            }

            if (Input.GetButton("Attack MANDO") && (Input.GetAxis("Vertical MANDO") < -0.7) && !attackPressed && nextAttackTimer <= 0)
            {
                if (characterController.m_Grounded == false)
                {
                    DownAttack();
                }
            }

            else if (Input.GetButton("Attack MANDO") && !attackPressed && nextAttackTimer <= 0)
            {
                Attack();
            }
        }
        if (!characterController.controller)
        {
            if (Input.GetButtonUp("Attack"))
            {
                attackPressed = false;
            }

            if (Input.GetButton("Attack") && Input.GetButton("Down") && !attackPressed && nextAttackTimer <= 0)
            {
                if (characterController.m_Grounded == false)
                {
                    DownAttack();
                }
            }

            else if (Input.GetButton("Attack") && !attackPressed && nextAttackTimer <= 0)
            {
                Attack();
            }
        }
    }
}
