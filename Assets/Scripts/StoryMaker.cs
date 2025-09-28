using System.Collections.Generic;
using UnityEngine;

public class StoryMaker : MonoBehaviour
{

    [SerializeField] public string _saveFilePath;
    private void Start()
    {
        _saveFilePath = Application.persistentDataPath + "/Stories";
        
    }
    //New story function will ask the user to fill in a story and then will serialize it in a json.
    //TODO User interaction
    public static string NewStory()
    {
        //For now ill just make a whole new story and see if it works, later I want to be able to have a story maker in the game.
        //First we make a Choices obj.
        //We need to be able to put in as many choices as we wish
        string description;
        string ThumbnailLinkId;
        Choice choice = new Choice("This is a choice", "abc");
        
        //Seccond a Thumbnail USING that choice made
        //Each choice corresponds to a thumbnail
        string Id;
        string Image;
        string Description;

        Thumbnail thumbnail = new Thumbnail(
            "abc",
            Resources.Load<Sprite>("PNG/background/1"),
            "This is a thumbnail description",
            new List<Choice> { choice }
        );

        //Third the Story is made with BOTH the previous thumbnail and Choice
        string StartingThumbnailId;

        Story story = new Story("first story", new List<Thumbnail> { thumbnail });

        var SerializedStory = JsonUtility.ToJson(story);
        Debug.Log(SerializedStory);
        return SerializedStory;
    }
    //TODO save it to a json file
    public static void SaveStory(string story)
    {
        //In comes the json string
        //and out should go a json file to a destination
        
        
    }

    void Awake()
    {
    ReadStory(NewStory());
    }
    //TODO read from json file

    //Read story wil deserialize a json file with a story in it.
    //TODO loop through the Choices and Thumbnails as they are lists
    //TODO have it upload to the story database.
    public static void ReadStory(string story)
    {
        var DeserializedStory = JsonUtility.FromJson<Story>(story);
        
        Debug.Log(DeserializedStory.StartingThumbnailId);
        Debug.Log(DeserializedStory.Thumbnails[0]);
         Debug.Log(DeserializedStory.Thumbnails[0].Id);
         Debug.Log(DeserializedStory.Thumbnails[0].Image);
         Debug.Log(DeserializedStory.Thumbnails[0].Description);
         Debug.Log(DeserializedStory.Thumbnails[0].Choices[0]);
         Debug.Log(DeserializedStory.Thumbnails[0].Choices[0].Description);
         Debug.Log(DeserializedStory.Thumbnails[0].Choices[0].ThumbnailLinkId);
    }
}
