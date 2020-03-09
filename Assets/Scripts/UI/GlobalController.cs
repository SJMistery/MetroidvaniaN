using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    public enum Level {TITLE, INTRO, CREDIT, OUTSIDE, INSIDE, ROOF, PRISON, CAVE, STORAGE };

    public int maxHp;//maximo vida
    public int hp;//vida de personaje que le queda
    public int cooldown;
    public int maxpotions;//maximo pociones
    public int disp_potions;//pociones disponibles

    public float soundVolume;
    public float musicVolume;

    public bool inverseTimeActive;
    public bool invencible;
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
    public Vector3 UpDoorPos;
    public Vector3 DownDoorPos;
    public Level actualLevel;
    public bool fromBeginning = false;
    public bool doorUpActivated = false;
    public bool doorMidActivated = false;
    public bool streamEnded = false;
    public string nameOfPartLevel;
    public bool moveIT = false;//para saber si el inverse time esta activo
    public static GlobalController Instance;//esto sirve para guardar los datos de posicion del jugador

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
}
