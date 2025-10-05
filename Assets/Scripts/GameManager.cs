using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ThumbnailUI ThumbnailUI;
    public static Story CurrentStory;
    public string StoryPath;
    
    private void Start() {
        StoryPath = Application.persistentDataPath + "/First story/First story.json";
        Load();
    
        // If I want to start a specific story directly:
        if (CurrentStory == null) {
            // Load a story from your StoriesLister or create one
            CurrentStory = Deserialize.ReadStory(StoryPath);
        }
    
        ThumbnailUI.Setup(CurrentStory);
    }

    [ContextMenu("Save")]
    private void Save() {
        string json = JsonUtility.ToJson(CurrentStory);
        File.WriteAllText(StoryPath, json);
    }
    
    [ContextMenu("Load")]
    private void Load() {
        string json = File.ReadAllText(StoryPath);
        CurrentStory = JsonUtility.FromJson<Story>(json);
        Debug.Log(CurrentStory);
        ThumbnailUI.Setup(CurrentStory);
    }
    
    [ContextMenu("Reset")]
    private void Reset() {
        CurrentStory = null;
    }
    
    [Serializable]
    public class SaveData {
        public string CurrentThumbnailId;
        public List<string> InventoryItemIds;
    }

    public static void SaveProgress(string currentThumbnailId) {
        SaveData save = new SaveData {
            CurrentThumbnailId = currentThumbnailId,
            InventoryItemIds = _currentInventory.Select(item => item.Id).ToList()
        };
        File.WriteAllText(Application.persistentDataPath + "/save.json", 
            JsonUtility.ToJson(save));
    }
    
    private static List<Item> _currentInventory = new List<Item>();
    
    public static bool HasRequiredItems(List<string> neededItems)
    {
        return neededItems.All(needed => _currentInventory.Any(item => item.Id == needed));
    }
    
    public static void ProcessChoiceEffects(Choice choice)
    {
        // Remove taken items
        foreach (string takenId in choice.TakenItemsId)
        {
            _currentInventory.RemoveAll(item => item.Id == takenId);
        }
        
        // Add given items (you'll need to reference the story's item list)
        foreach (string givenId in choice.GivenItemsId)
        {
            var itemToAdd = CurrentStory.Items.Find(i => i.Id == givenId);
            if (itemToAdd != null) _currentInventory.Add(itemToAdd);
        }
    }
}