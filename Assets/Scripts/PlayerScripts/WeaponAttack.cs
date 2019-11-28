using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    #region Attributes
    SpriteRenderer rend;
    public Vector3 Drawn = new Vector3(.51f, 1.0f, 0.0f);
    public Vector3 Holstered = new Vector3(.51f, -1.0f, 0.0f);
    int sequenceCount = 0;
    public float speedW = 1;
    bool attacking = false;
    #endregion
    private void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        rend.enabled = false;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown("t"))
        {
            attacking = true;
            rend.enabled = true;
            
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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (attacking)// && (other.gameObject.tag == "Enemy"))
        {
            Object.Destroy(other.gameObject);
            Debug.Log("I AM ATTACKING SOMEONE");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (attacking)// && (other.gameObject.tag == "Enemy"))
        {
            Object.Destroy(other.gameObject);
            Debug.Log("I AM ATTACKING SOMEONE");
        }
    }
    public void AttackOn()
    {

    }
    public void AttackOff()
    {

    }
};
