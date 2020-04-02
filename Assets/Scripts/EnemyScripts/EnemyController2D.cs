using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnemyController2D : MonoBehaviour
{

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public Animator enemyAnim;
    private GameObject srBeta;

    public int maxHP = 3;
    public int currentHP;
    public float hurtforceX;
    public float hurtforceY;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }



    private void Awake()
    {
        srBeta = GameObject.Find("SrBeta1");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        currentHP = maxHP;
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "DeathTrap")
        {
            TakeDMG(maxHP);
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

    public void TakeDMG(int dmg)
    {
        currentHP -= dmg;
        enemyAnim.SetTrigger("hurt");

        //Debug.Log(" " + currentHP);

        //si el enemigo tiene 0 hp se muere.
        if (currentHP <= 0)
        {
            Die();
        }

        else if (srBeta.transform.position.x <= transform.position.x)
        {
            m_Rigidbody2D.AddForce(new Vector2(hurtforceX, 0f));
        }
        else if (srBeta.transform.position.x > transform.position.x)
        {
            m_Rigidbody2D.AddForce(new Vector2(-hurtforceX, 0f));
        }

    }
   
    public void StopMovement()
    {
        enemyAnim.SetBool("Moving", false);
    }

    void Die()
    {
        enemyAnim.SetBool("Dead", true);
        //Debug.Log("enemy is dead");

        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;    //Congela todas las direcciones para que al morir, para que el PJ no salga disparado
        GetComponent<Collider2D>().enabled = false;                 //Desactiva el boxcollider para que se posible atravessar el cuerpo.

    }
}
