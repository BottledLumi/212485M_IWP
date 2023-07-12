using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    PlayerData playerData;

    [SerializeField] Image image;

    private void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.WeaponChangedEvent += OnWeaponChanged;
    }
    void Start()
    {
        image.preserveAspect = true;
    }

    void OnWeaponChanged(Weapon weapon)
    {
        image.sprite = weapon.Icon;
    }

    private void OnEnable()
    {
        image.sprite = playerData.Weapon.Icon;
    }
}
