using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    [SerializeField] GameScene gameScene;
    [HideInInspector] public uint enemyKilled = 0;
    [HideInInspector] public uint roomsCleared = 0;

    public event System.Action<Boss> BossEvent;
    public void CallBossEvent(Boss boss)
    {
        BossEvent?.Invoke(boss);
    }

    private GameObject screen;
    public void SetGameState(bool win)
    {
        if (screen)
            return;
        if (win)
            screen = gameScene.WinState();
        else
            screen = gameScene.LoseState();

        TextMeshProUGUI killText = screen.transform.GetChild(0).Find("KillCountText").GetComponent<TextMeshProUGUI>();
        killText.text = "Enemies killed: " + enemyKilled;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
