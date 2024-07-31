using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkinBase : MonoBehaviour
{
    [SerializeField] ShopSkinType type;
    public ShopSkinType GetType() => type;

}
