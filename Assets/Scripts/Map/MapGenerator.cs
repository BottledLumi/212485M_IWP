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

    [SerializeField] private ushort minConnectingRooms, maxConnectingRooms;

    Coord2D startCoord;

    // Return randomly-generated floor layout
    public RoomTemplate[,] GenerateFloorLayout()
    {
        int numSpecialRooms = numCauldronRooms + numTreasureRooms + numBossRooms;
        RoomTemplate[,] floorLayout = new RoomTemplate[floorWidth, floorHeight];


        startCoord.x = floorWidth / 2 - 1; startCoord.y = floorHeight / 2 - 1;
        floorLayout[startCoord.x, startCoord.y] = RandomiseRoomOfType(RoomType.STARTING);
        int numGeneratedRooms = 1;

        //*****Modified random walk *****
        if (numPaths < 1)
            return null;

        int x = startCoord.x, y = startCoord.y;

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
                while (floorLayout[path.coord.x, path.coord.y] 
                   || CheckNumOfNeighbours(floorLayout, path.coord.x, path.coord.y) < minConnectingRooms 
                   || CheckNumOfNeighbours(floorLayout, path.coord.x, path.coord.y) > maxConnectingRooms)
                    RandomisePathDirection(path);
                floorLayout[path.coord.x, path.coord.y] = RandomiseRoomOfType(RoomType.COMBAT);
                numGeneratedRooms++;
            }
        }
        //TO DO: Generate more isolated paths

        // Add special rooms
        List<Coord2D> isolatedRooms = new();
        for (int row = 0; row < floorWidth; row++)
        {
            for (int col = 0; col < floorHeight; col++)
            {
                if (!floorLayout[row, col])
                {
                    int numOfNeighbours = CheckNumOfNeighbours(floorLayout, row, col);
                    if (numOfNeighbours == 1)
                    {
                        isolatedRooms.Add(new Coord2D(row, col));
                    }
                }
            }
        }

        // Cauldron rooms
        for (int i = 0; i < numCauldronRooms; i++)
        {
            if (isolatedRooms.Count < 1)
                break;
            System.Random random = new System.Random();
            Coord2D randomCoord = isolatedRooms[random.Next(isolatedRooms.Count)];
            floorLayout[randomCoord.x, randomCoord.y] = RandomiseRoomOfType(RoomType.CAULDRON);
            numGeneratedRooms++;

            isolatedRooms.Remove(randomCoord);
        }

        // Treasure rooms
        for (int i = 0; i < numTreasureRooms; i++)
        {
            if (isolatedRooms.Count < 1)
                break;
            System.Random random = new System.Random();
            Coord2D randomCoord = isolatedRooms[random.Next(isolatedRooms.Count)];
            floorLayout[randomCoord.x, randomCoord.y] = RandomiseRoomOfType(RoomType.TREASURE);
            numGeneratedRooms++;

            isolatedRooms.Remove(randomCoord);
        }

        // Boss rooms
        for (int i = 0; i < numBossRooms; i++)
        {
            if (isolatedRooms.Count < 1)
                break;
            Coord2D randomCoord = new Coord2D(startCoord.x, startCoord.y);

            int tries = 0;
            while (DistanceFromStart(randomCoord) < numRooms/4 || CheckNumOfNeighbours(floorLayout, randomCoord.x, randomCoord.y) != 1) 
            {
                tries++;
                if (tries > 150)
                    break;

                System.Random random = new System.Random();
                randomCoord = isolatedRooms[random.Next(isolatedRooms.Count)];
            }
            floorLayout[randomCoord.x, randomCoord.y] = RandomiseRoomOfType(RoomType.BOSS);
            numGeneratedRooms++;

            isolatedRooms.Remove(randomCoord);
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

        int randomRoom = UnityEngine.Random.Range(0, roomTemplatesOfType.Count);
        return roomTemplatesOfType[randomRoom]; 
    }

    // Randomise directions among right, up, left, down
    private void RandomisePathDirection(Path path)
    {
        Path.DIRECTION direction = path.lastDirection;

        // Higher chance to continue in the same direction
        int proceed = UnityEngine.Random.Range(0, 10);
        if (path.lastDirection == Path.DIRECTION.NONE || proceed > 3)
        {
            int rand1 = UnityEngine.Random.Range(0, 1);
            if (rand1 == 1)
            {
                List<Path.DIRECTION> awayDirections = new List<Path.DIRECTION>();
                if (path.coord.x != 0 || path.coord.y != 0)
                {
                    if (path.coord.x > 0)
                        awayDirections.Add(Path.DIRECTION.RIGHT);
                    else if (path.coord.x < 0)
                        awayDirections.Add(Path.DIRECTION.LEFT);

                    if (path.coord.y > 0)
                        awayDirections.Add(Path.DIRECTION.UP);
                    else if (path.coord.y < 0)
                        awayDirections.Add(Path.DIRECTION.DOWN);
                }

                // Randomise from awayDirections
                int rand2 = UnityEngine.Random.Range(0, awayDirections.Count);
                direction = awayDirections[rand2];
            }
            else
            {
                // Convert enum into array
                Path.DIRECTION[] directions = (Path.DIRECTION[])Enum.GetValues(typeof(Path.DIRECTION));

                // Randomise from Path.DIRECTION enum
                int rand2 = UnityEngine.Random.Range(0, directions.Length);
                direction = directions[rand2];
            }
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

    private int DistanceFromStart(Coord2D coord)
    {
        return Mathf.Abs(coord.x - startCoord.x) + Mathf.Abs(coord.y - startCoord.y);
    }
}
