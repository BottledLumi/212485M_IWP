using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [SerializeField] GameObject inventoryDisplay, minimapDisplay;

    float timer = 0;
    bool minimapActive;

    private void Start()
    {
        minimapActive = minimapDisplay.activeInHierarchy;
    }
    private void Update()
    {
        // 
        if (Input.GetKey(KeyCode.Tab))
        {
            timer += Time.deltaTime;
            if (timer > 0.2f)
            {
                inventoryDisplay.SetActive(true);
                minimapDisplay.SetActive(false);
            }

        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (timer > 0.2f)
            {
                inventoryDisplay.SetActive(false);
                minimapDisplay.SetActive(minimapActive);
            }
            else
            {
                minimapDisplay.SetActive(!minimapDisplay.activeInHierarchy);
                minimapActive = minimapDisplay.activeInHierarchy;
            }

            timer = 0;
        }
    }
}
