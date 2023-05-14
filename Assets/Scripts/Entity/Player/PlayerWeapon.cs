using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    void Start()
    {
        
    }
    void Update()
    {
        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); mousePosition.z = 0;

            List<GameObject> targets = weapon.Attack(gameObject.transform.position, mousePosition);

        }
    }
}
