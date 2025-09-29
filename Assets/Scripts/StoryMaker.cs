using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using File = System.IO.File;

public class StoryMaker : MonoBehaviour
{
    
    
    //New story function will ask the user to fill in a story and then will serialize it in a json.
    //TODO User interaction
    public static string NewStory()
    {
        //For now ill just make a whole new story and see if it works, later I want to be able to have a story maker in the game.
        //First we make a Choices obj.
        //We need to be able to put in as many choices as we wish
        string DescriptionChoice;
        // The id of the thumbnail this choice send
        string ThumbnailLinkId;
        // The list of item need to be able to select this choice
        string[] NeededItemsId;
        // The list of item given when selecting this choice
        string[] GivenItemsId;
        // The list of item remove form the inventory when selecting this choice
        string[] TakenItemsId;
        Choice choice = new Choice(
            "Description of a choice", 
            "001", 
            new List<string>{"item1", "item2"}, 
            new List<string> { "itemA", "itemB" },
            new List<string> { "item1", "item2" });
        
        //Seccond a Thumbnail USING that choice made
        //Each choice corresponds to a thumbnail
        string ThumbainId;
        // The name of the image of the thumbnail inside the folder with the extension
        // For example : "toto.png"
        string ImageNameThumbnail;
        // The name of the sfx of the thumbnail inside the folder with the extension
        // For example : "toto.flac"
        string SfxName;
        // The name of the music of the thumbnail inside the folder with the extension
        // For example : "toto.ogg"
        string MusicName;
        // The description of the thumbnail, scenery description
        string DescriptionThumbnail;
        // The list of the choices for this thumbnail
        List<Choice> Choices;

        Thumbnail thumbnail = new Thumbnail(
            "001",
            "Image name thumbnail",
            "SFX name thumbnail",
            "Music name thumbnail",
            "This is a thumbnail description",
            new List<Choice> { choice }
        );
        
        //There are also Items that the play can use and find
        // The Id of the item for reference
        string ItemId;
        // The name of the item (for label)
        string ItemName;
        // The name of the icon representative of the item in the story folder (example : sword.png)
        string IconName;
        // The description of the item (for label)
        string Description;
        Item item = new Item(
            "001",
            "Item Name",
            "Icon 1",
            "This is a item description"
            );
        

        //Third the Story is made with BOTH the previous thumbnail and Choice
        string StoryName;
        // The name of the image representative of the story (banner-like)
        string ImageNameStory;
        // The starting id of the first thumbnail
        string StartingThumbnailId;
        // The list containing all thumbnail of the story
        List<Thumbnail> Thumbnails;
        // The list containing all items of the story
        List<Progress.Item> Items;

        Story story = new Story(
            "First story",
            "Story image",
            "001",
            new List<Thumbnail> { thumbnail },
            new List<Item> {item}
            );

        var SerializedStory = JsonUtility.ToJson(story);
        SaveStory(SerializedStory, "First story");
        return SerializedStory;
    }
    
    //TODO save it to a json file
    public static void SaveStory(string story, string title)
    {
        //In comes the json string
        //and out should go a json file to Application.persistentDataPath.
        //The file should be named after its title
        //TODO maybe sanitize and  limit titles for application names?

        try
        {
            Debug.Log(Application.persistentDataPath + "/" + title + ".json");
            //System.IO.File.WriteAllText(Application.persistentDataPath + "/" + title + ".json", story);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
    
    //Read story wil deserialize a json file with a story in it.
    //TODO loop through the Choices and Thumbnails as they are lists
    //TODO have it upload to the story database.
    public static Story ReadStory(string storyName)
    {
        Story DeserializedStory;
        try
        {
            DeserializedStory = JsonUtility.FromJson<Story>(File.ReadAllText(Application.persistentDataPath + "/" + storyName + ".json"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Debug.Log(storyName);
        
        Debug.Log(DeserializedStory.StartingThumbnailId);
        Debug.Log(DeserializedStory.Thumbnails[0]);
        Debug.Log(DeserializedStory.Thumbnails[0].Id);
        Debug.Log(DeserializedStory.Thumbnails[0].ImageName);
        Debug.Log(DeserializedStory.Thumbnails[0].Description);
        Debug.Log(DeserializedStory.Thumbnails[0].Choices[0]);
        Debug.Log(DeserializedStory.Thumbnails[0].Choices[0].Description);
        Debug.Log(DeserializedStory.Thumbnails[0].Choices[0].ThumbnailLinkId);

        return DeserializedStory;
    }

    
    
    
    
    void Awake()
    {
        //NewStory();
        Story story = ReadStory("First story");
        //SetupStory(story);
    }
}
