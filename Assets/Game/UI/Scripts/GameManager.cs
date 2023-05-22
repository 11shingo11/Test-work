using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        saveManager = GetComponent<SaveManager>();
        instance = this;
    }

    //Resources
    public List<Sprite> weaponSprites;
    public List<Sprite> armorSprites;
    public List<InventorySlot> inventorySlots;


    //References
    public Player player;
    public Weapon weapon;
    public GameObject bulletPrefab;
    public List<GameObject> itemsPrefab;
    public Item item;
    public Enemy enemy;
    public SaveManager saveManager;

    private void Update()
    {
        item = GetComponent<Item>();
        if (player.hitpoint <= 0)
            SaveGame();
    }

    public void DeleteItemFromInventory(int slotIndex)
    {
        InventorySlot slot = inventorySlots[slotIndex];
        slot.DestroyItem();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();
        saveData.ammo = GameManager.instance.player.ammo;
        saveData.inventorySlotsData = new List<InventorySlotData>();
        List<InventorySlot> tempInventorySlots = new List<InventorySlot>(inventorySlots);
        foreach (InventorySlot slot in tempInventorySlots)
        {
            InventorySlotData slotData = new InventorySlotData();
            slotData.spriteName = slot.spriteRenderer.sprite?.name;
            slotData.itemCount = slot.itemCountText?.text;
            saveData.inventorySlotsData.Add(slotData);
        }
        saveManager.SaveGame(saveData);
    }

    public void LoadGame()
    {
        SaveData saveData = saveManager.LoadGame();
        if (saveData != null)
        {
            GameManager.instance.player.ammo = saveData.ammo;
            List<InventorySlotData> inventorySlotDataList = saveData.inventorySlotsData;
            if (inventorySlotDataList.Count == GameManager.instance.inventorySlots.Count)
            {
                for (int i = 0; i < inventorySlotDataList.Count; i++)
                {
                    InventorySlotData slotData = inventorySlotDataList[i];
                    InventorySlot slot = GameManager.instance.inventorySlots[i];
                    slot.spriteRenderer.sprite = GetSpriteByName(slotData.spriteName);
                    slot.itemCountText.text = slotData.itemCount;
                }
            }
            else
            {
                Debug.Log("Mismatch between saved inventory slot count and current inventory slot count.");
            }
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }

    private Sprite GetSpriteByName(string spriteName)
    {
        Sprite sprite = GameManager.instance.weaponSprites.Find(x => x.name == spriteName);
        if (sprite == null)
            sprite = GameManager.instance.armorSprites.Find(x => x.name == spriteName);
        return sprite;
    }
}
