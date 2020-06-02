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

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");
        pcs = GameObject.FindGameObjectWithTag("P-C-S");
        //Posiciones donde el personaje debera aparecer
        GlobalController.Instance.positionOutside = new Vector3(56.51f, 20.22f);
        GlobalController.Instance.positionOutsideBC = new Vector3(142.12f, 0.4f);
        GlobalController.Instance.positionOutsideAC = new Vector3(164.45f, -0.6f);
        GlobalController.Instance.positionCaveBeg = new Vector3(0f, -0.38f);
        GlobalController.Instance.positionCaveEnd = new Vector3(141.3f, 8.37f);
        GlobalController.Instance.positionInsideBeg = new Vector3(-3.51f, -3.755696f, -2.21f);
        GlobalController.Instance.positionInsideUp = new Vector3(22.01f, 39.18f, -2.21f);
        GlobalController.Instance.positionInsideMid = new Vector3(23.77f, -21.83f);
        GlobalController.Instance.positionInsideUpST = new Vector3(1.28f, 17.13f, -2.21f);
        GlobalController.Instance.positionInsideMidST = new Vector3(35.98f, -6.78f, -2.21f);
        GlobalController.Instance.positionInsideLow = new Vector3(22.8f, -53.72f, -2.21f);
        GlobalController.Instance.positionRoof = new Vector3(43.12f, 48.5f);
        GlobalController.Instance.positionStorageUp = new Vector3(-4.12f, 26.31f);
        GlobalController.Instance.positionStorageMiddle = new Vector3(43.7f, 22.17f);
        GlobalController.Instance.positionPrisonBeg = new Vector3(43.7f, -13.01f);
        GlobalController.Instance.positionPrisonEnd = new Vector3(37.21f, -44.18f);
        GlobalController.Instance.inverseTimeActive = false;
    }

    public void LoadIntroScene()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("IntroductionScene"));
    }
    public void LoadTitleScene()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("IntroductionScene"));
    }

    public void LoadLevel1()
    {
        //SceneManager.LoadScene("0.Afueras de la Torre");
        //Estos tres le dan los valores iniciales al jugador
        GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.Afueras de la Torre"));
        GlobalController.Instance.maxHp = 5;
        GlobalController.Instance.hp = GlobalController.Instance.maxHp;
        GlobalController.Instance.cooldown = 100;
        GlobalController.Instance.maxpotions = 3;
        GlobalController.Instance.disp_potions = 3;
        GlobalController.Instance.fromBeginning = true;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionOutside;
        Shortcuts.Instance.numScene = 1;

    }

    public void LoadBCOutsideLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;
         */

        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionOutsideBC;
        GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.Afueras de la Torre"));

    }

    public void LoadACOutsideLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;
         */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionOutsideAC;
        GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.Afueras de la Torre"));
    }

    public void LoadInsideBegLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideBeg;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));
    }

    public void LoadCaveLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionCaveBeg;
        GlobalController.Instance.actualLevel = GlobalController.Level.CAVE;
        //LoadingScreenScript.Instance.StartRutine();
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.5 Cueva"));
        //SceneManager.LoadScene("LoadingScreen");
    }

    public void LoadCaveEndLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionCaveEnd;
        GlobalController.Instance.actualLevel = GlobalController.Level.CAVE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("0.5 Cueva"));
    }

    public void LoadInsideUpLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideUp;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));
    }

    public void LoadInsideMidLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideMid;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));

    }
    public void LoadInsideUpSTLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideUpST;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));
    }

    public void LoadInsideMidSTLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideMidST;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));
    }
    public void LoadInsideDownLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideLow;
        GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1. Dentro del Castillo"));
    }
    public void LoadRoofLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualLevel = GlobalController.Level.ROOF;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionRoof;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 BossFight"));
    }
    public void LoadStorageUpLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualLevel = GlobalController.Level.STORAGE;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionStorageUp;

        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
    }
    public void LoadStorageMiddleLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualLevel = GlobalController.Level.STORAGE;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionStorageMiddle;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
    }
    public void LoadPrisonBegLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
           GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
       else
           GlobalController.Instance.cooldown = 100;
        */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionPrisonBeg;
        GlobalController.Instance.actualLevel = GlobalController.Level.PRISON;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
    }
    public void LoadPrisonEndLevel()
    {
        GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
        /*if (shadow.GetComponent<InverseTime>().count < 100)
            GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
        else
            GlobalController.Instance.cooldown = 100;
         */
        GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        GlobalController.Instance.actualPos = GlobalController.Instance.positionPrisonEnd;
        GlobalController.Instance.actualLevel = GlobalController.Level.PRISON;
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("1.5 Tejado y Prision"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");

        if(shadow != null)
        {
            if (GlobalController.Instance.inverseTimeActive == false)
            {
                shadow.GetComponent<SpriteRenderer>().enabled = false;
                shadow.GetComponent<InverseTime>().enabled = false;
            }
            else
            {
                shadow.GetComponent<SpriteRenderer>().enabled = true;
                shadow.GetComponent<InverseTime>().enabled = true;
            }
        }
            
    }
}
