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
    [SerializeField] private bool alredyTP;
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

        //Para teleportarse, detecta si hay un punto de TP
        RaycastHit2D tpInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, transitionPoint);

        if (tpInfo.collider != null)
        {
            canTP = true;
            TPpointName = tpInfo.collider.name;
        }
        else
        {
            canTP = false;
        }

        if (canTP == true)
        {
            if (tpInfo.collider.tag == "Beginning")//Para ir al principio del nivel
            {

                if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadInsideBegLevel();
                    alredyTP = true;
                }
            }

            if (tpInfo.collider.tag == "Cave")//Para ir a la cueva
            {

                if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadCaveLevel();
                    alredyTP = true;
                }
            }

            if (tpInfo.collider.tag == "CaveE")//Para ir a la cueva
            {

                if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadCaveEndLevel();
                    alredyTP = true;
                }
            }

            if (tpInfo.collider.tag == "Beginning")//Para salir desde el principio de la cueva
            {

                if (GlobalController.Instance.actualLevel == GlobalController.Level.CAVE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadBCOutsideLevel();
                    alredyTP = true;
                }
            }

            if (tpInfo.collider.tag == "End")//Para ir a la cueva
            {

                if (GlobalController.Instance.actualLevel == GlobalController.Level.CAVE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadACOutsideLevel();
                    alredyTP = true;
                }
            }

            if (tpInfo.collider.tag == "Up")//Parte alta del nivel
            {
                if (GlobalController.Instance.actualLevel == GlobalController.Level.ROOF && alredyTP == false && Input.GetButton("Interact"))
                {
                    LoadScenes.Instance.LoadInsideUpLevel();
                    alredyTP = true;
                }
                if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE && alredyTP == false)
                {
                    if (tpInfo.collider.name == "TPpoint storage")
                    {
                        LoadScenes.Instance.LoadStorageUpLevel();
                        alredyTP = true;
                    }
                    if (Input.GetButton("Interact") && !(tpInfo.collider.name == "TPpoint storage"))
                    {
                        LoadScenes.Instance.LoadRoofLevel();
                        alredyTP = true;
                    }
                    
                }
                if (GlobalController.Instance.actualLevel == GlobalController.Level.STORAGE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadInsideUpSTLevel();
                    alredyTP = true;
                }

            }
            if (tpInfo.collider.tag == "Middle")
            {
                if (GlobalController.Instance.actualLevel == GlobalController.Level.PRISON && alredyTP == false)
                {
                    LoadScenes.Instance.LoadInsideMidLevel();
                    alredyTP = true;
                }
                if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE && alredyTP == false)
                {
                    if (tpInfo.collider.name == "TPpoint storage")
                    {
                        LoadScenes.Instance.LoadStorageMiddleLevel();
                        alredyTP = true;
                    }
                    else
                    {
                        LoadScenes.Instance.LoadPrisonBegLevel();
                        alredyTP = true;
                    }
                }
                if (GlobalController.Instance.actualLevel == GlobalController.Level.STORAGE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadInsideMidSTLevel();
                    alredyTP = true;
                }
            }
            if (tpInfo.collider.tag == "Down")
            {
                if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE && alredyTP == false)
                {
                    LoadScenes.Instance.LoadPrisonEndLevel();
                    alredyTP = true;
                }

                if (GlobalController.Instance.actualLevel == GlobalController.Level.PRISON && alredyTP == false)
                {
                    LoadScenes.Instance.LoadInsideDownLevel();
                    alredyTP = true;
                }
            }
        
        }

        RaycastHit2D levelPartInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, levelPart);

        if (levelPartInfo.collider != null)
        {
            GlobalController.Instance.nameOfPartLevel = levelPartInfo.collider.name;
        }

        /*RaycastHit2D interactableEffectInfo = Physics2D.Raycast(transform.position, Vector2.down, distance, interactiveEffect);

        if (((Control.controller & Input.GetButton("Interact MANDO")) || (!Control.controller && Input.GetButton("Interact")))&& interactableEffectInfo.collider != null)
        {
            GlobalController.Instance.stopAll = true;
            Cursor.visible = true;
            cutscene.SetActive(true);
            cutsceneText.GetComponent<TextMeshProUGUI>().text = "What's this?";
            cutsceneText.GetComponent<Autotyping_Text>().start = true;
        }*/
    }
}