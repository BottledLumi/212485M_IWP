using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hoverHeight = 1.0f; // The maximum height of the hover
    public float hoverSpeed = 1.0f; // The speed of the hover

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the vertical position offset using a sine wave
        float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the offset to the object's position
        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
    }
}