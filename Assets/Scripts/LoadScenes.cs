using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScenes : MonoBehaviour
{

    public GameObject player;
    [SerializeField] private GameObject shadow;
    public GameObject pcs;
    public static LoadScenes Instance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");
        pcs = GameObject.FindGameObjectWithTag("P-C-S");
        //Posiciones donde el personaje debera aparecer
        GlobalController.Instance.positionOutside = new Vector3(-1.81997f, 0.07341374f);
        GlobalController.Instance.positionInsideBeg = new Vector3(-3.51f, -3.755696f, -2.21f);
        GlobalController.Instance.positionInsideUp = new Vector3(24.49f, 39.07343f, -2.21f);
        GlobalController.Instance.positionInsideMid = new Vector3(28.70548f, -21.9266f, -2.21f);
        GlobalController.Instance.positionInsideLow = new Vector3(24.61228f, -53.92659f, -2.21f);
        GlobalController.Instance.positionRoof = new Vector3(43.63201f, 48.07341f);
        GlobalController.Instance.positionPrisonBeg = new Vector3(41.97f, -12.73f);
        GlobalController.Instance.positionPrisonEnd = new Vector3(33.76f, -43.92657f);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("0.Afueras de la Torre");
        //Estos tres le dan los valores iniciales al jugador
        GlobalController.Instance.maxHp = 5;
        GlobalController.Instance.hp = 5;
        GlobalController.Instance.cooldown = 100;
        GlobalController.Instance.maxpotions = 3;
        GlobalController.Instance.disp_potions = 3;
        GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionOutside;
        GlobalController.Instance.fromBeginning = true;
    }

    public void LoadInsideBegLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
       if(shadow.GetComponent<InverseTime>().count < 100)
        GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideBeg;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        SceneManager.LoadScene("1. Dentro del Castillo");
        Debug.Log("Deberia estar en el castillo");
    }

    public void LoadInsideUpLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideUp;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        SceneManager.LoadScene("1. Dentro del Castillo");
    }

    public void LoadInsideMidLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideMid;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        SceneManager.LoadScene("1. Dentro del Castillo");
    }
    public void LoadInsideDownLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideLow;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        SceneManager.LoadScene("1. Dentro del Castillo");
    }
    public void LoadRoofLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualLevel = GlobalController.Level.ROOF;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionRoof;

        SceneManager.LoadScene("1.5 Tejado y Prision");
    }
    public void LoadPrisonBegLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionPrisonBeg;
        GlobalController.Instance.actualLevel = GlobalController.Level.PRISON;
        SceneManager.LoadScene("1.5 Tejado y Prision");
    }
    public void LoadPrisonEndLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionPrisonEnd;
        GlobalController.Instance.actualLevel = GlobalController.Level.PRISON; 
        SceneManager.LoadScene("1.5 Tejado y Prision");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
