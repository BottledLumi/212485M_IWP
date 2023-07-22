using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public struct ROOM_STATUS
    {
        private int status;

        private const int ACTIVE_MASK = 1;     // Bit 0: Active flag
        private const int EXPLORED_MASK = 2;   // Bit 1: Explored flag
        private const int CLEARED_MASK = 4;    // Bit 2: Cleared flag

        public bool active
        {
            get { return (status & ACTIVE_MASK) != 0; }
            set
            {
                if (value)
                    status |= ACTIVE_MASK;
                else
                    status &= ~ACTIVE_MASK;
            }
        }
        public bool explored
        {
            get { return (status & EXPLORED_MASK) != 0; }
            set
            {
                if (value)
                    status |= EXPLORED_MASK;
                else
                    status &= ~EXPLORED_MASK;
            }
        }
        public bool cleared
        {
            get { return (status & CLEARED_MASK) != 0; }
            set
            {
                if (value)
                    status |= CLEARED_MASK;
                else
                    status &= ~CLEARED_MASK;
            }
        }
    }

    [HideInInspector] public uint width, height;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private List<Door> doors = new List<Door>();
    ROOM_STATUS status;
    public ROOM_STATUS Status
    {
        get { return status; }
    }
    public void AddDoor(Door door)
    {
        doors.Add(door);
    }

    // Player has entered the room
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        OnRoomEntered();
        //Debug.Log("Player has entered room");
        MapManager.Instance.RoomEntered(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        status.active = false;
        //Debug.Log("Player has left the room");
    }

    private void Update() // Temporary non-event based check for cleared enemies
    {
        if (!EnemiesActive() && status.active)
            RoomCleared();
    }

    private void SetEnemiesActive(bool _active)
    {
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<Enemy>().IsDead)
                enemy.SetActive(_active);
        }
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

    void OpenDoors()
    {
        foreach (Door door in doors)
            door.SetOpen(true);
    }

    private void OnRoomEntered()
    {
        status.active = true; status.explored = true;
        SetEnemiesActive(true);
        if (EnemiesActive())
        {
            PathFindManager.GeneratePath(gameObject, width,height);
            CloseDoors();
        }
    }

    void RoomCleared()
    {
        if (status.cleared)
            return;
        status.cleared = true; GameStateManager.instance.roomsCleared++;

        // If the room has room drops, chance an item drop
        RoomDrops roomDrops = GetComponent<RoomDrops>();
        if (roomDrops)
            roomDrops.ChanceDrop();

        AstarPath astarPath = GetComponent<AstarPath>();
        if (astarPath)
            Destroy(astarPath);
        OpenDoors();
    }
}
