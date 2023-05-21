using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open;
    enum DIRECTION
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    [SerializeField] DIRECTION direction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        }
    }
}
