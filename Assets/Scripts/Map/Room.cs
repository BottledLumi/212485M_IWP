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
    private List<GameObject> enemies;
    ROOM_STATUS status;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
