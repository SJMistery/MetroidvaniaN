using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InverseTime : MonoBehaviour
{

    
    [SerializeField] private TextMeshProUGUI cooldnText;
    [SerializeField] private GameObject clockImage;


    public bool respawnReset;
    Queue<Vector2> trackPos;
    public Transform shadowObj;
    public GameObject playerP;
    public int count;
    public int frameRet = 180;
    public int frameCoold = 100;
    public Rigidbody2D playerBody;
    public int saveForce;

    private Image barImage; //imagen para la barra de cooldown
    private Color barColor = Color.red; //color de la barra de cooldown
    private Gradient barGradient = new Gradient();
    private float _currentFraction = 1.0f;
    float targetFill = 0.0f; //valores para hacer los calculos de la barra de cooldown.
    float _maxValue = 25.0f; //valores para hacer los calculos de la barra de cooldown.

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        respawnReset = false;
        trackPos = new Queue<Vector2>();
        Vector3 temp = playerP.transform.position;
        temp.z = -1;
        shadowObj.position = temp;
        count = GlobalController.Instance.cooldown;
        cooldnText = GameObject.Find("cooldown").GetComponent<TextMeshProUGUI>();
        cooldnText.gameObject.SetActive(false);
        clockImage = GameObject.Find("clock");
        clockImage.SetActive(false);
        barImage = GameObject.Find("Cooldown_bar").GetComponent<Image>();
    }

    private void UpdateCooldownBarFill(float currentValue, float maxValue)
    {
        // Fix the value to be a percentage.
        _currentFraction = currentValue / maxValue;

        // If the value is greater than 1 or less than 0, then fix the values to being min/max.
        if (_currentFraction < 0 || _currentFraction > 1)
            _currentFraction = _currentFraction < 0 ? 0 : 1;

        // Store the target amount of fill according to the users options.
        targetFill = _currentFraction;

        // Store the values so that other functions used can reference the maxValue.
        _maxValue = maxValue;

        // Then just apply the target fill amount.
        barImage.fillAmount = 1 - targetFill;
    } //funcion que controla como se rellena o se vacia la barra de vida.

    // Update is called once per frame
    void Update()
    {
        UpdateCooldownBarFill(count, frameCoold);

        if (respawnReset)
        {
            count = 0;
            trackPos.Clear();
            respawnReset = false;
        }
        Vector3 tempInt = playerP.transform.position;
        trackPos.Enqueue(tempInt);
        if (count < frameRet)
        {
            count++;
        }
        else
        {
            Vector3 temp = trackPos.Dequeue();
            temp.z = -1;
            shadowObj.position = temp;
        }
        if ((Input.GetKeyDown("r"))&&(count>frameCoold))
        {
            Debug.Log("recalling");
           Vector3 temp = shadowObj.position;//obtain up to 5 intervals

            temp.z = -2;
            playerP.transform.position = temp;
            trackPos.Clear();
            if (Input.GetButton("Jump")){
                playerBody.AddForce(new Vector2(0f, saveForce));
            }
            count = 0;
        }


        if (frameCoold-count > 0)
            cooldnText.gameObject.SetActive(true);
        else
            cooldnText.gameObject.SetActive(false);
        if (frameCoold-count <= 0)
            clockImage.SetActive(true);
        else
            clockImage.SetActive(false);
        cooldnText.text = count.ToString();
    }
}
