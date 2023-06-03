using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] uint pixelsInBetween;
    MapManager mapManager;

    public Image imagePrefab; // Reference to the UI Image prefab
    public Sprite spriteToInstantiate; // Reference to the sprite you want to instantiate

    private GameObject[,] map;
    private Image[,] minimap;

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
        if (map == null)
            return;
        if (minimap == null)
            minimap = new Image[map.GetLength(0), map.GetLength(1)];
        //ClearMinimap();
        for (int row = 0; row < minimap.GetLength(0); row++)
        {
            for (int col = 0; col < minimap.GetLength(1); col++)
            {
                if (!map[row, col]) // if the room is empty on the map
                {
                    if (minimap[row, col]) // if active, set inactive
                        minimap[row, col].gameObject.SetActive(false);
                    continue;
                }
                Image image = minimap[row, col];
                if (!image) // if null, instantiate
                    image = Instantiate(imagePrefab, transform);
                image.sprite = spriteToInstantiate;
                RectTransform rectTransform = image.rectTransform;
                image.transform.position = new Vector3((row - minimap.GetLength(0) / 2 + 1) * (rectTransform.rect.width * 2 + pixelsInBetween),
                                                       (col - minimap.GetLength(1) / 2 + 1) * (rectTransform.rect.height * 2 + pixelsInBetween), 0) + transform.position;
                image.gameObject.SetActive(map[row,col].GetComponent<Room>().Status.explored); // Set active if the room has been explored

                minimap[row, col] = image;
            }
        }
    }

    void ClearMinimap()
    {
        for (int row = 0; row < minimap.GetLength(0); row++)
        {
            for (int col = 0; col < minimap.GetLength(1); col++)
            {
                minimap[row, col] = null;
            }
        }
    }
}
