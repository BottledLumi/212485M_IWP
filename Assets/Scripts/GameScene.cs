using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[CreateAssetMenu(fileName = "New Game Scene", menuName = "Game Scene")]
public class GameScene : ScriptableObject
{
    [SerializeField] Canvas winScreen;
    [SerializeField] Canvas loseScreen;

    public GameObject WinState()
    {
        Canvas screen = Instantiate(winScreen);
        screen.transform.GetChild(0).Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(delegate { GameStateManager.instance.ReturnToMenu(); });

        return screen.gameObject;
    }

    public GameObject LoseState()
    {
        Canvas screen = Instantiate(loseScreen);
        screen.transform.GetChild(0).Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(delegate { GameStateManager.instance.ReturnToMenu(); });

        return screen.gameObject;
    }
}
