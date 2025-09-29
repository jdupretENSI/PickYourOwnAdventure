using System;
using System.IO;
using UnityEngine;

public class Deserialize : MonoBehaviour
{
    //Read story wil deserialize a json file with a story in it.
    //TODO loop through the Choices and Thumbnails as they are lists
    public static Story ReadStory(string storyName)
    {
        Story DeserializedStory;
        try
        {
            DeserializedStory =
                JsonUtility.FromJson<Story>(
                    File.ReadAllText(Application.persistentDataPath + "/" + storyName + ".json"));
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
}
