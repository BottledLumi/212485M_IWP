using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private MapGenerator mapGenerator;
    [SerializeField] private GameObject world;
    [SerializeField] private GameObject roomSpawn;

    private Grid grid;

    [SerializeField] private uint tilesBetweenRooms;
    [SerializeField] private uint roomWidth, roomHeight;

    public List<RoomTemplate[,]> floorLayouts = new List<RoomTemplate[,]>();
    void Start()
    {
        grid = world.GetComponent<Grid>();
        mapGenerator = GetComponent<MapGenerator>();

        floorLayouts.Add(mapGenerator.GenerateFloorLayout());
        GenerateFloor(0);
    }

    void GenerateFloor(int floorNum)
    {
        Transform roomPos = roomSpawn.transform;
        RoomTemplate[,] floor = floorLayouts[floorNum];
        if (floor == null)
            return;
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
                    newRoom.transform.SetParent(world.transform);
                }
            }
        }
    }
}
