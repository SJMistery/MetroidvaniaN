using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RespawnPlayer : MonoBehaviour
{
    private int fullHP = 25;
    public Image barImage;
    public Color barColor = Color.red;
    public Gradient barGradient = new Gradient();
    public float _currentFraction = 1.0f;
    public TextMeshProUGUI barText;
    float targetFill = 0.0f;
    float _maxValue = 25.0f;


    public Vector3 RespawnPoint;
    public int LifeBar;
    public bool respawnReset;
    public GameObject shadowReset;
    // Start is called before the first frame update
    void Start()
    {
        respawnReset = false;
        RespawnPoint = transform.position;
        LifeBar = 25;
        fullHP = LifeBar;

        barImage = GameObject.Find("Green_Bar").GetComponent<Image>();
        barText = GameObject.Find("Life_Bar_Text").GetComponent<TextMeshProUGUI>();

    }

    public float GetCurrentFraction
    {
        get
        {
            return _currentFraction;
        }
    }
    private void UpdateBarFill(float currentValue, float maxValue)
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
        barImage.fillAmount = targetFill;
    }

    private void UpdateBarText(float currentValue, float maxValue)
    {
        barText.text = currentValue + "/" + maxValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("collided");
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("damaged");
            LifeBar -= 1;
            if (LifeBar <= 0)
            {
                //Debug.Log("killed");
                transform.position = RespawnPoint;
                shadowReset.transform.position = RespawnPoint;
                respawnReset = true;
                LifeBar = 25;
            }
        }
        else if (other.gameObject.tag == "DeathTrap")
        {
            // Debug.Log("instakilled");
            transform.position = RespawnPoint;
            shadowReset.transform.position = RespawnPoint;
            respawnReset = true;
            LifeBar = 25;
        }
        else if (other.gameObject.tag == "SavePoint")
        {
            // Debug.Log("saved");
            RespawnPoint = other.gameObject.transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        respawnReset = false;
        UpdateBarFill(LifeBar, fullHP);
        UpdateBarText(LifeBar, fullHP);
    }
}

