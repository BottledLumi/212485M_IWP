using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open;
    [HideInInspector] public Door linkedDoor = null;
    public enum DIRECTION
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    [SerializeField] DIRECTION direction;
    public DIRECTION getDirection()
    {
        return direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        Vector3 newPos = linkedDoor.transform.Find("Door").position;
        switch (direction)
        {
            case DIRECTION.LEFT:
                newPos.x -= 3;
                break;
            case DIRECTION.RIGHT:
                newPos.x += 3;
                break;
            case DIRECTION.UP:
                newPos.y += 3;
                break;
            case DIRECTION.DOWN:
                newPos.y -= 3;
                break;
        }
        collision.gameObject.transform.position = newPos;
    }
}
