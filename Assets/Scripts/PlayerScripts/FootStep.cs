using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{

    CharacterController2D_Mod characterController;
    Rigidbody2D playerBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController2D_Mod>();
        playerBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.m_Grounded && !audioSource.isPlaying)
        {
            if (playerBody.velocity.x < -2f || playerBody.velocity.x > 2f)
            {
                audioSource.volume = Random.Range(0.1f, GlobalController.Instance.soundVolume);
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.Play();
            }
        }
    }
}
