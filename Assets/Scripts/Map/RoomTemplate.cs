using UnityEngine;
using RoomSettings;

namespace RoomSettings
{
    public enum RoomType
    {
        STARTING,
        COMBAT,
        TREASURE,
        CAULDRON,
        BOSS
    }
}

[CreateAssetMenu(fileName = "New Room Template", menuName = "Room Template")]

public class RoomTemplate : ScriptableObject
{
    [SerializeField] private GameObject roomPrefab;
    public GameObject getRoomPrefab() { return roomPrefab; }

    [SerializeField] private RoomType roomType;
    public RoomType getRoomType() { return roomType; }

    [SerializeField] private Sprite roomIcon;

    private void Awake()
    {
        switch (roomType)
        {
            case RoomType.STARTING:
                roomIcon = null;
                break;
            case RoomType.COMBAT:
                roomIcon = null;
                break;
            case RoomType.TREASURE:
                roomIcon = null;
                break;
            case RoomType.CAULDRON:
                roomIcon = null;
                break;
            case RoomType.BOSS:
                roomIcon = null;
                break;
            default:
                Debug.LogError("Invalid room type!");
                break;
        }
    }
}
