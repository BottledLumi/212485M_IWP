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
    private List<GameObject> enemies = new List<GameObject>();
    private List<Door> doors = new List<Door>();
    ROOM_STATUS status;

    public void AddDoor(Door door)
    {
        doors.Add(door);
    }    
}
