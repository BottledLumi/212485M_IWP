using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CheeseburgerEffect", menuName = "ItemEffect/CheeseburgerEffect")]
public class CheeseburgerEffect : ItemEffect
{
    PlayerData playerData;
    PlayerWeapon playerWeapon;

    public override void OnAdd()
    {
        MapManager.Instance.RoomEnteredEvent += OnRoomEntered;

        playerData = PlayerData.Instance;
        playerWeapon = ItemsManager.Instance.player.GetComponent<PlayerWeapon>();
        playerWeapon.EnemyHitEvent += OnEnemyHit;
    }

    public override void OnRemove()
    {
        MapManager.Instance.RoomEnteredEvent -= OnRoomEntered;
        playerWeapon.EnemyHitEvent -= OnEnemyHit;
    }

    private void OnRoomEntered(Room room)
    {
        foreach (GameObject gameObject in room.Enemies)
        {
            if (!gameObject.GetComponent<Boss>())
                gameObject.AddComponent<DisplayHealth>();
        }
    }

    private void OnEnemyHit(Enemy enemy)
    {
        if (Value < 2)
            return;
        float extraPercentage = (enemy.Health / enemy.MaxHealth) * (Value-1) / 5;

        enemy.TakeDamage(playerData.Attack * playerData.Weapon.getAttack() * extraPercentage);
    }
}
