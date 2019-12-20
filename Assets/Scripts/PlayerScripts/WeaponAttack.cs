using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    #region Attributes
    SpriteRenderer rend;
    Collider2D hitbox;
    public Vector3 Drawn = new Vector3(.51f, 1.0f, 0.0f);
    public Vector3 Holstered = new Vector3(.51f, -1.0f, 0.0f);
    int sequenceCount = 0;
    public float speedW = 1;
    bool attacking = false;
    #endregion
    private void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        rend.enabled = false;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown("t"))
        {
            attacking = true;
            
            
        }
        if (attacking && (sequenceCount == 0))
        {
            rend.enabled = true;
            hitbox.enabled = true;
        }
        if (attacking && (sequenceCount < 120))
        {
            sequenceCount++;
        }
        else if (attacking)
        {
            sequenceCount = 0;
            attacking = false;
            rend.enabled = false;
            hitbox.enabled = false;
        }
    }
    
    public void AttackOn()
    {

    }
    public void AttackOff()
    {

    }
};
