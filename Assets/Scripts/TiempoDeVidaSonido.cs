using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDeVidaSonido : MonoBehaviour
{
    public float tiempoDeVida;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject ,tiempoDeVida);
        GetComponent<AudioSource>().volume = GlobalController.Instance.soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = GlobalController.Instance.soundVolume;
    }
}
