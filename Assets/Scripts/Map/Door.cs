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
        Debug.Log("collision");
        if (!collision.gameObject.CompareTag("Player"))
            return;
        Debug.Log("player collision");
        switch (direction)
        {
            case DIRECTION.LEFT:
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x - 10, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                break;
            case DIRECTION.RIGHT:
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + 10, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                break;
            case DIRECTION.UP:
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y+10, collision.gameObject.transform.position.z);
                break;
            case DIRECTION.DOWN:
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y-10, collision.gameObject.transform.position.z);
                break;
        }
    }
}
