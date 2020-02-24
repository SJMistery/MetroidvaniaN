using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Shortcuts : MonoBehaviour
{
    public static Shortcuts Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shortcutMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject cheatAdvisor;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Camera cam;
    public enum PartOfLevel { NONE, START, UP, SUP, SMIDDLE, MIDDLE, DOWN, BEGINNING, END, LAST};

    public int numScene = 0;
    public PartOfLevel partOfLevel = PartOfLevel.NONE;
    [SerializeField] private bool alredyTP;
    [SerializeField] private GameObject SceneText;
    [SerializeField] private GameObject PartofScene;

    [SerializeField] private GameObject[] pages = new GameObject[3];
    [SerializeField] private int numPage = 0;
    [SerializeField] private bool draw = true;
    [SerializeField] private float distance = 0.2f; //distancia a la que el raycast detecta la escalera. ES VERTICAL, tiene que estar muy bajo.
    public LayerMask buttons;
    [SerializeField] private string nameOfButton;
    [SerializeField] private RaycastHit2D buttoninfo;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        menu = GameObject.Find("Pause Menu").GetComponent<MenuController>().Menu;
        shortcutMenu = GameObject.FindGameObjectWithTag("SCMenu");
        SceneText = GameObject.Find("SceneText");
        PartofScene = GameObject.Find("PartSceneText");
        pages = GameObject.FindGameObjectsWithTag("Page");
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        cursor = GameObject.FindGameObjectWithTag("cursor");
        cheatAdvisor = GameObject.FindGameObjectWithTag("CheatAdvisor");
        shortcutMenu.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        switch(GlobalController.Instance.actualLevel)
        {
            case GlobalController.Level.TITLE:
                numScene = 0;
                break;
            case GlobalController.Level.OUTSIDE:
                numScene = 1;
                break;
            case GlobalController.Level.CAVE:
                numScene = 2;
                break;
            case GlobalController.Level.INSIDE:
                numScene = 3;
                break;
            case GlobalController.Level.ROOF:
                numScene = 5;
                break;
            case GlobalController.Level.PRISON:
                numScene = 5;
                break;
            case GlobalController.Level.STORAGE:
                numScene = 4;
                break;
        }
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

    public void addToNumScene()
    {
        if (numScene <= 5)
            numScene++;
    }

    public void deductToNumScene()
    {
        if (numScene >= 0)
            numScene--;
    }

    public void addToPartOSC()
    {
        if (partOfLevel < PartOfLevel.LAST)
            partOfLevel++;
    }

    public void deductToPartOSC()
    {
        if ((int)partOfLevel > 0)
            partOfLevel--;
    }

    public void hideSCMenu()
    {
        shortcutMenu.SetActive(false);
    }

    public void InvencibilityON()
    {
        GlobalController.Instance.invencible = true;
    }

    public void InvencibilityOFF()
    {
        GlobalController.Instance.invencible = false;
    }

    public void InverseTimeON()
    {
        GlobalController.Instance.inverseTimeActive = true;
    }

    public void InverseTimeOFF()
    {
        GlobalController.Instance.inverseTimeActive = false;
    }

    public void AutoInputScene()
    {
        switch (buttoninfo.collider.name)
        {
            case "Title scene":
                numScene = 0;
                partOfLevel = PartOfLevel.NONE;
                break;
            case "Outside - Start":
                numScene = 1;
                partOfLevel = PartOfLevel.START;
                break;
            case "Outside - BC":
                numScene = 1;
                partOfLevel = PartOfLevel.BEGINNING;
                break;
            case "Outside - AC":
                numScene = 1;
                partOfLevel = PartOfLevel.END;
                break;
            case "Cave - Start":
                numScene = 2;
                partOfLevel = PartOfLevel.START;
                break;
            case "Cave - End":
                numScene = 2;
                partOfLevel = PartOfLevel.END;
                break;
            case "Interior - Start":
                numScene = 3;
                partOfLevel = PartOfLevel.START;
                break;
            case "Interior - Up":
                numScene = 3;
                partOfLevel = PartOfLevel.UP;
                break;
            case "Interior - Middle":
                numScene = 3;
                partOfLevel = PartOfLevel.MIDDLE;
                break;
            case "Interior - Parte Baja":
                numScene = 3;
                partOfLevel = PartOfLevel.DOWN;
                break;
            case "Interior - SU":
                numScene = 3;
                partOfLevel = PartOfLevel.SUP;
                break;
            case "Interior - SM":
                numScene = 3;
                partOfLevel = PartOfLevel.SMIDDLE;
                break;
            case "Roof":
                numScene = 5;
                partOfLevel = PartOfLevel.UP;
                break;
            case "Prison - Start":
                numScene = 5;
                partOfLevel = PartOfLevel.START;
                break;
            case "Prison - End":
                numScene = 5;
                partOfLevel = PartOfLevel.END;
                break;
            case "Storage - Up":
                numScene = 4;
                partOfLevel = PartOfLevel.UP;
                break;
            case "Storage - Middle":
                numScene = 4;
                partOfLevel = PartOfLevel.MIDDLE;
                break;
            case "NONE":
                partOfLevel = PartOfLevel.NONE;
                break;
        }
    }

    public void nextPage()
    {
        if (numPage < 2)
        {
            pages[numPage].SetActive(false);
            numPage++;
            pages[numPage].SetActive(true);
        }
        else
        {
            pages[numPage].SetActive(false);
            numPage = 0;
            pages[numPage].SetActive(true);
        }
    }
    public void prevPage()
    {
        if (numPage > 0)
        {
            pages[numPage].SetActive(false);
            numPage--;
            pages[numPage].SetActive(true);
        }
        else
        {
            pages[numPage].SetActive(false);
            numPage = 2;
            pages[numPage].SetActive(true);
        }
    }

    public void LoadSaidScene()
    {
        if (numScene == 0 && alredyTP == false)
        {
            GlobalController.Instance.actualLevel = GlobalController.Level.TITLE;
            SceneManager.LoadScene("TitleScene");
        }

        if(numScene == 1 && partOfLevel == PartOfLevel.START && alredyTP==false)
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionOutside;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.OUTSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
                SceneManager.LoadScene("0.Afueras de la Torre");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionOutside;
            }
        }

        if (numScene == 1 && partOfLevel == PartOfLevel.BEGINNING && alredyTP == false)//Para salir desde el principio de la cueva
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionOutsideBC;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.OUTSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
                SceneManager.LoadScene("0.Afueras de la Torre");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionOutsideBC;
            }
            alredyTP = true;
        }

        if (numScene == 1 && partOfLevel == PartOfLevel.END && alredyTP == false)//Para ir a Afuera Torre despues de la cueva
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionOutsideAC;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.OUTSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.OUTSIDE;
                SceneManager.LoadScene("0.Afueras de la Torre");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionOutsideAC;
            }
            alredyTP = true;
        }

        if (numScene == 2 && partOfLevel == PartOfLevel.START && alredyTP == false)//Para ir al principio de la cueva
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionCaveBeg;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.CAVE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.CAVE;
                SceneManager.LoadScene("0.5 Cueva");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionCaveBeg;
            }
            alredyTP = true;
        }

        if (numScene == 2 && partOfLevel == PartOfLevel.END && alredyTP == false)//Para ir al final de la cueva
        {
            if (GlobalController.Instance.actualLevel != GlobalController.Level.CAVE)
            {
                GlobalController.Instance.actualPos = GlobalController.Instance.positionCaveEnd;
                GlobalController.Instance.actualLevel = GlobalController.Level.CAVE;
                SceneManager.LoadScene("0.5 Cueva");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionCaveEnd;
            }
            alredyTP = true;
        }

        if (numScene == 3 && partOfLevel == PartOfLevel.BEGINNING && alredyTP == false)//Para ir al principio del interior de la torre
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideBeg;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
                SceneManager.LoadScene("1. Dentro del Castillo");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionInsideBeg;
            }
            alredyTP = true;
        }

        if (numScene == 3 && partOfLevel == PartOfLevel.SUP && alredyTP == false)//Para ir a la salida del almacen parte alta
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideUpST;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
                SceneManager.LoadScene("1. Dentro del Castillo");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionInsideUpST;
            }
            alredyTP = true;
        }

        if (numScene == 3 && partOfLevel == PartOfLevel.SMIDDLE && alredyTP == false)
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideMidST;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
                SceneManager.LoadScene("1. Dentro del Castillo");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionInsideMidST;
            }
            alredyTP = true;
        }

        if (partOfLevel == PartOfLevel.DOWN && numScene == 3 && alredyTP == false)
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideLow;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
                SceneManager.LoadScene("1. Dentro del Castillo");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionInsideLow;
            }
            alredyTP = true;
        }

        if (partOfLevel == PartOfLevel.UP)//Parte alta del nivel
        {
            if (numScene == 3 && alredyTP == false)//entrada del tejado
            {
                GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideUp;
                if (GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE)
                {
                    GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
                    SceneManager.LoadScene("1. Dentro del Castillo");
                }
                else
                {
                    player.transform.position = GlobalController.Instance.positionInsideUp;
                }
                alredyTP = true;
            }
            if (numScene == 4 && alredyTP == false)//almacen alto
            {
                GlobalController.Instance.actualPos = GlobalController.Instance.positionStorageUp;
                if (GlobalController.Instance.actualLevel != GlobalController.Level.STORAGE)
                {
                    GlobalController.Instance.actualLevel = GlobalController.Level.STORAGE;
                    SceneManager.LoadScene("1.5 Tejado y Prision");
                }
                else
                {
                    player.transform.position = GlobalController.Instance.positionStorageUp;
                }
                alredyTP = true;

            }
            if (numScene == 5 && alredyTP == false)//tejado
            {
                GlobalController.Instance.actualPos = GlobalController.Instance.positionRoof;
                if (GlobalController.Instance.actualLevel != GlobalController.Level.ROOF)
                {
                    GlobalController.Instance.actualLevel = GlobalController.Level.ROOF;
                    SceneManager.LoadScene("1.5 Tejado y Prision");
                }
                else
                {
                    player.transform.position = GlobalController.Instance.positionRoof;
                }
                alredyTP = true;
            }
        }

        if (partOfLevel == PartOfLevel.MIDDLE)
        {
            if (numScene == 3 && alredyTP == false)//Interior castillo parte media
            {
                GlobalController.Instance.actualPos = GlobalController.Instance.positionInsideMid;
                if (GlobalController.Instance.actualLevel != GlobalController.Level.INSIDE)
                {
                    GlobalController.Instance.actualLevel = GlobalController.Level.INSIDE;
                    SceneManager.LoadScene("1. Dentro del Castillo");
                }
                else
                {
                    player.transform.position = GlobalController.Instance.positionInsideMid;
                }
                alredyTP = true;
            }
            if (numScene == 4 && alredyTP == false)
            {
                GlobalController.Instance.actualPos = GlobalController.Instance.positionStorageMiddle;
                if (GlobalController.Instance.actualLevel != GlobalController.Level.STORAGE)
                {
                    GlobalController.Instance.actualLevel = GlobalController.Level.STORAGE;
                    SceneManager.LoadScene("1.5 Tejado y Prision");
                }
                else
                {
                    player.transform.position = GlobalController.Instance.positionStorageMiddle;
                }
                alredyTP = true;
            }
        }

        if (numScene == 5 && (partOfLevel == PartOfLevel.BEGINNING || partOfLevel == PartOfLevel.START) && alredyTP == false)//A la Prision
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionPrisonBeg;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.PRISON)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.PRISON;
                SceneManager.LoadScene("1.5 Tejado y Prision");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionPrisonBeg;
            }
            alredyTP = true;
        }

        if (numScene == 5 && partOfLevel == PartOfLevel.END && alredyTP == false)
        {
            GlobalController.Instance.actualPos = GlobalController.Instance.positionPrisonEnd;
            if (GlobalController.Instance.actualLevel != GlobalController.Level.PRISON)
            {
                GlobalController.Instance.actualLevel = GlobalController.Level.PRISON;
                SceneManager.LoadScene("1.5 Tejado y Prision");
            }
            else
            {
                player.transform.position = GlobalController.Instance.positionPrisonEnd;
            }
            alredyTP = true;
        }

        //Estos tres le dan los valores iniciales al jugador
        if (GlobalController.Instance.fromBeginning == false)
        {
            GlobalController.Instance.maxHp = 5;
            GlobalController.Instance.hp = 5;
            GlobalController.Instance.cooldown = 100;
            GlobalController.Instance.maxpotions = 3;
            GlobalController.Instance.disp_potions = 3;
            GlobalController.Instance.fromBeginning = true;
        }
        else
        {
            GlobalController.Instance.hp = player.GetComponent<CharacterController2D_Mod>().LifeBar;//Se guarda la vida que tiene el jugador
            /*if (shadow.GetComponent<InverseTime>().count < 100)
                GlobalController.Instance.cooldown = shadow.GetComponent<InverseTime>().count;
            else
                GlobalController.Instance.cooldown = 100;
             */
            GlobalController.Instance.disp_potions = player.GetComponent<CharacterController2D_Mod>().healsAvalible;
        }
        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        shortcutMenu.SetActive(false);
        alredyTP = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().sortingLayerName = "Player";
        GetComponent<Canvas>().sortingOrder = 23;

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.Alpha1))
            GlobalController.Instance.invencible = true;

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.Alpha2))
            GlobalController.Instance.invencible = false;

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.M))
            shortcutMenu.SetActive(true);

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.A))
            GlobalController.Instance.inverseTimeActive = true;

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.D))
            GlobalController.Instance.inverseTimeActive = false;


        if ((menu.activeSelf == true && menu != null) || shortcutMenu.activeSelf == true || numScene == 0)
        {
            cursor.SetActive(true);
        }
        else
        {
            cursor.SetActive(false);
        }

        if (cursor.activeSelf == true)
        {
            cursor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 11, Input.mousePosition.y - 11, 87.54097f));
        }

        if (shortcutMenu.activeSelf == true)
        {
            SceneText = GameObject.Find("SceneText");
            PartofScene = GameObject.Find("PartSceneText");
            cursor = GameObject.FindGameObjectWithTag("cursor");

            switch (numScene)
            {
                case 0:
                    SceneText.GetComponent<TextMeshProUGUI>().text = "Title Scene";
                    break;
                case 1:
                    SceneText.GetComponent<TextMeshProUGUI>().text = "Afueras de la Torre";
                    break;
                case 2:
                    SceneText.GetComponent<TextMeshProUGUI>().text = "Cueva";
                    break;
                case 3:
                    SceneText.GetComponent<TextMeshProUGUI>().text = "Dentro del Castillo";
                    break;
                case 4:
                    SceneText.GetComponent<TextMeshProUGUI>().text = "Almacén";
                    break;
                case 5:
                    SceneText.GetComponent<TextMeshProUGUI>().text = "Tejado y Prisión";
                    break;
            }

            PartofScene.GetComponent<TextMeshProUGUI>().text = partOfLevel.ToString();

            buttoninfo = Physics2D.Raycast(cursor.transform.position, Vector2.up, distance, buttons);

            if (buttoninfo.collider != null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * buttoninfo.distance, Color.yellow);
                Debug.Log("Did Hit");
                nameOfButton = buttoninfo.collider.name;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
                nameOfButton = "NONE";
            }

        }

        cheatAdvisor.SetActive(GlobalController.Instance.invencible);

        Cursor.visible = false;


    }

    private void OnDrawGizmos()
    {
        if (draw == true)
        {
            Gizmos.color = Color.yellow;
            if(cursor != null)
            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(cursor.transform.position.x, cursor.transform.position.y + distance));
        }
    }
}
