using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcuts : MonoBehaviour
{
    public static Shortcuts Instance;
    // Start is called before the first frame update
    void Start()
    {
        
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

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.Alpha1))
            GlobalController.Instance.invencible = true;

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.Alpha2))
            GlobalController.Instance.invencible = false;
    }
}
