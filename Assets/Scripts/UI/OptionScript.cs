using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionScript : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject SoundText;
    [SerializeField] private GameObject MusicText;

    [SerializeField] private Scrollbar soundScrollbar;
    [SerializeField] private Scrollbar musicScrollbar;
    public static OptionScript Instance;

    // Start is called before the first frame update
    void Start()
    {
        SoundText = GameObject.Find("Sound%Text");
        MusicText = GameObject.Find("Music%Text");
        menu = GameObject.Find("OPTMenu");
        menu.SetActive(false);
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
    public void ActivateMenu()
    {
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SoundText.GetComponent<TextMeshProUGUI>().text = Mathf.Round(soundScrollbar.value*100).ToString() + "%";
        MusicText.GetComponent<TextMeshProUGUI>().text = Mathf.Round(musicScrollbar.value*100).ToString() + "%";

        GlobalController.Instance.soundVolume = soundScrollbar.value;
        GlobalController.Instance.musicVolume = musicScrollbar.value;

        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().sortingLayerName = "Player";
        GetComponent<Canvas>().sortingOrder = 23;
    }
}
