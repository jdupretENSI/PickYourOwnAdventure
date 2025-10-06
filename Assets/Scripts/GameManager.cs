using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Utils;

public class GameManager : MonoBehaviourSingleton<GameManager> {

    public ThumbnailUI ThumbnailUI;

    //Assuming the story folder and title are named the same this should allow me to use them freely.
    private string StoryFolder;
    private string StoryTitle;
    private string FullStoryPath;
    private Story _currentStory;
    private Thumbnail _currentThumbnail;
    
// In GameManager.cs, update the Start method:
    private void Start()
    {
        //TODO I need to find a way to make this dynamic.
        StoryTitle = "TheLostTemple";
        StoryFolder = Application.persistentDataPath + "/" + StoryTitle;
        FullStoryPath = StoryFolder + "/" + StoryTitle + ".json";

        if (LastGameExist()) LoadLastGame(); else LoadStory();

        // If I want to start a specific story directly:
        if (_currentStory == null) {
            // Load a story from your StoriesLister or create one
            _currentStory = Deserialize.ReadStory(FullStoryPath);
        }

        // Pass the story path to ThumbnailUI
        ThumbnailUI.Setup(_currentStory, FullStoryPath);
    }

    [ContextMenu("NewSave")]
    private void NewSave() {
        string json = JsonUtility.ToJson(_currentStory);
        File.WriteAllText(FullStoryPath, json);
    }
    
    [ContextMenu("LoadStory")]
    private void LoadStory() {
        string json = File.ReadAllText(FullStoryPath);
        _currentStory = JsonUtility.FromJson<Story>(json);
        ThumbnailUI.Setup(_currentStory);
    }
    

    [ContextMenu("SaveGame")]
    private void SaveGame() {
        string json = JsonUtility.ToJson(_currentThumbnail);
        File.WriteAllText(StoryFolder, json);
    }
    
    [ContextMenu("LoadLastGame")]
    private void LoadLastGame() {
        string save = File.ReadAllText(StoryFolder + "/" + "save" + ".json");
        _currentThumbnail = JsonUtility.FromJson<Thumbnail>(save);
        ThumbnailUI.LoadThumbnail(_currentThumbnail);
    }
    
    [ContextMenu("Reset")]
    private void Reset() {
        _currentStory = null;
    }
    
    private bool LastGameExist()
    {
        return File.Exists(StoryFolder + "/" + "save" + ".json");
    }
    
    [Serializable]
    public class SaveData {
        public string CurrentThumbnailId;
        public List<string> InventoryItemIds;
    }

    public void SaveProgress(string currentThumbnailId) {
        SaveData save = new SaveData {
            CurrentThumbnailId = currentThumbnailId,
            InventoryItemIds = _currentInventory.Select(item => item.Id).ToList()
        };
        File.WriteAllText(StoryFolder + "/save.json", 
            JsonUtility.ToJson(save));
    }
    
    private static List<Item> _currentInventory = new List<Item>();
    
    public bool HasRequiredItems(List<string> neededItems)
    {
        return neededItems.All(needed => _currentInventory.Any(item => item.Id == needed));
    }
    
    public void ProcessChoiceEffects(Choice choice)
    {
        // Remove taken items
        foreach (string takenId in choice.TakenItemsId)
        {
            _currentInventory.RemoveAll(item => item.Id == takenId);
        }
        
        // Add given items (you'll need to reference the story's item list)
        foreach (string givenId in choice.GivenItemsId)
        {
            var itemToAdd = _currentStory.Items.Find(i => i.Id == givenId);
            if (itemToAdd != null) _currentInventory.Add(itemToAdd);
        }
    }
}