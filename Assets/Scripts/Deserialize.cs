using System;
using System.IO;
using UnityEngine;

public class Deserialize : MonoBehaviour
{
    //Read story wil deserialize a json file with a story in it.
    
    //StoryName should be the entire path to the story.
    public static Story ReadStory(string StoryName)
    {
        Story DeserializedStory;
        try
        {
            DeserializedStory =
                JsonUtility.FromJson<Story>(
                    File.ReadAllText(StoryName));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return DeserializedStory;
    }
}
