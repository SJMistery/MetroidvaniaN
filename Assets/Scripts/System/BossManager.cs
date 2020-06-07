using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    private AudioSource musica;
    public Collider2D doorBoss;
    public GameObject slider;
    private GameObject platform;
    private bool platformIsActive = true;
    private bool sliderIsActive = true;


    private void Start()
    {
        musica = GameObject.Find("Musica").GetComponent<AudioSource>();
        doorBoss = GameObject.Find("TPpoint Up").GetComponent<BoxCollider2D>();
        slider = GameObject.Find("Boss Hp Bar");
        platform = GameObject.Find("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalController.Instance.actualLevel == GlobalController.Level.ROOF) {

            if (platformIsActive == true && !GlobalController.Instance.bossPlatformSpawned)
            {
                platform.SetActive(false);
                platformIsActive = false;
            }

            if (GlobalController.Instance.bossDeafeted == true)
            {
                musica.Stop();
                doorBoss.enabled = true;
                if (sliderIsActive)
                {
                    slider.SetActive(false);
                    sliderIsActive = false;
                }
            }
            else if (!GlobalController.Instance.bossDeafeted)
            {
                doorBoss.enabled = false;
            }
            if (GlobalController.Instance.bossPlatformSpawned)
            {
                platform.SetActive(true);
                platformIsActive = true;
            }
        }
    }
}
