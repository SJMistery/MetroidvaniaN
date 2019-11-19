using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    #region Attributes

    public Transform Sword;
    public Vector3 Drawn = new Vector3(.51f, 1.0f, 0.0f);
    public Vector3 Holstered = new Vector3(.51f, -1.0f, 0.0f);
    int sequenceCount = 0;
    public float speedW = 1;
    bool attacking = false;
    Collider targetAttack;
    #endregion
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown("t"))
        {
            attacking = true;
        }
        if (attacking && (sequenceCount < 120))
        {
            sequenceCount++;
        }
        else if (attacking)
        {
            sequenceCount = 0;
            attacking = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((attacking) && (other.gameObject.tag == "Enemy"))
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
