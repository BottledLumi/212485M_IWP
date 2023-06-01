using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public struct ROOM_STATUS
    {
        public bool active; // Player is in the room
        public bool explored; // Player has entered the room
        public bool cleared; // Room has been cleared
    }
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private List<Door> doors = new List<Door>();
    ROOM_STATUS status;
    public void AddDoor(Door door)
    {
        doors.Add(door);
    }

    // Player has entered the room
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        status.active = true; status.explored = true;
        if (EnemiesActive())
            CloseDoors();
        Debug.Log("Player has entered room");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        status.active = false;
        Debug.Log("Player has left the room");
    }

    private void Update() // Temporary non-event based check for cleared enemies
    {
        if (!EnemiesActive())
            RoomCleared();
    }

    bool EnemiesActive()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
                return true;
        }
        return false;
    }

    void CloseDoors()
    {
        foreach (Door door in doors)
            door.SetOpen(false);
    }

    void RoomCleared()
    {
        status.cleared = true;
        foreach (Door door in doors)
            door.SetOpen(true);
    }
}
