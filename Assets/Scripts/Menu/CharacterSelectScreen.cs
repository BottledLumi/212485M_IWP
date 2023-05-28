using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectScreen : MonoBehaviour
{
    [Header("Select button properties")]
    [SerializeField] Button selectButton;

    [Header("Back button properties")]
    [SerializeField] Button backButton;
    [SerializeField] GameObject previousPage;

    private void Start()
    {
        //Assign the buttons
        selectButton.onClick.AddListener(delegate { LoadScene(); });
        backButton.onClick.AddListener(delegate { DisplayPage(previousPage); });
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);  //Load game scene as single scene
    }

    private void DisplayPage(GameObject page)
    {
        if (page)
        {
            page.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
