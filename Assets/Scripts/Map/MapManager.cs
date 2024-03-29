using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager instance; // Singleton
    public static MapManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Find the existing instance in the scene
                instance = FindObjectOfType<MapManager>();

                // If no instance exists, create a new one
                if (instance == null)
                {
                    GameObject mapManagerGO = new GameObject("MapManager");
                    instance = mapManagerGO.AddComponent<MapManager>();
                }
            }

            return instance;
        }
    }

    private MapGenerator mapGenerator;
    [SerializeField] private GameObject world, rooms, doors;
    [SerializeField] private GameObject roomSpawn;

    private Grid grid;

    [SerializeField] private uint tilesBetweenRooms;
    [SerializeField] private uint roomWidth, roomHeight; // Manual input of room size for now

    public Dictionary<int, RoomTemplate[,]> floorLayouts = new Dictionary<int, RoomTemplate[,]>(); // Floor layouts
    private Dictionary<int, GameObject[,]> floors = new Dictionary<int, GameObject[,]>();
    public GameObject[,] getFloor(int floorNum)
    {
        return floors[floorNum];
    }

    [SerializeField] GameObject leftDoor, rightDoor, upDoor, downDoor; // Door prefabs
    int activeFloor = 1;
    public int ActiveFloor
    {
        get { return activeFloor; }
    }
    public event System.Action<Room> RoomEnteredEvent; // Event for when player enters a room
    void Start()
    {
        instance = gameObject.GetComponent<MapManager>();
        grid = world.GetComponent<Grid>();
        mapGenerator = GetComponent<MapGenerator>();

        floorLayouts.Add(activeFloor, mapGenerator.GenerateFloorLayout()); // Generate first floor layout
        GenerateFloor(activeFloor); // Generate first floor
    }

    void GenerateRooms(int floorNum) // Generate rooms for floor
    {
        Transform roomPos = roomSpawn.transform;
        RoomTemplate[,] floor = floorLayouts[floorNum];
        if (floor == null)
            return;
        GameObject[,] floorRooms = new GameObject[floor.GetLength(0),floor.GetLength(1)];
        for (int row = 0; row < floor.GetLength(0); row++)
        {
            for (int col = 0; col < floor.GetLength(1); col++)
            {
                if (floor[row, col])
                {
                    //GameObject newRoom = floor[row, col].getRoomPrefab();
                    //Tilemap tilemap = newRoom.GetComponent<Tilemap>();
                    roomPos.position = new Vector3((row-floor.GetLength(0)/2+1) * (tilesBetweenRooms + grid.cellSize.x * roomWidth),
                                                   (col-floor.GetLength(1)/2+1) * (tilesBetweenRooms + grid.cellSize.y * roomHeight), 0);
                    
                    GameObject newRoom = Instantiate(floor[row, col].getRoomPrefab(), roomPos.position, Quaternion.identity);
                    newRoom.transform.SetParent(rooms.transform);
                    newRoom.GetComponent<Room>().width = roomWidth;
                    newRoom.GetComponent<Room>().height = roomHeight;

                    floorRooms[row, col] = newRoom;
                }
            }
        }

        // Assign floorRooms to floorNum
        floors.Add(floorNum, floorRooms);
    }

    void GenerateDoors(int floorNum) // Generate doors for floor
    {
        GameObject[,] floorRooms = floors[floorNum];
        for (int row = 0; row < floorRooms.GetLength(0); row++)
        {
            for (int col = 0; col < floorRooms.GetLength(1); col++)
            {
                if (!floorRooms[row, col])
                    continue;
                
                GameObject room = floorRooms[row, col];
                Room roomComponent = room.GetComponent<Room>();

                Tilemap tilemap = room.transform.Find("Walls").GetComponent<Tilemap>();
                if (tilemap && roomComponent)
                {
                    int roomWidthInTiles = tilemap.size.x; int roomHeightInTiles = tilemap.size.y;
                    if (row + 1 < floorRooms.GetLength(0) && floorRooms[row + 1, col])
                        CreateDoor(roomComponent, room.transform.position + new Vector3(roomWidthInTiles / 2, 0, 0), rightDoor);

                    if (row - 1 >= 0 && floorRooms[row - 1, col])
                        CreateDoor(roomComponent, room.transform.position + new Vector3(-roomWidthInTiles / 2, 0, 0), leftDoor);

                    if (col + 1 < floorRooms.GetLength(1) && floorRooms[row, col + 1])
                        CreateDoor(roomComponent, room.transform.position + new Vector3(0, roomHeightInTiles / 2, 0), upDoor);

                    if (col - 1 >= 0 && floorRooms[row, col - 1])
                        CreateDoor(roomComponent, room.transform.position + new Vector3(0, -roomHeightInTiles / 2, 0), downDoor);
                }
            }
        }

        // Link doors
        foreach (Transform childTransform in doors.transform)
        {
            Door childDoor = childTransform.gameObject.GetComponent<Door>();

            if (childDoor.linkedDoor) // If door is already linked, continue
                continue;
            LinkDoor(childDoor);
        }
    }
    void CreateDoor(Room roomComponent, Vector3 doorPos, GameObject doorPrefab)
    {
        transform.position = doorPos;
        GameObject newDoor = Instantiate(doorPrefab, transform);
        newDoor.transform.SetParent(doors.transform);

        if (roomComponent != null)
        {
            roomComponent.AddDoor(newDoor.GetComponent<Door>());
        }
    }
    void ConnectDoors(Door door1, Door door2)
    {
        door1.linkedDoor = door2;
        door2.linkedDoor = door1;
    }
    void LinkDoor(Door door)
    {
        Door doorToLink = null;
        foreach (Transform childTransform in doors.transform)
        {
            Door childDoor = childTransform.gameObject.GetComponent<Door>();
            // if childDoor is not to the left of door or childDoor is already linked, continue
            if (childDoor.linkedDoor)
                continue;
            switch (door.getDirection())
            {
                case Door.DIRECTION.LEFT:
                    if (childDoor.transform.position.x >= door.transform.position.x)
                        continue;
                    break;
                case Door.DIRECTION.RIGHT:
                    if (childDoor.transform.position.x <= door.transform.position.x)
                        continue;
                    break;
                case Door.DIRECTION.UP:
                    if (childDoor.transform.position.y <= door.transform.position.y)
                        continue;
                    break;
                case Door.DIRECTION.DOWN:
                    if (childDoor.transform.position.y >= door.transform.position.y)
                        continue;
                    break;
            }
            // if there is no doorToLink, set childDoor to doorToLink by default then continue
            if (!doorToLink)
            {
                doorToLink = childDoor;
                continue;
            }
            // if childDoor is closer to door, change doorToLink to childDoor
            if (Vector3.Distance(door.transform.position, childDoor.transform.position) < Vector3.Distance(door.transform.position, doorToLink.transform.position))
                doorToLink = childDoor;
        }
        if (doorToLink)
            ConnectDoors(door, doorToLink);
    }

    public void GenerateFloor(int floorNum)
    {
        GenerateRooms(floorNum); GenerateDoors(floorNum);
    }

    public void RoomEntered(Room room)
    {
        RoomEnteredEvent?.Invoke(room);
    }
}

