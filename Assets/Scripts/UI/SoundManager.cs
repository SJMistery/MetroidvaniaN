using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioMixer mixer;
    [SerializeField]
    private AudioMixerSnapshot pausedSnapshot;
    [SerializeField]
    private AudioMixerSnapshot resumeSnapshot;
    [SerializeField] private string OutputMixer;
    [SerializeField] private bool changed;
    [SerializeField]private AudioSource BackgroundMusic;
    public int startingPitch = 4;
    public int timeToDecrease = 5;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        if (BackgroundMusic == null)
            BackgroundMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        
    }

    private void changeToPauseSnapshot()
    {
        pausedSnapshot.TransitionTo(.01f);
        changed = true;
    }

    private void changeToResumeSnapshot()
    {
        resumeSnapshot.TransitionTo(.01f);
        changed = false;
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
        time = Time.timeScale;
        BackgroundMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        OutputMixer = "Music";
        BackgroundMusic.outputAudioMixerGroup = mixer.FindMatchingGroups(OutputMixer)[0];

        if (BackgroundMusic != null)
            BackgroundMusic.volume = GlobalController.Instance.musicVolume;

        if (Time.timeScale == 0)
        {
            if (!changed)
                changeToPauseSnapshot();

        }
        else
        {
            if (changed)
                changeToResumeSnapshot();


            if (GlobalController.Instance.moveIT == true)
            {
                BackgroundMusic.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
            }
            else
            {
                BackgroundMusic.pitch = 1;
            }
        }
    }
}
