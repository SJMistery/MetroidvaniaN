using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class CharacterController2D_Mod: MonoBehaviour
{

	private Animator anim;

	private enum State { idle, running, jumping, falling, hurt, dead, resting, attacking1, attacking2, attacking3 }
	private State state = State.idle;

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public float h_AirResist; // variable que controla la resistencia del aire en el eje horizontal.
	public GameObject JumpSound; //gameobject que contiene el sonido cuando el Pj salta y otra funcion que lo destruye poco después.

	private int fullHP = 0; //barra del PJ
	private Image barImage; //imagen para la barra de vida
	private Color barColor = Color.red; //color de la barra de vida
	private Gradient barGradient = new Gradient();
	private float _currentFraction = 1.0f;
	private TextMeshProUGUI barText; //texto para la barra de vida
	float targetFill = 0.0f; //valores para hacer los calculos de la barra de vida.
	float _maxValue = 25.0f; //valores para hacer los calculos de la barra de vida.
	private Vector3 RespawnPoint;
	public int LifeBar;
	
	private int heal = 1;
	private int maxHeal = 3;
	private int healAvalible;
	public float hurtforce;
	private float contadormuerte = 3f;
	public int noOfClicks = 0;
	public float maxComboDelay = 0f; //Time when last button was clicked.  Delay between clicks for which clicks will be considered as combo.
	bool collided; //sirve para que el PJ no pierda más de 1 vida cuando entra en contacto con el enemigo.
	bool attackPressed; //sirve para que la animacion de ataque solo se ejecute 1 sola vez.
	public bool respawnReset;

	private BoxCollider2D hitbox;
	private float waitAttackTime = 0.25f;

	
	[SerializeField] private GameObject shadowReset;


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
		healAvalible = maxHeal;

		barImage = GameObject.Find("Green_Bar").GetComponent<Image>();
		barText = GameObject.Find("Life_Bar_Text").GetComponent<TextMeshProUGUI>();
		hitbox = GameObject.Find("Espada").GetComponent<BoxCollider2D>();
		hitbox.enabled = false;

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

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Debug.Log("collided");
		if (other.gameObject.tag == "Enemy" && !collided)
		{

			LifeBar -= 1;
			state = State.hurt;

			if (LifeBar <= 0)
			{
				//state = State.dead;
				Debug.Log("killed");
				state = State.dead;
				GetComponent<PlayerMovement>().enabled = false;
				m_Rigidbody2D.velocity = new Vector2(0, 0);
			}
			else
			{
				Debug.Log("damaged");
				//state = State.hurt;
				if (other.gameObject.transform.position.x <= transform.position.x)
				{
					// enemy is at right so PJ should move to left
					m_Rigidbody2D.velocity = new Vector2(hurtforce, hurtforce * 0.5f);
				}
				else
				{
					m_Rigidbody2D.velocity = new Vector2(-hurtforce, hurtforce * 0.5f);
				}
			}
			collided = true;
		}
		else if (other.gameObject.tag == "DeathTrap")
		{
			LifeBar = 0;
			state = State.dead;
			GetComponent<PlayerMovement>().enabled = false;
			m_Rigidbody2D.velocity = new Vector2(0, 0);
		}

	} //NEEW y MOVIDO
	private void OnTriggerExit2D(Collider2D other) //funcion, que convinada con la de ontriggerenter2d y la variable collided, hace que el Pj solo reciba daño una sola vez.
	{
		if (other.gameObject.tag == "Enemy")
		{
			collided = false;
		}
	}
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "SavePoint" && Input.GetButton("Interact"))
		{
			Debug.Log("saved");
			RespawnPoint = other.gameObject.transform.position;
			LifeBar = fullHP;
			state = State.resting;
			healAvalible = maxHeal;

		}
	} //NEW Y MOVIDO


	private void AnimationState() //NEEW Permite controlar los estados y logica de las animaciones
	{

		if (state == State.resting)
		{
			GetComponent<PlayerMovement>().enabled = false;
			m_Rigidbody2D.velocity = new Vector2(0, 0);
			if (Input.GetButton("Cancel"))
			{
				GetComponent<PlayerMovement>().enabled = true;
				state = State.idle;

			}
		}
		else if (state == State.dead)
		{
			if (contadormuerte <= 0) //Contador de 3 segundos que controla que el PJ no haga respawn hasta que se haya finalizado este tiempo.
			{
				contadormuerte = 3;
				transform.position = RespawnPoint;
				shadowReset.transform.position = RespawnPoint;
				respawnReset = true;
				LifeBar = fullHP;
				GetComponent<PlayerMovement>().enabled = true;
				state = State.resting;
			}
		}
		else if (state == State.hurt)
		{
			if (m_Grounded && Mathf.Abs(m_Rigidbody2D.velocity.x) < 1f)
			{
				state = State.idle;
				GetComponent<PlayerMovement>().enabled = true;
			}
			else
			{
				GetComponent<PlayerMovement>().enabled = false;
			}
		}
		else if (Input.GetButton("Attack") && !attackPressed)
		{

			maxComboDelay = 1.5f;
			waitAttackTime = 0.25f;
			//Record time of last button click
			noOfClicks++;
			if (noOfClicks == 1)
			{
				state = State.attacking1;
				hitbox.enabled = true;
			}
			else if (noOfClicks == 2)
			{
				state = State.attacking2;
				hitbox.enabled = true;
			}
			else if (noOfClicks == 3)
			{
				state = State.attacking3;
				noOfClicks = 0;
				hitbox.enabled = true;
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
	private void HealHP()
	{
		if(healAvalible > 0)
		{
			LifeBar += heal; ;
			if (LifeBar >= fullHP)
			{
				LifeBar = fullHP;
			}
		}
		else
		{
			healAvalible = 0;
		}
		healAvalible -= 1;

	} //NEEEW


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

		/*QUITADO, nuevo o movido*/

		maxComboDelay -= Time.deltaTime;
		waitAttackTime -= Time.deltaTime;


		if (maxComboDelay <= 0f)
		{
			noOfClicks = 0;
			maxComboDelay = 0;
		}
		if (waitAttackTime <= 0f)
		{
			waitAttackTime = 0;
			hitbox.enabled = false;
		} 

		if (LifeBar <= 0) //contador en segundos que "detiene el juego" durante 3 segundos después d emorir, para que el Respawn en el checkpoint no sea instantaneo.
		{
			contadormuerte -= Time.deltaTime;
			//Debug.Log("contadormuerte" + contadormuerte.ToString());
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
			HealHP();
		}
		if (Input.GetButtonUp("Attack"))
		{
			attackPressed = false;
		}
		if (Input.GetButton("Jump") && m_Rigidbody2D.velocity.y > Mathf.Abs(Mathf.Epsilon))
		{
			state = State.jumping;

		}

		AnimationState();
		anim.SetInteger("state", (int)state); //obtiene el valor del integer que tiene state para que las condiciones de las animaciones funcionen correctamente.


		respawnReset = false;
		UpdateBarFill(LifeBar, fullHP);
		UpdateBarText(LifeBar, fullHP);

	}


}