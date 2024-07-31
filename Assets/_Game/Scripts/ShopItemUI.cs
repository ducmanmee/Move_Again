using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image iconShop;
    public int idItemShop;
    public GameObject target;
    public GameObject lockIcon;
    public GameObject eqipped;
    
    public void ShopBtnClick()
    {
        if (CanvasShopFashion.curTypeShop == ShopSkinType.pant)
        {
            Shop_Pant.ins.SetTargetBtn(idItemShop);
            Shop_Pant.ins.IdSelecting = idItemShop;
            Shop_Pant.ins.StatusBtnShop(idItemShop);
            Shop_Pant.ins.Btn_Click();
        }
        else if (CanvasShopFashion.curTypeShop == ShopSkinType.hat)
        {
            Shop_Hat.ins.SetTargetBtn(idItemShop);
            Shop_Hat.ins.IdSelecting = idItemShop;
            Shop_Hat.ins.StatusBtnShop(idItemShop);
            Shop_Hat.ins.Btn_Click();
        }
        else if (CanvasShopFashion.curTypeShop == ShopSkinType.shield)
        {
            Shop_Shield.ins.SetTargetBtn(idItemShop);
            Shop_Shield.ins.IdSelecting = idItemShop;
            Shop_Shield.ins.StatusBtnShop(idItemShop);
            Shop_Shield.ins.Btn_Click();
        }
        else
        {
            
        }
    }    

    public void ActiveTarget(bool active)
    {
        target.SetActive(active);
    }   

    public void SetEquipped(bool active)
    {
        eqipped.SetActive(active);
    }    

    public void SetLock(bool active)
    {
        lockIcon.SetActive(active);
    }    
    
}
