using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool open = true;
    [HideInInspector] public Door linkedDoor = null;
    [SerializeField] Animator animator;
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

    public void SetOpen(bool _open)
    {
        if (_open)
        {
            animator.SetTrigger("Open");
            animator.ResetTrigger("Close");
        }
        else
        {
            animator.SetTrigger("Close");
            animator.ResetTrigger("Open");
        }
        open = _open;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !open)
            return;
        Vector3 newPos = linkedDoor.transform.position;
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
