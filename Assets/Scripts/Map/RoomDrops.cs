using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDrops : MonoBehaviour
{
    [SerializeField] List<GameObject> dropPool = new List<GameObject>();
    
    [SerializeField] float dropRate;
 
    public void ChanceDrop()
    {
        float randomPercentage = Random.Range(0f, 1f) * 100f;
        if (randomPercentage < dropRate)
        {
            int rand = UnityEngine.Random.Range(0, dropPool.Count);
            Drop(dropPool[rand]);
        }
    }

    private void Drop(GameObject prefab)
    {
        Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    }
}
