using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Linq;
using Object = UnityEngine.Object;

public class StoriesLister : MonoBehaviour
{
    private List<Story> story;
    public GameObject StoryPrefab;
    public Transform storyParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        foreach (DirectoryInfo dir in info.GetDirectories())
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension == ".json")
                {
                    Debug.Log(Deserialize.ReadStory(file.ToString()));
                    story.Add(Deserialize.ReadStory(file.ToString()));
                }
            }
        }
        
        StoryLister(story);
    }


    //This function is meant to build a list of stories from the users local repository
    //We should go check persistent data path and for any stories found in there we build a new story.
    //Each story should use the Story title as its name
    //DONE if possible add the thumbnail in the story to the story.

    public void StoryLister(List<Story> storyList)
    {
        foreach (Story story in storyList)
        {
            // Instantiate the prefab
            var buttonInstance = Instantiate(StoryPrefab, storyParent);
            
            // Get the StoryButtonUI component from the instantiated prefab
            StoryButtonUI storyButton = buttonInstance.GetComponent<StoryButtonUI>();
            
            if (storyButton != null)
            {
                // Call Setup on the specific instance
                storyButton.Setup(story.StoryName, "RPG Avatars people icons (bg)");
            }
            else
            {
                Debug.LogError("StoryButtonUI component not found on prefab!");
            }
        }
        
    }
}
