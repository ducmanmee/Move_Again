using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopSkinType { pant = 0, hat = 1, shield = 2, fulset = 3}
public enum ShopBtnType { buy = 0, select = 1, equipped = 2}

public class CanvasShopFashion : UICanvas
{
    public ShopItemUI[] shopItems;
    public Texture2D[] iconItemShop;
    public ShopSkinBase[] shops;
    public static ShopSkinType curTypeShop;
    [SerializeField] GameObject[] shopsBtn;
    int idSelecting;
    public TMP_Text textPrice;
    protected ShopBtnType curTypeBtn;
    protected int curIdItem;

    public TMP_Text textGold;

    private void OnEnable()
    {
        SetTextGold();
        Player.ins.ChangeAnim(Constain.ANIM_DANCE);
        CameraManager.ins.SetCamShopSkin();
        SetShopSkin((int)ShopSkinType.pant);
    }

    public void SetTextGold()
    {
        textGold.text = DataManager.ins.dt.gold.ToString();
    }

    public void SetTargetBtn(int id)
    {
        for(int i=0; i<shopItems.Length; i++)
        {
            if (shopItems[i].idItemShop == id)
            {
                shopItems[i].ActiveTarget(true);
            }    
            else
            {
                shopItems[i].ActiveTarget(false);
            }
        }
    }

    public void SetEquippedBtn(int id)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].idItemShop == id)
            {
                shopItems[i].SetEquipped(true);
            }
            else
            {
                shopItems[i].SetEquipped(false);
            }
        }
    }

    public void SetIcon(Image icon, Texture2D iconTexture)
    {
        if (icon != null && iconTexture != null)
        {
            icon.sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), Vector2.zero);
            icon.SetNativeSize();
        }
    }

    public void SetShopSkin(int type)
    {
        curTypeShop = (ShopSkinType)type;
        for (int i = 0; i < shops.Length; i++)
        {
            if(type == (int)shops[i].GetType())
            {
                shops[i].gameObject.SetActive(true);
            }    
            else
            {
                shops[i].gameObject.SetActive(false);
            }
        }
    }

    public void MainMenuBtn()
    {
        UIManager.ins.CloseUI<CanvasShopFashion>();
        CanvasMainmenu menu = UIManager.ins.OpenUI<CanvasMainmenu>();
        menu.ResetBtn();
    }

    public void SetPrice(int price)
    {
        
        textPrice.text = price.ToString();
    }    

    public virtual void Btn_Click()
    {

    }    

    public ShopSkinType GetCurShop() => curTypeShop;
    public int IdSelecting
    {
        get { return idSelecting; }
        set { idSelecting = value; }
    }

    public void ActiveBtn(ShopBtnType shopBtn)
    {
        for(int i=0; i < shopsBtn.Length;i++)
        {
            if((int)shopBtn == i)
            {
                shopsBtn[i].gameObject.SetActive(true);
            }    
            else
            {
                shopsBtn[i].gameObject.SetActive(false);
            }
        }
    } 

}
