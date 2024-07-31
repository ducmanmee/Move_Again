using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]

public class ItemsSO : ScriptableObject
{
    [Header("Common Item Properties")]
    public string itemName;
    public int itemPrice;
    public ItemType itemType;
    public StateItemType state;
}

public enum ItemType
{
    None,
    Pant,
    Hat,
    Shield,
    FulSkin
}

public enum StateItemType
{
    buy_yet = 0,
    buy = 1
}
