using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField]private AudioSource BackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        if(BackgroundMusic == null)
        BackgroundMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }
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

    // Update is called once per frame
    void Update()
    {
        if (BackgroundMusic != null)
            BackgroundMusic.volume = GlobalController.Instance.musicVolume;
    }
}
