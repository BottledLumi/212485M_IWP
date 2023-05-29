using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private MapGenerator mapGenerator;
    [SerializeField] private GameObject world, rooms, doors;
    [SerializeField] private GameObject roomSpawn;

    private Grid grid;

    [SerializeField] private uint tilesBetweenRooms;
    [SerializeField] private uint roomWidth, roomHeight;

    public List<RoomTemplate[,]> floorLayouts = new List<RoomTemplate[,]>();
    private Dictionary<int, GameObject[,]> floors = new Dictionary<int, GameObject[,]>();

    [SerializeField] GameObject leftDoor, rightDoor, upDoor, downDoor; // Door prefabs
    void Start()
    {
        grid = world.GetComponent<Grid>();
        mapGenerator = GetComponent<MapGenerator>();

        floorLayouts.Add(mapGenerator.GenerateFloorLayout());
        GenerateFloor(0); GenerateDoors(0);
    }

    void GenerateFloor(int floorNum)
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

                    floorRooms[row, col] = newRoom;
                }
            }
        }

        // Assign floorRooms to floorNum
        floors.Add(floorNum, floorRooms);
    }

    // Add doors
    void GenerateDoors(int floorNum)
    {
        GameObject[,] floorRooms = floors[floorNum];
        for (int row = 0; row < floorRooms.GetLength(0); row++)
        {
            for (int col = 0; col < floorRooms.GetLength(1); col++)
            {
                if (floorRooms[row, col])
                {
                    GameObject room = floorRooms[row, col];
                    Room roomComponent = room.GetComponent<Room>();

                    Tilemap tilemap = room.transform.Find("Walls").GetComponent<Tilemap>();
                    if (tilemap && roomComponent)
                    {
                        int roomWidthInTiles = tilemap.size.x; int roomHeightInTiles = tilemap.size.y+1; // +1 to y to solve odd-even discrepancy
                        if (floorRooms[row + 1, col])
                        {
                            CreateDoor(roomComponent, room.transform.position + new Vector3(roomWidthInTiles / 2, 0, 0), rightDoor);
                        }

                        if (floorRooms[row - 1, col])
                        {
                            CreateDoor(roomComponent, room.transform.position + new Vector3(-roomWidthInTiles / 2, 0, 0), leftDoor);
                        }

                        if (floorRooms[row, col + 1])
                        {
                            CreateDoor(roomComponent, room.transform.position + new Vector3(0, roomHeightInTiles / 2, 0), upDoor);
                        }

                        if (floorRooms[row, col - 1])
                        {
                            CreateDoor(roomComponent, room.transform.position + new Vector3(0, -roomHeightInTiles / 2, 0), downDoor);
                        }
                    }
                }
            }
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
}

