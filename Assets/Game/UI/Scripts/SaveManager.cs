using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

[System.Serializable]
public class SaveData
{
    public List<InventorySlotData> inventorySlotsData;
    public float hp;
    public int ammo;
    public string spriteName;
    public string itemCount;
}
[System.Serializable]
public class InventorySlotData
{
    public string spriteName;
    public string itemCount;
    public override string ToString()
    {
        return $"Sprite Name: {spriteName}, Item Count: {itemCount}";
    }
}


public class SaveManager : MonoBehaviour
{
    private string savePath;
    private SaveData saveData;

    private void Awake()
    {
        savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "saveData.json");
    }

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved.");
    }

    public SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded.");
            return data;
        }
        else
        {
            Debug.Log("Save file not found.");
            return null;
        }
    }


    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }
}
