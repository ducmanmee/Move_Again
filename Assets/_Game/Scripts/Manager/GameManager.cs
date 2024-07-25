using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Win, Revive, Setting, Lose}

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    public void OnInit()
    {

    }

    private void Start()
    {
        //UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void RevivalGame()
    {
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<CanvasRevival>();
        ChangeState(GameState.Revive);
    }

    public void LoseGame()
    {
        StartCoroutine(IE_LoseGame());
    }

    public IEnumerator IE_LoseGame()
    {
        yield return Cache.GetWFS(Constain.TIMER_DEAD);
        ChangeState(GameState.Lose);
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<CanvasFail>();
    }
        
    public void WinGame()
    {
        StartCoroutine(IE_WinGame());
    }

    public IEnumerator IE_WinGame()
    {
        Player.ins.IsWin = true;
        yield return Cache.GetWFS(Constain.TIMER_WIN);
        ChangeState(GameState.Win);
        UIManager.ins.CloseAll();
        UIManager.ins.OpenUI<CanvasVictory>();
    }


}
