using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] uint pixelsInBetween;
    MapManager mapManager;

    public Image imagePrefab; // Reference to the UI Image prefab
    public Sprite activeNode, exploredNode, unexploredNode; // Reference to the sprite you want to instantiate
    public Sprite bossIcon, treasureIcon, cauldronIcon;

    private GameObject[,] map;
    private RoomTemplate[,] roomTypes;
    private Image[,] minimap;

    [SerializeField] float size;

    private void Start()
    {
        mapManager = MapManager.Instance;
        mapManager.RoomEnteredEvent += RenderMinimap;
    }
    public void setMap(GameObject[,] _map)
    {
        map = _map;
    }

    void RenderMinimap()
    {
        map = mapManager.getFloor(mapManager.ActiveFloor);
        roomTypes = mapManager.floorLayouts[mapManager.ActiveFloor];
        if (map == null)
            return;
        if (minimap == null)
            minimap = new Image[map.GetLength(0), map.GetLength(1)];

        //ClearMinimap();
        Vector3 activeNodePosition = Vector3.zero;
        for (int row = 0; row < minimap.GetLength(0); row++)  // Loop through minimap
        {
            for (int col = 0; col < minimap.GetLength(1); col++)
            {
                Image image = minimap[row, col];
                if (!map[row, col]) // if the room is empty on the map
                {
                    if (minimap[row, col]) // if active, set inactive
                        minimap[row, col].gameObject.SetActive(false);
                    continue;
                }
                if (!image) // if null, instantiate
                    image = Instantiate(imagePrefab, transform);
                image.transform.localScale = new Vector3(size, size, size); // Scale

                RectTransform rectTransform = image.rectTransform;
                image.transform.position = new Vector3((row - minimap.GetLength(0) / 2 + 1) * ((rectTransform.rect.width + pixelsInBetween) * size),
                                                        (col - minimap.GetLength(1) / 2 + 1) * ((rectTransform.rect.height + pixelsInBetween) * size),0f) + transform.position;

                RoomSettings.RoomType roomType = roomTypes[row, col].getRoomType();

                image.gameObject.SetActive(map[row,col].GetComponent<Room>().Status.explored); // Set active if the room has been explored
                if (CheckSurroundingRoomsExplored(row, col))
                    image.gameObject.SetActive(true);
                if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, image.transform.position))
                {
                    image.gameObject.SetActive(false);
                }

                // Set images based on room status and type
                Room currentRoom = map[row, col].GetComponent<Room>();
                if (currentRoom.Status.active)
                {
                    activeNodePosition = image.transform.position;
                    image.sprite = activeNode;
                }
                else if (currentRoom.Status.explored) // if explored, render explored node
                    image.sprite = exploredNode;
                else
                    image.sprite = unexploredNode;

                NodeIcon nodeIcon = image.GetComponent<NodeIcon>();
                if (nodeIcon)
                {
                    switch (roomType)
                    {
                        case RoomSettings.RoomType.BOSS:
                            nodeIcon.Icon = bossIcon;
                            break;
                        case RoomSettings.RoomType.TREASURE:
                            nodeIcon.Icon = treasureIcon;
                            break;
                        case RoomSettings.RoomType.CAULDRON:
                            nodeIcon.Icon = cauldronIcon;
                            break;
                        default:
                            nodeIcon.Icon = null;
                            break;
                    }
                }

                minimap[row, col] = image;
            }
        }
        // Adjust the position of the minimap to center the active node
        Vector3 offset = transform.position - activeNodePosition;
        foreach (Image image in minimap)
        {
            if (image != null && image.transform != null)
                image.transform.localPosition += offset;
        }
    }

    //void ClearMinimap()
    //{
    //    for (int row = 0; row < minimap.GetLength(0); row++)
    //    {
    //        for (int col = 0; col < minimap.GetLength(1); col++)
    //        {
    //            minimap[row, col] = null;
    //        }
    //    }
    //}

    bool CheckSurroundingRoomsExplored(int row, int col)
    {
        if (row + 1 < map.GetLength(0) && map[row + 1, col] && map[row + 1, col].GetComponent<Room>().Status.explored) // Check if out of bounds, exists and if active
            return true;
        if (row - 1 >= 0 && map[row - 1, col] && map[row - 1, col].GetComponent<Room>().Status.explored)
            return true;
        if (col + 1 < map.GetLength(1) && map[row, col + 1] && map[row, col + 1].GetComponent<Room>().Status.explored)
            return true;
        if (col - 1 >= 0 && map[row, col - 1] && map[row, col - 1].GetComponent<Room>().Status.explored)
            return true;
        return false;
    }
}
