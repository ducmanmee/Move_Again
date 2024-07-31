using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Shield : CanvasShopFashion
{
    public static Shop_Shield ins;
    ShieldType[] shieldTypeShops;
    [SerializeField] ShieldSODatas shieldSODatas;

    private void MakeInstance()
    {
        if (ins == null)
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
        GetEnumShield();
        InitializeButtons();
        StatusBtnShop(10);
    }

    public void GetEnumShield()
    {
        ShieldType[] enumValues = (ShieldType[])System.Enum.GetValues(typeof(ShieldType));
        shieldTypeShops = new ShieldType[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            shieldTypeShops[i] = enumValues[i];
        }
    }

    void InitializeButtons()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].idItemShop = (int)shieldTypeShops[i + 1];
            if (DataManager.ins.dt.status_Khien[Array.IndexOf(shieldTypeShops, (ShieldType)curIdItem) + 1] == (int)StateItemType.buy_yet)
            {
                shopItems[i].SetLock(true);
            }
            SetEquippedBtn(DataManager.ins.dt.idShield);
            if (i < iconItemShop.Length)
            {
                SetIcon(shopItems[i].iconShop, iconItemShop[i]);
            }
        }
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.ChangeShield((ShieldType)IdSelecting);
    }

    public void StatusBtnShop(int id)
    {
        int index = Array.IndexOf(shieldTypeShops, (ShieldType)id);
        curIdItem = id;
        if (DataManager.ins.dt.status_Khien[index + 1] == (int)StateItemType.buy_yet)
        {
            curTypeBtn = ShopBtnType.buy;
            SetPrice(shieldSODatas.GetPrice((ShieldType)id));
            ActiveBtn(ShopBtnType.buy);
        }
        else
        {
            if (id == DataManager.ins.dt.idShield)
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
        if (curTypeBtn == ShopBtnType.buy)
        {
            if (DataManager.ins.dt.gold > shieldSODatas.GetPrice((ShieldType)curIdItem))
            {
                shopItems[Array.IndexOf(shieldTypeShops, (ShieldType)curIdItem) - 1].SetLock(false);
                DataManager.ins.dt.gold -= shieldSODatas.GetPrice((ShieldType)curIdItem);
                SetTextGold();
                DataManager.ins.dt.status_Khien[Array.IndexOf(shieldTypeShops, (ShieldType)curIdItem) + 1] = (int)StateItemType.buy;
                StatusBtnShop(curIdItem);
            }
        }
        else if (curTypeBtn == ShopBtnType.select)
        {
            DataManager.ins.dt.idShield = curIdItem;
            SetEquippedBtn(DataManager.ins.dt.idShield);
            StatusBtnShop(curIdItem);
            Player.ins.ChangeShield((ShieldType)curIdItem);
        }
        else
        {
            shopItems[Array.IndexOf(shieldTypeShops, (ShieldType)curIdItem) - 1].SetEquipped(false);
            DataManager.ins.dt.idShield = 0;
            Player.ins.ChangeShield(ShieldType.None);
            StatusBtnShop(curIdItem);
        }
    }
}
