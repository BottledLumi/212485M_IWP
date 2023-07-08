using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindManager : MonoBehaviour
{
    [SerializeField] GameObject Rooms;
    
    public void GeneratePath()
    {
        List<GameObject> listOfCombatRoomsGO = FindCombatRooms();
        foreach(GameObject combatRoom in listOfCombatRoomsGO)
        {
            AstarPath pathFinder = combatRoom.GetComponent<AstarPath>();
            if (!pathFinder)
            {
                pathFinder = combatRoom.AddComponent<AstarPath>();
            }
        }
    }

    List<GameObject> FindCombatRooms()
    {
        List<GameObject> listOfCombatRoomsGO = new();
        foreach (Transform room in Rooms.transform)
        {
            if (room.name.Contains("CombatRoom"))
            {
                listOfCombatRoomsGO.Add(room.gameObject);
            }
        }
        return listOfCombatRoomsGO;
    }
}
