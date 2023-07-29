using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaterEffect", menuName = "ItemEffect/WaterEffect")]
public class WaterEffect : ItemEffect
{
    float baseMsMultiplier = 0.8f;

    public override void OnAdd()
    {
        MapManager.Instance.RoomEnteredEvent += OnRoomEntered;
    }

    private float MovementSpeedMultiplier()
    {
        return Mathf.Pow(baseMsMultiplier, Value);
    }

    private void OnRoomEntered(Room room)
    {
        foreach (GameObject gameObject in room.Enemies)
            gameObject.GetComponent<Enemy>().MovementSpeed *= MovementSpeedMultiplier();
    }
}
