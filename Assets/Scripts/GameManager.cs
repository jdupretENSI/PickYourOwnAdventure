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
    private SaveState _currentSaveState = new SaveState();
    private static List<Item> _currentInventory = new List<Item>();
    
// In GameManager.cs, update the Start method:
    private void Start()
    {
        //TODO I need to find a way to make this dynamic.
        StoryTitle = "TheLostTemple";
        StoryFolder = Application.persistentDataPath + "/" + StoryTitle;
        FullStoryPath = StoryFolder + "/" + StoryTitle + ".json";

        if (LastGameExist()) LoadLastGame(); else LoadStory();
        

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
        ThumbnailUI.Setup(_currentStory, FullStoryPath);
    }
    

    [ContextMenu("SaveGame")]
    public void SaveGame(Thumbnail currentThumbnail) 
    {
        _currentThumbnail = currentThumbnail;
    
        // Direct assignment - much clearer
        _currentSaveState.thumbnail = _currentThumbnail;
        _currentSaveState.items = _currentInventory;
    
        string json = JsonUtility.ToJson(_currentSaveState);
        File.WriteAllText(StoryFolder + "/" + "save" + ".json", json);
    }
    
    [ContextMenu("LoadLastGame")]
    private void LoadLastGame() {
        //Get the save file
        string save = File.ReadAllText(StoryFolder + "/" + "save" + ".json");
        SaveState LastSave = JsonUtility.FromJson<SaveState>(save);
        //Load last thumbnail used
        _currentThumbnail = LastSave.thumbnail;
        //Get the story itself
        string json = File.ReadAllText(FullStoryPath);
        _currentStory = JsonUtility.FromJson<Story>(json);
        
        //We set up the entire story
        ThumbnailUI.Setup(_currentStory);
        
        //Give the items the player had last game.
        _currentInventory = LastSave.items;
        
        //And we want the story to start from where the player left off
        _currentThumbnail = _currentStory.Thumbnails.Find(t => t.Id == _currentThumbnail.Id);
        
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