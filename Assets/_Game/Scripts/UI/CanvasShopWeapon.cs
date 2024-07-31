using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CanvasShopWeapon : UICanvas
{
    [SerializeField] WeaponSODatas weaponSODatas;
    List<WeaponShop> weaponShops = new List<WeaponShop>();
    List<float> weaponShopsPrice = new List<float>();
    public Transform holderWeaponTF;
    WeaponShop curWeaponShop;
    int index;

    public TMP_Text textGold;
    public TMP_Text price;
    [SerializeField] GameObject[] shopsBtn;
    ShopBtnType curTypeBtn;

    private void OnEnable()
    {
        SetTextGold();
        Player.ins.gameObject.SetActive(false);
        GetListWPShop();
        index = 0;
        if (curWeaponShop  != null)
        {
            Destroy(curWeaponShop.gameObject);
        }    
        curWeaponShop = Instantiate(weaponSODatas.GetPrefabShop(WeaponType.Arrow), holderWeaponTF);
        StatusBtnShop();
    }

    public void SetTextGold()
    {
        textGold.text = DataManager.ins.dt.gold.ToString();
    }

    public void GetListWPShop()
    {
        WeaponType[] weaponTypes = (WeaponType[])Enum.GetValues(typeof(WeaponType));

        for (int i = 0; i < weaponTypes.Length; i++)
        {
            WeaponType value = weaponTypes[i];
            if (value == WeaponType.None) continue;
            weaponShops.Add(weaponSODatas.GetPrefabShop(value));
            weaponShopsPrice.Add(weaponSODatas.GetPriceWeapon(value));
        }
    }

    public void MainMenuBtn()
    {
        if (curWeaponShop != null)
        {
            Destroy(curWeaponShop.gameObject);
        }
        UIManager.ins.CloseUI<CanvasShopWeapon>();
        CanvasMainmenu menu = UIManager.ins.OpenUI<CanvasMainmenu>();
        menu.ResetBtn();
    }

    public void ChangeBtn(int i)
    {
        if(curWeaponShop != null)
        {
            Destroy(curWeaponShop.gameObject);
        }

        index += i;
        int weaponCount = weaponShops.Count;
        if (index < 0)
        {
            index = weaponCount - 1;
        }
        else if (index >= weaponCount)
        {
            index = 0;
        }

        curWeaponShop = Instantiate(weaponShops[index], holderWeaponTF);
        StatusBtnShop();
    }

    public void SetPrice()
    {
        price.text = weaponShopsPrice[index].ToString();
    }    

    public void StatusBtnShop()
    {
        if (DataManager.ins.dt.status_Weapon[index] == (int)StateItemType.buy_yet)
        {
            curTypeBtn = ShopBtnType.buy;
            SetPrice();
        }   
        else
        {
            if ((int)weaponSODatas.GetWeaponType(index) == DataManager.ins.dt.idWeapon)
            {
                curTypeBtn = ShopBtnType.equipped;
            }
            else
            {
                curTypeBtn = ShopBtnType.select;
            }
        }  
        ActiveBtn(curTypeBtn);

    }

    public void ActiveBtn(ShopBtnType shopBtn)
    {
        for (int i = 0; i < shopsBtn.Length; i++)
        {
            if ((int)shopBtn == i)
            {
                shopsBtn[i].gameObject.SetActive(true);
            }
            else
            {
                shopsBtn[i].gameObject.SetActive(false);
            }
        }
    }

    public void BuyBtn()
    {
        if (curTypeBtn == ShopBtnType.buy)
        {
            if (DataManager.ins.dt.gold > weaponSODatas.GetPriceWeapon(weaponSODatas.GetWeaponType(index)))
            {
                DataManager.ins.dt.gold -= weaponSODatas.GetPriceWeapon(weaponSODatas.GetWeaponType(index));
                SetTextGold();
                DataManager.ins.dt.status_Weapon[index] = (int)StateItemType.buy;
                StatusBtnShop();
            }
        }
        else if (curTypeBtn == ShopBtnType.select)
        {
            DataManager.ins.dt.idWeapon = (int)weaponSODatas.GetWeaponType(index);
            StatusBtnShop();
            Player.ins.ChangeWeapon(weaponSODatas.GetWeaponType(index));
        }
    }
}
