using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [Header ("Start button properties")]
    [SerializeField] Button startButton;
    [SerializeField] GameObject startPage;

    [Header ("Settings button properties")]
    [SerializeField] Button settingButton;
    [SerializeField] GameObject settingsPage;

    [Header ("Quit button properties")]
    [SerializeField] Button quitButton;

    private void Start()
    {
        //Assign the buttons
        startButton.onClick.AddListener(delegate { DisplayPage(startPage); });
        settingButton.onClick.AddListener(delegate { DisplayPage(settingsPage); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
    }

    private void DisplayPage(GameObject page)
    {
        if (page)
        {
            page.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
