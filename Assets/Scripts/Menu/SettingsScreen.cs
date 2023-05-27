using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] Button confirmButton;

    [Header("Back button properties")]
    [SerializeField] Button backButton;
    [SerializeField] GameObject previousPage;

    private void Start()
    {
        backButton.onClick.AddListener(delegate { DisplayPage(previousPage); });
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
