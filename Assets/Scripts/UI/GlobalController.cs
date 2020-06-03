using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{

    public enum Level {TITLE, INTRO, CREDIT, OUTSIDE, INSIDE, ROOF, PRISON, CAVE, STORAGE };

    public int maxHp = 5;//maximo vida
    public int hp = 5;//vida de personaje que le queda
    public int cooldown;
    public int maxpotions = 3;//maximo pociones
    public int disp_potions = 3;//pociones disponibles

    public float soundVolume;
    public float musicVolume;

    public bool inverseTimeActive = false;
    public bool invencible = false;
    public Vector3 positionOutside;
    public Vector3 positionOutsideBC;//Antes de entrar a la cueva
    public Vector3 positionOutsideAC;//despues de salir de la cueva
    public Vector3 positionCaveBeg;//despues de salir de la cueva
    public Vector3 positionCaveEnd;//despues de salir de la cueva
    public Vector3 positionInsideBeg;
    public Vector3 positionInsideUp;
    public Vector3 positionInsideMid;
    public Vector3 positionInsideUpST;//al salir del storage up
    public Vector3 positionInsideMidST;//al salir del storage middle
    public Vector3 positionInsideLow;
    public Vector3 positionRoof;
    public Vector3 positionStorageUp;
    public Vector3 positionStorageMiddle;
    public Vector3 positionPrisonBeg;
    public Vector3 positionPrisonEnd;
    public Vector3 actualPos;
    public Vector3 UpDoorPosCastle;
    public Vector3 DownDoorPosCastle;
    public Level actualLevel;
    public bool fromBeginning = false;
    public bool doorUpActivated = false;
    public bool doorMidActivated = false;
    public bool streamEnded = false;
    public bool stopAll = false;
    public string nameOfPartLevel;
    public bool infiniteJump = false;
    public bool moveIT = false;//para saber si el inverse time esta activo
    public static GlobalController Instance;//esto sirve para guardar los datos de posicion del jugador
    public bool bossDeafeted = false;
    public bool cutsceneActive = false;
    public bool firstCutsceneEnded = false;
    public Vector3 currentSafepoint = new Vector3(68.42f, 24.42f, -1.91f);
    public string savedLevel = "0.Afueras de la Torre";
    public bool Dead;

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
         currentSafepoint = new Vector3(68.42f, 24.42f, -1.91f);
         savedLevel = "0.Afueras de la Torre";

        if (SceneManager.GetActiveScene().name == "0.Afueras de la Torre")
        {
            actualLevel = Level.OUTSIDE;
            actualPos = new Vector3(68.42f, 24.42f, -1.91f);
        }
        else if (SceneManager.GetActiveScene().name == "0.5 Cueva")
        {
            actualLevel = Level.CAVE;
            actualPos = new Vector3(0f, -0.38f);
        }
        else if (SceneManager.GetActiveScene().name == "1. Dentro del castillo")
        {
            actualLevel = Level.INSIDE;
            actualPos = new Vector3(-3.51f, -3.755696f, -2.21f);
        }
        else if (SceneManager.GetActiveScene().name == "1.5 BossFight")
        {
            actualLevel = Level.ROOF;
            actualPos = new Vector3(43.12f, 48.5f);
        }
        else if (SceneManager.GetActiveScene().name == "1.5 Tejado y Prision")
        {
            actualLevel = Level.PRISON;
            actualPos = new Vector3(43.7f, -13.01f);
        }

    }
    private void Update()
    {
       
    }
}
