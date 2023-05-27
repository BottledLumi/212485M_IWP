using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using RoomSettings;
public struct Coord2D
{
    public int x;
    public int y;
    public Coord2D(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
public class MapGenerator : MonoBehaviour
{
    class Path
    {
        public enum DIRECTION
        {
            RIGHT,
            UP,
            LEFT,
            DOWN,
            NONE
        }

        public Coord2D coord;
        public DIRECTION lastDirection;
        public Path(Coord2D _coord, DIRECTION _lastDirection)
        {
            coord = _coord;
            lastDirection = _lastDirection;
        }
    }

    [SerializeField] private ushort floorWidth, floorHeight;
    [SerializeField] private List<RoomTemplate> roomTemplates;
    [SerializeField] private ushort numPaths; // beginning number of paths
    [SerializeField] private ushort numRooms;
    [SerializeField] private ushort numCauldronRooms, numTreasureRooms, numBossRooms;

    // Return randomly-generated floor layout
    public RoomTemplate[,] GenerateFloorLayout()
    {
        int x, y;
        int numSpecialRooms = numCauldronRooms + numTreasureRooms + numBossRooms;
        RoomTemplate[,] floorLayout = new RoomTemplate[floorWidth, floorHeight];

        x = floorWidth / 2 - 1; y = floorHeight / 2 - 1;
        floorLayout[x, y] = RandomiseRoomOfType(RoomType.STARTING);
        int numGeneratedRooms = 1;

        //*****Modified random walk *****
        if (numPaths < 1)
            return null;

        List<Path> pathList = new();
        for (int i = 0; i < numPaths; i++)
        {
            Path newPath = new(new Coord2D(x, y), Path.DIRECTION.NONE);
            pathList.Add(newPath);
        }

        // Floor-generation algorithm
        while (numGeneratedRooms < numRooms - numSpecialRooms)
        {
            for (int i = 0; i < pathList.Count && numGeneratedRooms < numRooms - numSpecialRooms; i++)
            {
                Path path = pathList[i];
                while (floorLayout[path.coord.x, path.coord.y])
                    RandomisePathDirection(path);
                floorLayout[path.coord.x, path.coord.y] = RandomiseRoomOfType(RoomType.COMBAT);
                numGeneratedRooms++;
            }
        }
        //TO DO: Generate more isolated paths

        // Add special rooms
        List<Path> isolatedRooms = new();
        for (int row = 0; row < floorWidth; row++)
        {
            for (int col = 0; col < floorHeight; col++)
            {
                int numOfNeighbours = CheckNumOfNeighbours(floorLayout, row, col);
                if (numOfNeighbours == 1)
                {
                    Path newPath = new(new Coord2D(row, col), Path.DIRECTION.NONE);
                    isolatedRooms.Add(newPath);
                }
            }
        }

        // Cauldron rooms
        for (int i = 0; i < numCauldronRooms; i++)
        {
            if (isolatedRooms.Count < 1)
                break;
            System.Random random = new System.Random();
            Path randomPath = isolatedRooms[random.Next(isolatedRooms.Count)];
            while (floorLayout[randomPath.coord.x, randomPath.coord.y])
                RandomisePathDirection(randomPath);
            floorLayout[randomPath.coord.x, randomPath.coord.y] = RandomiseRoomOfType(RoomType.CAULDRON);
            numGeneratedRooms++;

            isolatedRooms.Remove(randomPath);
        }

        // Treasure rooms
        for (int i = 0; i < numTreasureRooms; i++)
        {
            if (isolatedRooms.Count < 1)
                break;
            System.Random random = new System.Random();
            Path randomPath = isolatedRooms[random.Next(isolatedRooms.Count)];
            while (floorLayout[randomPath.coord.x, randomPath.coord.y])
                RandomisePathDirection(randomPath);
            floorLayout[randomPath.coord.x, randomPath.coord.y] = RandomiseRoomOfType(RoomType.TREASURE);
            numGeneratedRooms++;
            
            isolatedRooms.Remove(randomPath);
        }

        // Boss rooms
        for (int i = 0; i < numBossRooms; i++)
        {
            if (isolatedRooms.Count < 1)
                break;

            System.Random random = new System.Random();
            Path randomPath = isolatedRooms[random.Next(isolatedRooms.Count)];
            while (floorLayout[randomPath.coord.x, randomPath.coord.y])
                RandomisePathDirection(randomPath);
            floorLayout[randomPath.coord.x, randomPath.coord.y] = RandomiseRoomOfType(RoomType.BOSS);
            numGeneratedRooms++;

            isolatedRooms.Remove(randomPath);
        }

        return floorLayout;
    }

    // Return random room of a specific type
    private RoomTemplate RandomiseRoomOfType(RoomType roomType)
    {
        List<RoomTemplate> roomTemplatesOfType = new List<RoomTemplate>();
        foreach (RoomTemplate room in roomTemplates)
        {
            if (room.getRoomType() == roomType)
                roomTemplatesOfType.Add(room);
        }

        if (roomTemplatesOfType.Count < 1)
            return null;

        int randomRoom = UnityEngine.Random.Range(0, roomTemplatesOfType.Count - 1);
        return roomTemplatesOfType[randomRoom]; 
    }

    // Randomise directions among right, up, left, down
    private void RandomisePathDirection(Path path)
    {
        Path.DIRECTION direction = path.lastDirection;

        // Higher chance to continue in the same direction
        int proceed = UnityEngine.Random.Range(0, 10);
        if (path.lastDirection == Path.DIRECTION.NONE || proceed > 5)
        {
            // Convert enum into array
            Path.DIRECTION[] directions = (Path.DIRECTION[])Enum.GetValues(typeof(Path.DIRECTION));

            // Randomise from Path.DIRECTION enum
            int rand = UnityEngine.Random.Range(0, directions.Length);
            direction = directions[rand];
        }

        switch (direction)
        {
            case Path.DIRECTION.RIGHT:
                if (path.coord.x < floorWidth - 1) path.coord.x++;
                break;
            case Path.DIRECTION.UP:
                if (path.coord.y < floorHeight - 1) path.coord.y++;
                break;
            case Path.DIRECTION.LEFT:
                if (path.coord.x > 0) path.coord.x--;
                break;
            case Path.DIRECTION.DOWN:
                if (path.coord.y > 0) path.coord.y--;
                break;
        }

        path.lastDirection = direction;
    }

    private int CheckNumOfNeighbours(RoomTemplate[,] floorLayout, int x, int y)
    {
        if (x < 0 || x >= floorWidth || y < 0 || y >= floorHeight)
            return 0;

        int numOfNeighbours = 0;
        Vector2Int[] neighbours = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        foreach (Vector2Int neighbour in neighbours)
        {
            int neighbourX = x + neighbour.x;
            int neighbourY = y + neighbour.y;
            if (neighbourX >= 0 && neighbourX < floorWidth && neighbourY >= 0 && neighbourY < floorHeight)
            {
                if (floorLayout[neighbourX, neighbourY])
                    numOfNeighbours++;
            }
        }

        return numOfNeighbours;
    }
}
