using UnityEngine;

public class PlayerMovement: MonoBehaviour
{

    public CharacterController2D_Mod Control;
    float HMov = 0f;
    public float Accel = 1f;
    public float Brake = 2f;
    public float BaseAcc = 10f;
    public float TopSpd = 40f;
    float HSpd;
    bool Jump = false;
    bool Crouch = false;
    bool Pivoting = false;
    float LHMov = 0f;
    int JumpCount;
    bool JumpStart = false;
    public float Drag;
    public bool unpaused = true;
    public Rigidbody2D playerBody;
    public float jumpForce = 20;
    int counter;
    public float distance;
    public LayerMask whatIsLadder;
    public LayerMask transitionPoint;
    [SerializeField] private bool isClimbing;
    [SerializeField] private bool canTP;
    [SerializeField] private bool alredyTP;
    [SerializeField] private string tpTag;
    [SerializeField] private float inputVertical;
    [SerializeField] private float speed = 15;



    // Update is called once per frame
    private void Start()
    {
        unpaused = true;
        alredyTP = false;

    }
    //Update with no physics, FixedUpdate with physics
    void Update()
    {
        if (unpaused)
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
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
            JumpStart = true;

        }
        if (Input.GetButtonDown("Crouch"))
        {
            Crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Crouch = false;
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

    void FixedUpdate()
    {
        Control.Move(HMov * HSpd * Time.fixedDeltaTime, Crouch, JumpStart);
        JumpStart = false;
        if (Jump)
        {

            JumpCount++;
            playerBody.AddForce(new Vector2(0f, -Drag));
            if (JumpCount > 6)
            {
                Jump = false;
                JumpCount = 0;
            }
        }

        //para subir escaleras
        //detecta si hay una escalera donde el pj dibujando una linea que funciona de detector
        RaycastHit2D ladderInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if(ladderInfo.collider != null)
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

        //Transportarte a un punto desde x lugar
        RaycastHit2D tpInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, transitionPoint);

        if (tpInfo.collider != null)
        {
            canTP = true;
            tpTag = tpInfo.collider.tag;
        }
        else
        {
            canTP = false;
            tpTag = null;
        }

        if(canTP == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (tpInfo.collider.tag == "Beginning")//Para ir al principio del nivel
                {

                    if (GlobalController.Instance.actualLevel == GlobalController.Level.OUTSIDE && alredyTP == false)
                    {
                        LoadScenes.Instance.LoadInsideBegLevel();
                        alredyTP = true;
                    }
                }

                if (tpInfo.collider.tag == "Up")//Parte alta del nivel
                {
                    if (GlobalController.Instance.actualLevel == GlobalController.Level.ROOF && alredyTP == false)
                    {
                        LoadScenes.Instance.LoadInsideUpLevel();
                        alredyTP = true;
                    }
                    if (GlobalController.Instance.actualLevel == GlobalController.Level.INSIDE && alredyTP == false)
                    {
                        LoadScenes.Instance.LoadRoofLevel();
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
                        LoadScenes.Instance.LoadPrisonBegLevel();
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
        }
    }
}