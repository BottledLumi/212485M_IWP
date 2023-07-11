using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    [SerializeField] float stayTime;

    private void Start()
    {
        StartCoroutine(WaitTimeCoroutine());
    }

    IEnumerator WaitTimeCoroutine()
    {
        yield return new WaitForSeconds(stayTime);
        Destroy(gameObject);
    }
}
