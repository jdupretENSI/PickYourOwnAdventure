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
    private Story _CurrentStory;
    private Thumbnail _CurrentThumbnail;
    private SaveState _CurrentSaveState = new SaveState();
    private static List<Item> _CurrentInventory = new List<Item>();
    
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
        string json = JsonUtility.ToJson(_CurrentStory);
        File.WriteAllText(FullStoryPath, json);
    }
    
    [ContextMenu("LoadStory")]
    private void LoadStory() {
        string json = File.ReadAllText(FullStoryPath);
        _CurrentStory = JsonUtility.FromJson<Story>(json);
        ThumbnailUI.Setup(_CurrentStory, StoryFolder);
    }
    

    [ContextMenu("SaveGame")]
    public void SaveGame(Thumbnail currentThumbnail) 
    {
        _CurrentThumbnail = currentThumbnail;
    
        // Direct assignment - much clearer
        _CurrentSaveState.thumbnail = _CurrentThumbnail;
        _CurrentSaveState.items = _CurrentInventory;
    
        string json = JsonUtility.ToJson(_CurrentSaveState);
        File.WriteAllText(StoryFolder + "/" + "save" + ".json", json);
    }
    
    [ContextMenu("LoadLastGame")]
    private void LoadLastGame() {
        //Get the save file
        string save = File.ReadAllText(StoryFolder + "/" + "save" + ".json");
        SaveState LastSave = JsonUtility.FromJson<SaveState>(save);
        //Load last thumbnail used
        _CurrentThumbnail = LastSave.thumbnail;
        //Get the story itself
        string json = File.ReadAllText(FullStoryPath);
        _CurrentStory = JsonUtility.FromJson<Story>(json);
        
        //We set up the entire story
        ThumbnailUI.Setup(_CurrentStory,  StoryFolder);

        try
        {
            //Give the items the player had last game.
            _CurrentInventory = LastSave.items;
        
            //And we want the story to start from where the player left off
            _CurrentThumbnail = _CurrentStory.Thumbnails.Find(t => t.Id == _CurrentThumbnail.Id);
            if (_CurrentThumbnail == null) throw new Exception("No saved thumbnail found");
        }
        catch (Exception e)
        {
            //Seems to work, but I don't see the error message? IDK who care.
            Console.WriteLine(e + "This save does not belong to this story. Starting a new story.");
            LoadStory();
        }
        
        ThumbnailUI.LoadThumbnail(_CurrentThumbnail);
    }
    
    [ContextMenu("Reset")]
    private void Reset() {
        _CurrentStory = null;
    }
    
    private bool LastGameExist()
    {
        return File.Exists(StoryFolder + "/" + "save" + ".json");
    }
    
    
    
    public bool HasRequiredItems(List<string> neededItems)
    {
        return neededItems.All(needed => _CurrentInventory.Any(item => item.Id == needed));
    }
    
    public void ProcessChoiceEffects(Choice choice)
    {
        // Remove taken items
        foreach (string takenId in choice.TakenItemsId)
        {
            _CurrentInventory.RemoveAll(item => item.Id == takenId);
        }
        
        // Add given items (you'll need to reference the story's item list)
        foreach (string givenId in choice.GivenItemsId)
        {
            var itemToAdd = _CurrentStory.Items.Find(i => i.Id == givenId);
            if (itemToAdd != null) _CurrentInventory.Add(itemToAdd);
        }
    }
}