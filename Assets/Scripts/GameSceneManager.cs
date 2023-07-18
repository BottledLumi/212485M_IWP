using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameScene gameScene;
    public uint enemyKilled = 0;

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
