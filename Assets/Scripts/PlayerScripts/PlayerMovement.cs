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
    [SerializeField] private bool isClimbing;
    [SerializeField] private float inputVertical;
    [SerializeField] private float speed = 15;



    // Update is called once per frame
    private void Start()
    {
        unpaused = true;

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

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if(hitInfo.collider != null)
        {
            if (Input.GetButtonDown("Vertical"))
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
            inputVertical = Input.GetAxisRaw("Vertical");
            playerBody.velocity = new Vector2(playerBody.velocity.x, inputVertical * speed);
            playerBody.gravityScale = 0;
        }
        else
        {
            playerBody.gravityScale = 4;
        }
    }
}