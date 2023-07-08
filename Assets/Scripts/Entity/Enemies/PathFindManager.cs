using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathFindManager : MonoBehaviour
{
    static public void GeneratePath(GameObject room, uint width, uint height)
    {
        AstarPath pathFinder = room.GetComponent<AstarPath>();
        if (!pathFinder)
        {
            pathFinder = room.AddComponent<AstarPath>();
        }

        GridGraph graph = pathFinder.data.AddGraph(typeof(GridGraph)) as GridGraph;
        graph.is2D = true;
        graph.center = room.transform.localPosition;
        graph.SetDimensions((int)width, (int)height, 1);
        graph.collision.use2D = true;
        graph.collision.mask = LayerMask.GetMask("Environment");
        graph.Scan();
    }
}
