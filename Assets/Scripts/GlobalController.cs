using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    public enum Level {OUTSIDE, INSIDE, ROOF, PRISON };

    public int maxHp;//maximo vida
    public int hp;//vida de personaje que le queda
    public int cooldown;
    public int maxpotions;//pociones disponibles
    public int disp_potions;//pociones disponibles
    public Vector3 positionOutside;
    public Vector3 positionInsideBeg;
    public Vector3 positionInsideUp;
    public Vector3 positionInsideMid;
    public Vector3 positionInsideLow;
    public Vector3 positionRoof;
    public Vector3 positionPrisonBeg;
    public Vector3 positionPrisonEnd;
    public Vector3 actualPos;
    public Level actualLevel;
    public bool fromBeginning = false;
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
