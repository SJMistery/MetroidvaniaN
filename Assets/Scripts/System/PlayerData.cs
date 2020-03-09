using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] pos;
    public float[] color;
    public int health;
    public bool reverseTimeActive;
    public int potionsAvailable;

    public float soundVolume;
    public float musicVolume;
    public GlobalController.Level level;
    public bool fromBeginning;
    public PlayerData(CharacterController2D_Mod player)
    {
        health = player.LifeBar;
        reverseTimeActive = player.bobinaDelTiempo;
        potionsAvailable = player.healsAvalible;
        pos = new float[3];

        pos[0] = player.transform.position.x;
        pos[1] = player.transform.position.y;
        pos[2] = player.transform.position.z;

        color = new float[3];

        color[0] = player.gameObject.GetComponent<SpriteRenderer>().color.r;
        color[1] = player.gameObject.GetComponent<SpriteRenderer>().color.g;
        color[2] = player.gameObject.GetComponent<SpriteRenderer>().color.b;

        soundVolume = GlobalController.Instance.soundVolume;
        musicVolume = GlobalController.Instance.musicVolume;
        level = GlobalController.Instance.actualLevel;
        fromBeginning = GlobalController.Instance.fromBeginning;
    }
}
