using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{

    private CharacterController2D_Mod Control;
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject cutsceneText;
    float HMov = 0f;
    private float Accel = 7f;
    private float Brake = 100f;
    private float BaseAcc = 15f;
    private float TopSpd = 75f;
    private float Drag = 30f;
    
    float HSpd;
    bool Jump = false;
    bool Crouch = false;
    bool Pivoting = false;
    float LHMov = 0f;
    bool JumpStart = false;
    public bool unpaused = true;
    public Rigidbody2D playerBody;

    private float distance = 0.1f; //distancia a la que el raycast detecta la escalera. ES VERTICAL, tiene que estar muy bajo.
    public LayerMask whatIsLadder;
    public LayerMask transitionPoint;
    public LayerMask levelPart;
    public LayerMask shadowRadius;
    public LayerMask interactiveEffect;
    public bool isClimbing;
    public string TPpointName;
    [SerializeField] private bool canTP;
    [SerializeField] private float inputVertical;
    [SerializeField] private float speed = 15;

    // Update is called once per frame
    
    
    private void Start()
    {
        unpaused = true;
        Control = GetComponent<CharacterController2D_Mod>();
        if(GlobalController.Instance.actualLevel == GlobalController.Level.STORAGE || GlobalController.Instance.actualLevel == GlobalController.Level.PRISON)
        {
            cutscene = GameObject.FindGameObjectWithTag("cutscene");
            cutsceneText = GameObject.Find("CutsceneText");
            cutscene.SetActive(false);
        }
        if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE && GlobalController.Instance.firstCutsceneEnded)
        {
            cutscene = GameObject.Find("Cutscene");
            cutsceneText = GameObject.Find("CutsceneText");
            cutscene.SetActive(false);
        }

    }
    //Update with no physics, FixedUpdate with physics
    void Update()
    {
        unpaused = !GlobalController.Instance.stopAll;

        if (unpaused && !GlobalController.Instance.moveIT )
        {
            LHMov = HMov;
            HMov = Input.GetAxisRaw("Horizontal");
            if (LHMov * HMov < 0)
            {
                //PIVOTING
                Pivoting = true;
            }
            else Pivoting = false;


            if (!Pivoting && (Input.GetButtonDown("Horizontal")))
            {
                HSpd += BaseAcc;
            }
            else if (Pivoting && (Input.GetButtonDown("Horizontal")))
            {
                HSpd -= BaseAcc;
            }
            if (Input.GetButton("Horizontal"))
            {
                if (HSpd < TopSpd)
                {
                    HSpd += Accel;

                }
            }
            else
            {
                if (HSpd > 0)
                {
                    HSpd -= Brake;

                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                Jump = true;
                JumpStart = true;

            }

            //assegura que el PC no es passi de rapid i no vagi cap enrera
            if (HSpd > TopSpd)
            {
                HSpd = TopSpd;
            }
            if (HSpd < 0)
            {
                HSpd = 0;

            }
        }
        else
        {
            //GetComponent<Rigidbody2D>().simulated = false;
            RaycastHit2D shadowActivationRadius = Physics2D.Raycast(transform.position, Vector2.up, distance, levelPart);

            if (shadowActivationRadius.collider != null)
            {
                //GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        
    }


    void FixedUpdate()
    {
        if (!GlobalController.Instance.moveIT || !GlobalController.Instance.stopAll)
        {
            Control.Move(HMov * HSpd * Time.fixedDeltaTime, Crouch, JumpStart);
            JumpStart = false;
            if (Jump)
            {

                playerBody.AddForce(new Vector2(0f, -Drag));
                Jump = false;

            }
        }
        //para subir escaleras
        //detecta si hay una escalera donde el pj dibujando una linea que funciona de detector
        RaycastHit2D ladderInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if (ladderInfo.collider != null)
        {
            if (Input.GetButtonDown("Vertical"))//detecta si esta pulsando hacia arriba/abajo
            {
                isClimbing = true;
            }

        }
        else
        {
            isClimbing = false;
        }
        if (isClimbing == true)
        {
            inputVertical = Input.GetAxisRaw("Vertical");//si lo hace sube o baja
            playerBody.velocity = new Vector2(playerBody.velocity.x, inputVertical * speed);
            playerBody.gravityScale = 0;
        }
        else
        {
            playerBody.gravityScale = 4;
        }

        RaycastHit2D levelPartInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, levelPart);

        if (levelPartInfo.collider != null)
        {
            GlobalController.Instance.nameOfPartLevel = levelPartInfo.collider.name;
        }
    }

    //Para teleportarse, detecta si hay un punto de TP
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.tag == "Beginning")//Para ir al principio del nivel
        {

            if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE  )
            {
                LoadScenes.Instance.LoadInsideBegLevel();
                 
            }
            if (GlobalController.Instance.actualLevel == GlobalController.Level.CAVE  )
            {
                LoadScenes.Instance.LoadBCOutsideLevel();
                 
            }
        }

        if (collision.tag == "Cave")//Para ir a la cueva
        {

            if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE  )
            {
                LoadScenes.Instance.LoadCaveLevel();
                 
            }
        }

        if (collision.tag == "CaveE")//Para ir a la cueva
        {

            if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE  )
            {
                LoadScenes.Instance.LoadCaveEndLevel();
                 
            }
        }

        if (collision.tag == "End")//Para ir a la cueva
        {

            if (GlobalController.Instance.actualLevel == GlobalController.Level.CAVE  )
            {
                LoadScenes.Instance.LoadACOutsideLevel();
                 
            }
        }

        if (collision.tag == "Up")//Parte alta del nivel
        {
            if (GlobalController.Instance.actualLevel == GlobalController.Level.ROOF  )
            {
                LoadScenes.Instance.LoadInsideUpLevel();
                 
            }
            if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE  )
            {
                if (collision.name == "TPpoint storage")
                {
                    LoadScenes.Instance.LoadStorageUpLevel();
                     
                }
                else
                {
                    LoadScenes.Instance.LoadRoofLevel();
                     
                }
            }
            if (GlobalController.Instance.actualLevel == GlobalController.Level.STORAGE  )
            {
                LoadScenes.Instance.LoadInsideUpSTLevel();
                 
            }

        }
        if (collision.tag == "Middle")
        {
            if (GlobalController.Instance.actualLevel == GlobalController.Level.PRISON  )
            {
                LoadScenes.Instance.LoadInsideMidLevel();
                 
            }
            if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE  )
            {
                if (collision.name == "TPpoint storage")
                {
                    LoadScenes.Instance.LoadStorageMiddleLevel();
                     
                }
                else
                {
                    LoadScenes.Instance.LoadPrisonBegLevel();
                     
                }
            }
            if (GlobalController.Instance.actualLevel == GlobalController.Level.STORAGE  )
            {
                LoadScenes.Instance.LoadInsideMidSTLevel();
                 
            }
        }
        if (collision.tag == "Down")
        {
            if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE  )
            {
                LoadScenes.Instance.LoadPrisonEndLevel();
                 
            }

            if (GlobalController.Instance.actualLevel == GlobalController.Level.PRISON  )
            {
                LoadScenes.Instance.LoadInsideDownLevel();
                 
            }
        }
    }
}