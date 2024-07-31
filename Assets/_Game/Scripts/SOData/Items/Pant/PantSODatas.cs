using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantSODatas", menuName = "Scriptable Objects/PantSODatas")]

public class PantSODatas : ScriptableObject
{
    [SerializeField] PantSO[] pantDatas;

    public Material GetMat(PantType type)
    {
        for (int i = 0; i < pantDatas.Length; i++)
        {
            if (pantDatas[i].pantType == type)
            {
                return pantDatas[i].pantMat;
            }
        }
        return null;
    }

    public int GetPrice(PantType pantType)
    {
        for (int i = 0; i < pantDatas.Length; i++)
        {
            if (pantDatas[i].pantType == pantType)
            {
                return pantDatas[i].itemPrice;
            }
        }
        return -1;
    }    
}
public enum PantType
{
    None = 0,
    Batman = 10,
    Chambi = 20,
    Comy = 30,
    Dabao = 40,
    Onion = 50,
    Pokemon = 60,
    Rainbow = 70,
    Skull = 80,
    Vantim = 90
}


