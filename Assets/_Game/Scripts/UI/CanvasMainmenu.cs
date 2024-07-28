using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainmenu : UICanvas
{
    private void OnEnable()
    {
        CameraManager.ins.SetCamMainMenu();
        GameManager.ChangeState(GameState.MainMenu);
    }

    public void PlayBtn()
    {
        UIManager.ins.CloseUI<CanvasMainmenu>();
        UIManager.ins.OpenUI<CanvasGameplay>();
    }   
    
    public void WeaponShopBtn()
    {

    }   
    
    public void SkinShopBtn()
    {

    }    
}
