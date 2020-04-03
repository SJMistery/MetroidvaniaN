using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    private AudioSource musica;
    public Collider2D doorBoss;

    // Start is called before the first frame update
    void Start()
    {
        musica = GameObject.Find("Musica").GetComponent<AudioSource>();
        doorBoss = GameObject.Find("TPpoint Up").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalController.Instance.bossDeafeted && GlobalController.Instance.actualLevel == GlobalController.Level.ROOF)
        {
            musica.Stop();
            doorBoss.enabled = true;
            Destroy(GameObject.Find("Slider").gameObject);

        }
        else if (!GlobalController.Instance.bossDeafeted && GlobalController.Instance.actualLevel == GlobalController.Level.ROOF)
        {
            doorBoss.enabled = false;
        }

    }
}
