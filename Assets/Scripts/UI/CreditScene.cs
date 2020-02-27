using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void LoadTitleScene()
    {
        LoadingScreenScript.Instance.Show(SceneManager.LoadSceneAsync("TitleScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
