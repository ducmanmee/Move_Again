using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Image iconImg;
    [SerializeField] Image directImg;
    [SerializeField] RectTransform direct;
    [SerializeField] TMP_Text nameTxt;
    [SerializeField] TMP_Text scoreTxt;

    Transform target;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2; 

    Vector3 viewPoint;

    Vector2 viewPointX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointY = new Vector2(0.05f, 0.85f);
    
    Vector2 viewPointInCameraX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointInCameraY = new Vector2(0.05f, 0.95f);

    Camera Camera => CameraManager.ins.cam;

    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(target.position);
        direct.gameObject.SetActive(!IsInCamera);
        nameTxt.gameObject.SetActive(IsInCamera);

        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);

        Vector3 targetSPoint = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
        Vector3 playerSPoint = Camera.WorldToScreenPoint(Player.ins.CharacterTF().position) - screenHalf;      
        rect.anchoredPosition = targetSPoint;

        direct.up = (targetSPoint - playerSPoint).normalized;
    }

    public void OnInit()
    {
        SetScore(0);
        SetColor(new Color(Random.value, Random.value, Random.value, 1));
        //SetAlpha(GameManager.Ins.IsState(GameState.Gameplay) ? 1 : 0);
    }

    public void OnDespawn()
    {
        PoolingTargetIndicator.ins.EnQueueObj(Constain.TAG_TARGET, this);
    }    

    public void SetTarget(Transform target)
    {
        this.target = target;
        OnInit();
    }

    public void SetScore(int score)
    {
        scoreTxt.text = score.ToString();
    }

    public void SetName(string name)
    {
        nameTxt.text = name;
    }

    private void SetColor(Color color)
    {
        iconImg.color = color;
        nameTxt.color = color;
    }
}
