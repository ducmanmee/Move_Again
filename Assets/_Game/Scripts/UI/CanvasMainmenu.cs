using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class CanvasMainmenu : UICanvas
{
    public TMP_Text textGold;
    public GameObject[] leftBtn;
    public float leftMoveX;
    public GameObject[] rightBtn;
    public float rightMoveX;
    public float durationMoveBtn;

    private void OnEnable()
    {
        SetTextGold();
        CameraManager.ins.SetCamMainMenu();
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.ins.OnInit();
    }

    public void MoveBtn(TweenCallback onComplete)
    {
        Sequence moveSequence = DOTween.Sequence();

        for (int i = 0; i < leftBtn.Length; i++)
        {
            moveSequence.Join(leftBtn[i].transform.DOMoveX(leftBtn[i].transform.position.x + leftMoveX, durationMoveBtn).SetEase(Ease.Linear));
        }

        for (int i = 0; i < rightBtn.Length; i++)
        {
            moveSequence.Join(rightBtn[i].transform.DOMoveX(rightBtn[i].transform.position.x + rightMoveX, durationMoveBtn).SetEase(Ease.Linear));
        }

        moveSequence.OnComplete(onComplete);
    }

    public void ResetBtn()
    {
        Sequence resetSequence = DOTween.Sequence();

        for (int i = 0; i < leftBtn.Length; i++)
        {
            resetSequence.Join(leftBtn[i].transform.DOMoveX(leftBtn[i].transform.position.x - leftMoveX, durationMoveBtn - .2f).SetEase(Ease.Linear));
        }

        for (int i = 0; i < rightBtn.Length; i++)
        {
            resetSequence.Join(rightBtn[i].transform.DOMoveX(rightBtn[i].transform.position.x - rightMoveX, durationMoveBtn - .2f).SetEase(Ease.Linear));
        }
    }

    public void SetTextGold()
    {
        textGold.text = DataManager.ins.dt.gold.ToString();
    }    

    public void PlayBtn()
    {
        UIManager.ins.CloseUI<CanvasMainmenu>();
        UIManager.ins.OpenUI<CanvasGameplay>();
    }   
    
    public void WeaponShopBtn()
    {
        MoveBtn(() => {
            UIManager.ins.CloseUI<CanvasMainmenu>();
            UIManager.ins.OpenUI<CanvasShopWeapon>();
        }); 
    }   
    
    public void SkinShopBtn()
    {
        MoveBtn(() =>
        {
            UIManager.ins.CloseAll();
            UIManager.ins.OpenUI<CanvasShopFashion>();
        });
    }
}
