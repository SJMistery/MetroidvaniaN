using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public static LoadingScreenScript Instance;
    // Make sure the loading screen shows for at least 1 second:
    private const float MIN_TIME_TO_SHOW = 1f;
    // The reference to the current loading operation running in the background:
    [SerializeField] private AsyncOperation currentLoadingOperation;
    // A flag to tell whether a scene is being loaded or not:
    [SerializeField] private bool isLoading;
    // The rect transform of the bar fill game object:
    [SerializeField]
    private Image barFillRectTransform;
    // Initialize as the initial local scale of the bar fill game object. Used to cache the Y-value (just in case):
    [SerializeField] private float barFillLocalScale;
    // The text that shows how much has been loaded:
    [SerializeField]
    private TextMeshProUGUI percentLoadedText;
    // The elapsed time since the new scene started loading:
    [SerializeField] private float timeElapsed;
    // Set to true to hide the progress bar:
    [SerializeField]
    private bool hideProgressBar;
    // Set to true to hide the percentage text:
    [SerializeField]
    private bool hidePercentageText;
    private void Awake()
    {
        // Singleton logic:
        if (Instance == null)
        {
            Instance = this;
            // Don't destroy the loading screen while switching scenes:
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Configure();
        Hide();
    }
    private void Configure()
    {
        // Save the bar fill's initial local scale:
        barFillLocalScale = barFillRectTransform.fillAmount;
        // Enable/disable the progress bar based on configuration:
        barFillRectTransform.transform.parent.gameObject.SetActive(!hideProgressBar);
        // Enable/disable the percentage text based on configuration:
        percentLoadedText.gameObject.SetActive(!hidePercentageText);
    }
    private void Update()
    {
        if (isLoading)
        {
            // Get the progress and update the UI. Goes from 0 (start) to 1 (end):
            SetProgress(currentLoadingOperation.progress);
            // If the loading is complete, hide the loading screen:
            if (currentLoadingOperation.isDone)
            {
                Hide();
            }
            else
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= MIN_TIME_TO_SHOW)
                {
                    // The loading screen has been showing for the minimum time required.
                    // Allow the loading operation to formally finish:
                    currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }
    // Updates the UI based on the progress:
    private void SetProgress(float progress)
    {
        // Update the fill's scale based on how far the game has loaded:
        barFillLocalScale = progress;
        // Update the rect transform:
        barFillRectTransform.fillAmount = barFillLocalScale;
        // Set the percent loaded text:
        percentLoadedText.text = Mathf.CeilToInt(progress * 100).ToString() + "%";
    }
    // Call this to show the loading screen.
    // We can determine the loading's progress when needed from the AsyncOperation param:
    public void Show(AsyncOperation loadingOperation)
    {
        // Enable the loading screen:
        gameObject.SetActive(true);
        // Store the reference:
        currentLoadingOperation = loadingOperation;
        // Stop the loading operation from finishing, even if it technically did:
        currentLoadingOperation.allowSceneActivation = false;
        // Reset the UI:
        SetProgress(0f);
        // Reset the time elapsed:
        timeElapsed = 0f;
        isLoading = true;
    }
    // Call this to hide it:
    public void Hide()
    {
        // Disable the loading screen:
        gameObject.SetActive(false);
        currentLoadingOperation = null;
        isLoading = false;
    }
}
