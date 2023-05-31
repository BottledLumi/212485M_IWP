using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] GameObject worldObject; // Reference to your "World" GameObject
    List<Tilemap> tilemaps = new List<Tilemap>();
    public void GenerateNavMesh()
    {
        // Create a new NavMeshBuildSettings instance
        NavMeshBuildSettings buildSettings = NavMesh.GetSettingsByID(0);

        // Create a NavMeshData instance
        NavMeshData navMeshData = new NavMeshData();

        // Generate the NavMesh for the specified bounds and walkable tiles
        Bounds bounds = CalculateWorldBounds();
        NavMeshBuilder.UpdateNavMeshData(navMeshData, buildSettings, CollectBuildSources(), bounds);

        // Remove any existing NavMesh data in the scene
        NavMesh.RemoveAllNavMeshData();

        // Add the new NavMeshData to the scene
        NavMesh.AddNavMeshData(navMeshData);
    }

    private List<NavMeshBuildSource> CollectBuildSources()
    {
        // Get the Tilemap component from the "World" GameObject or any other relevant objects
        Tilemap[] tilemapArray = worldObject.GetComponentsInChildren<Tilemap>();
        // Convert the array to a list
        tilemaps = new List<Tilemap>(tilemapArray);

        // Create a list of NavMeshBuildSource for walkable tiles
        List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();

        foreach (Tilemap tilemap in tilemaps)
        {
            foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(tilePosition);

                // Check if the tile is walkable (You may have your own criteria to determine this)
                if (tile != null && IsTileWalkable(tile))
                {
                    Vector3 tileWorldPosition = tilemap.CellToWorld(tilePosition);
                    NavMeshBuildSource buildSource = new NavMeshBuildSource
                    {
                        shape = NavMeshBuildSourceShape.Box,
                        size = tilemap.cellSize,
                        transform = Matrix4x4.TRS(tileWorldPosition + tilemap.cellSize * 0.5f, Quaternion.identity, Vector3.one),
                        area = 0 // Set the area type as desired (0 for default)
                    };

                    buildSources.Add(buildSource);
                }
            }
        }

        return buildSources;
    }

    private bool IsTileWalkable(TileBase tile)
    {
        switch (tile.name)
        {
            case "pantryFloor":
            return true;
        }
        return false;
    }

    private Bounds CalculateWorldBounds()
    {
        // Calculate the bounds of the "World" GameObject or any other relevant objects
        Renderer[] renderers = worldObject.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }

        return new Bounds();
    }
}