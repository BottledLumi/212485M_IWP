using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] GameObject mapManagerGO;
    MapManager mapManager;

    public Image imagePrefab; // Reference to the UI Image prefab
    public Sprite spriteToInstantiate; // Reference to the sprite you want to instantiate

    private void Start()
    {
        // Instantiate the UI Image prefab
        Image image = Instantiate(imagePrefab, transform);

        // Set the sprite
        image.sprite = spriteToInstantiate;
    }
}
