using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D Control;
    float HMov = 0f;
    public float HSpd = 40f;
    bool Jump = false;
    bool Crouch = false;
  
    // Update is called once per frame
    //Update with no physics, FixedUpdate with physics
    void Update() {
       HMov = Input.GetAxisRaw("Horizontal");
        //control.Move(2, false, false);
        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        if (Input.GetButton("Crouch"))
        {
            Crouch = true;
        }
        else Crouch = false;
    }
    void FixedUpdate()
    {
        Control.Move(HMov*HSpd*Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;

    }
}

//vector 4 spaces
//each second register position and put on last place, moving the three places one place ahead beforehand, discarding the oldest
//on press select the 
//cooldown de X seconds

