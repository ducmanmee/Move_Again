using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Hat : CanvasShopFashion
{
    public static Shop_Hat ins;
    HatType[] hatTypeShops;
    [SerializeField] HatSODatas hatSODatas;

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
        GetEnumHat();
        InitializeButtons();
        StatusBtnShop(10);
    }

    public void GetEnumHat()
    {
        HatType[] enumValues = (HatType[])System.Enum.GetValues(typeof(HatType));
        hatTypeShops = new HatType[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            hatTypeShops[i] = enumValues[i];
        }
    }

    void InitializeButtons()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].idItemShop = (int)hatTypeShops[i + 1];
            if (DataManager.ins.dt.status_Hat[Array.IndexOf(hatTypeShops, (HatType)curIdItem) + 1] == (int)StateItemType.buy_yet)
            {
                shopItems[i].SetLock(true);
            }
            SetEquippedBtn(DataManager.ins.dt.idHat);
            if (i < iconItemShop.Length)
            {
                SetIcon(shopItems[i].iconShop, iconItemShop[i]);
            }
        }
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.ChangeHat((HatType)IdSelecting);
    }

    public void StatusBtnShop(int id)
    {
        int index = Array.IndexOf(hatTypeShops, (HatType)id);
        curIdItem = id;
        if (DataManager.ins.dt.status_Hat[index + 1] == (int)StateItemType.buy_yet)
        {
            curTypeBtn = ShopBtnType.buy;
            SetPrice(hatSODatas.GetPrice((HatType)id));
            ActiveBtn(ShopBtnType.buy);
        }
        else
        {
            if (id == DataManager.ins.dt.idHat)
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
        Debug.Log(curIdItem);
        if (curTypeBtn == ShopBtnType.buy)
        {
            if (DataManager.ins.dt.gold > hatSODatas.GetPrice((HatType)curIdItem))
            {
                shopItems[Array.IndexOf(hatTypeShops, (HatType)curIdItem) - 1].SetLock(false);
                DataManager.ins.dt.gold -= hatSODatas.GetPrice((HatType)curIdItem);
                SetTextGold();
                DataManager.ins.dt.status_Hat[Array.IndexOf(hatTypeShops, (HatType)curIdItem) + 1] = (int)StateItemType.buy;
                StatusBtnShop(curIdItem);
            }
        }
        else if (curTypeBtn == ShopBtnType.select)
        {
            DataManager.ins.dt.idHat = curIdItem;
            SetEquippedBtn(DataManager.ins.dt.idHat);
            StatusBtnShop(curIdItem);
            Player.ins.ChangeHat((HatType)curIdItem);
        }
        else
        {
            shopItems[Array.IndexOf(hatTypeShops, (HatType)curIdItem) - 1].SetEquipped(false);
            DataManager.ins.dt.idHat = 0;
            Player.ins.ChangeHat(HatType.None);
            StatusBtnShop(curIdItem);
        }
    }
}
