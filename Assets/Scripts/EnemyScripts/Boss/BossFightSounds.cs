using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightSounds : MonoBehaviour
{
    public GameObject swordsound;
    public GameObject walksound;
    public GameObject dashsound;
    public GameObject hammerattack;
    public GameObject hammerslam;
    public GameObject skyattack;
    // Start is called before the first frame update

    public void SoundSwordAttack()
    {
        Instantiate(swordsound);
    }
    public void SoundWalk()
    {
        Instantiate(walksound);
    }
    public void SoundDash()
    {
        Instantiate(dashsound);
    }

    public void SoundHammerAttack()
    {
        Instantiate(hammerattack);
    }
    public void SoundHammerSlam()
    {
        Instantiate(hammerslam);
    }

    private void SoundSkyAttack()
    {
        Instantiate(skyattack);
    }
}
