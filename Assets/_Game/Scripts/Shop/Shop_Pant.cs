using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Pant : CanvasShopFashion
{
    public static Shop_Pant ins;
    PantType[] pantTypeShops;
    [SerializeField] PantSODatas pantSODatas;

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
        GetEnumPant();
        InitializeButtons();
        StatusBtnShop(10);
    }

    public void GetEnumPant()
    {
        PantType[] enumValues = (PantType[])System.Enum.GetValues(typeof(PantType));
        pantTypeShops = new PantType[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            pantTypeShops[i] = enumValues[i];
        }
    }  
    
    void InitializeButtons()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].idItemShop = (int)pantTypeShops[i+1];
            if (DataManager.ins.dt.status_Pant[Array.IndexOf(pantTypeShops, (PantType)curIdItem) + 1] == (int)StateItemType.buy_yet)
            {
                shopItems[i].SetLock(true);
            }    
            SetEquippedBtn(DataManager.ins.dt.idPant);
            
            if (i < iconItemShop.Length)
            {
                SetIcon(shopItems[i].iconShop, iconItemShop[i]);
            }
        }
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.ChangePant((PantType)IdSelecting);
    }

    public void StatusBtnShop(int id)
    {
        int index = Array.IndexOf(pantTypeShops, (PantType)id);
        curIdItem = id;
        if (DataManager.ins.dt.status_Pant[index + 1] == (int)StateItemType.buy_yet)
        {
            curTypeBtn = ShopBtnType.buy;
            SetPrice(pantSODatas.GetPrice((PantType)id));
            ActiveBtn(ShopBtnType.buy);
        }    
        else
        {
            if(id == DataManager.ins.dt.idPant)
            {
                curTypeBtn = ShopBtnType.equipped;
                ActiveBtn(ShopBtnType.equipped);
            }    
            else
            {
                curTypeBtn = ShopBtnType.select;
                ActiveBtn(ShopBtnType.select);
            }    
        }    
    }

    public void BuyBtn()
    {
        if(curTypeBtn == ShopBtnType.buy)
        {
            if(DataManager.ins.dt.gold > pantSODatas.GetPrice((PantType)curIdItem))
            {
                shopItems[Array.IndexOf(pantTypeShops, (PantType)curIdItem)-1].SetLock(false);
                DataManager.ins.dt.gold -= pantSODatas.GetPrice((PantType)curIdItem);
                SetTextGold();
                DataManager.ins.dt.status_Pant[Array.IndexOf(pantTypeShops, (PantType)curIdItem) + 1] = (int)StateItemType.buy;
                StatusBtnShop(curIdItem);
            }      
        }
        else if(curTypeBtn == ShopBtnType.select)
        {
            DataManager.ins.dt.idPant = curIdItem;
            SetEquippedBtn(DataManager.ins.dt.idPant);
            StatusBtnShop(curIdItem);
            Player.ins.ChangePant((PantType)curIdItem);
        }
        else
        {
            shopItems[Array.IndexOf(pantTypeShops, (PantType)curIdItem) - 1].SetEquipped(false);
            DataManager.ins.dt.idPant = 0;
            Player.ins.ChangePant(PantType.None);
            StatusBtnShop(curIdItem);
        }    
    }    
}
