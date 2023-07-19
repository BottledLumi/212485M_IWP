using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                GameStateManager.instance.SetGameState(true);
                break;
        }
    }
}
