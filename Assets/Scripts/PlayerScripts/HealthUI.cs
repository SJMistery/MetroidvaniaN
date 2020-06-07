using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    //This script controls the Health and the Heals of the UI

    CharacterController2D_Mod characterController; // Start is called before the first frame update
    public Image[] hearts;

    public Image[] potions;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Sprite fullPotion;
    public Sprite emptyPotion;
    void Start()
    {
        characterController = GetComponent<CharacterController2D_Mod>();
    }

    // Update is called once per frame
    void Update()
    {
        //Health and hearts.
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < characterController.LifeBar)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
           
            if (i < characterController.fullHP)
            {

                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;

            }
        }

        //Heals and potions.
        for (int i = 0; i < potions.Length; i++)
        {
            if (i < characterController.healsAvalible)
            {
                potions[i].sprite = fullPotion;
            }
            else
            {
                potions[i].sprite = emptyPotion;
            }

            if (i < characterController.maxHeals)
            {

                potions[i].enabled = true;
            }
            else
            {
                potions[i].enabled = false;

            }
        }

    }
}
