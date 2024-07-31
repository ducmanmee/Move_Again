using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UICanvas
{
    public static CanvasGameplay ins;

    [SerializeField] Text numberOfEnemyTxt;

    private void MakeInstance()
    {
        if(ins == null)
        {
            ins = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void OnEnable()
    {
        GameManager.ChangeState(GameState.GamePlay);
        CameraManager.ins.SetCamGamePlay();
        CanvasGameplay.ins.SetNumberText(LevelManager.ins.RemainingEnemy + 1);
    }

    public void SetNumberText(int index)
    {
        numberOfEnemyTxt.text = index.ToString();
    }  
    
    public void SettingBtn()
    {
        UIManager.ins.OpenUI<CanvasSetting>();
    }    
}
