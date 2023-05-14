using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField] float offsetValue;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frames
    void LateUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = mousePosition - player.transform.position;

        transform.position = new Vector3(player.transform.position.x + mouseDirection.x * offsetValue, player.transform.position.y + mouseDirection.y * offsetValue, transform.position.z);
    }
}
