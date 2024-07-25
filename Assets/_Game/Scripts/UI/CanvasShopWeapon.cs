using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanvasShopWeapon : UICanvas
{
    [SerializeField] WeaponSODatas weaponSODatas;
    List<WeaponShop> weaponShops = new List<WeaponShop>();
    public Transform holderWeaponTF;
    WeaponShop curWeaponShop;
    int index;

    private void Start()
    {
        GetListWPShop();
        index = 0;
    }

    private void OnEnable()
    {
        curWeaponShop = Instantiate(weaponSODatas.GetPrefabShop(WeaponType.Arrow), holderWeaponTF);
    }

    public void GetListWPShop()
    {
        foreach (WeaponType value in Enum.GetValues(typeof(WeaponType)))
        {
            if(value == WeaponType.None) continue;
            weaponShops.Add(weaponSODatas.GetPrefabShop(value));
        }
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
            index = weaponCount;
        }
        else if (index >= weaponCount)
        {
            index = 0;
        }

        curWeaponShop = Instantiate(weaponShops[index], holderWeaponTF);
    }    
}
