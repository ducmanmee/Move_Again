using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void HomeBtn()
    {
        UIManager.ins.CloseAll();
        LevelManager.ins.OnReset();
        UIManager.ins.OpenUI<CanvasMainmenu>();
    }   
    
    public void ContinueBtn()
    {
        UIManager.ins.CloseUI<CanvasSetting>();
    }    
}
