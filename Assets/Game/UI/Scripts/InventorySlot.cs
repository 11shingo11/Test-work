using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Text itemCountText;
    public int freeIndex = -1;
    public int currentSlotIndex;
    public int slotIndex;


    void Start()
    {
        slotIndex = GameManager.instance.inventorySlots.IndexOf(this);
    }

    public void SetItem(Sprite sprite, int itemCount)
    {
        spriteRenderer.sprite = sprite;
        if (itemCount>1)
            itemCountText.text = itemCount.ToString();
        else itemCountText.text = string.Empty;
    }

    private void EndOfItem()
    {
        if (GameManager.instance.player.ammo == 0)
        {
            GameManager.instance.inventorySlots[GameManager.instance.player.ammoSlot].SetItem(null, 1);
            freeIndex = GameManager.instance.player.ammoSlot;
            GameManager.instance.player.ammoSlot = -1;
            GameManager.instance.player.invCount -= 1;
            if (freeIndex < GameManager.instance.player.makarovSlot)
            {
                string itemCountString = GameManager.instance.inventorySlots[GameManager.instance.player.makarovSlot].itemCountText.text;
                Sprite tmpSprite = GameManager.instance.inventorySlots[GameManager.instance.player.makarovSlot].spriteRenderer.sprite;
                if (int.TryParse(itemCountString, out int tmpItemCount))
                {
                    GameManager.instance.inventorySlots[GameManager.instance.player.makarovSlot].SetItem(null, 1);
                    GameManager.instance.player.makarovSlot -= 1;
                    GameManager.instance.inventorySlots[GameManager.instance.player.makarovSlot].SetItem(tmpSprite, tmpItemCount);
                }
                else
                {
                    GameManager.instance.inventorySlots[GameManager.instance.player.makarovSlot].SetItem(null, 1);
                    GameManager.instance.player.makarovSlot -= 1;
                    GameManager.instance.inventorySlots[GameManager.instance.player.makarovSlot].SetItem(tmpSprite, 1);
                }    
                
                
            }

            if (freeIndex < GameManager.instance.player.akSlot)
            {
                string itemCountString = GameManager.instance.inventorySlots[GameManager.instance.player.akSlot].itemCountText.text;
                Sprite tmpSprite = GameManager.instance.inventorySlots[GameManager.instance.player.akSlot].spriteRenderer.sprite;
                if (int.TryParse(itemCountString, out int tmpItemCount))
                {
                    GameManager.instance.inventorySlots[GameManager.instance.player.akSlot].SetItem(null, 1);
                    GameManager.instance.player.akSlot -= 1;
                    GameManager.instance.inventorySlots[GameManager.instance.player.akSlot].SetItem(tmpSprite, tmpItemCount);
                }
                else
                {
                    GameManager.instance.inventorySlots[GameManager.instance.player.akSlot].SetItem(null, 1);
                    GameManager.instance.player.akSlot -= 1;
                    GameManager.instance.inventorySlots[GameManager.instance.player.akSlot].SetItem(tmpSprite, 1);
                }
            }

            if (freeIndex < GameManager.instance.player.helmSlot)
            {
                string itemCountString = GameManager.instance.inventorySlots[GameManager.instance.player.helmSlot].itemCountText.text;
                Sprite tmpSprite = GameManager.instance.inventorySlots[GameManager.instance.player.helmSlot].spriteRenderer.sprite;
                if (int.TryParse(itemCountString, out int tmpItemCount))
                {
                    GameManager.instance.inventorySlots[GameManager.instance.player.helmSlot].SetItem(null, 1);
                    GameManager.instance.player.helmSlot -= 1;
                    GameManager.instance.inventorySlots[GameManager.instance.player.helmSlot].SetItem(tmpSprite, tmpItemCount);
                }
                else
                {
                    GameManager.instance.inventorySlots[GameManager.instance.player.helmSlot].SetItem(null, 1);
                    GameManager.instance.player.helmSlot -= 1;
                    GameManager.instance.inventorySlots[GameManager.instance.player.helmSlot].SetItem(tmpSprite, 1);
                }
            }


        }
    }

    private void LateUpdate()
    {
        if(GameManager.instance.player.ammoSlot>=0)
            EndOfItem();
    }

    public void OnYesButtonClick(int slotIndex)
    {
        GameManager.instance.DeleteItemFromInventory(slotIndex);
    }

    public void DestroyItem()
    {
        if ((itemCountText.text) == "")
        {
            spriteRenderer.sprite = null;
            itemCountText.text = string.Empty;
        }
        else 
        {
            if (int.Parse(itemCountText.text) - 1 > 1)
                itemCountText.text = (int.Parse(itemCountText.text) - 1).ToString();
            else
                itemCountText.text = string.Empty;
        }           
    }


}
