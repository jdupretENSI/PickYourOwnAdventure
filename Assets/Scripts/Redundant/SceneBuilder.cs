using System;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;

public class SceneBuilder : MonoBehaviour
{

    public GameObject Title, Sprite, ButtonPanel, ChoicePrefab;
    public Transform ChoiceParent;

    //Whenever this class is called, it should build the entire scene using the story passed to it.

    //It will take a string (the json file deserialized) and then plug all the components in place
    public void BuildScene(Story story, [CanBeNull] string SceneID)
    {
        //Like the builder we have all the story bits going up from choices > Thumbnails > Stories
        //                                                                         Items > Stories
        
        //The function takes the story we are on, then paths through the story looking for the thumbnail with the same ID.
        //When a choice is made we go back here with the new ID rebuilding the scene.
        
        //Okay this is the general idea, come back to this when you can.
        //TODO add choices, and do a for each on the choices and instantiate new choices each time instead of modifying the value.
        //Will have to empty the zone, grab the prefab.

        
        //we need to destroy all the previous choices before building a new scene
        foreach (Transform child in ChoiceParent)
        {
            Destroy(child.gameObject);
        }
        
        //If no Scene ID then it's the beginning.
        if (SceneID == null)
        {
            Title.GetComponent<TextMeshProUGUI>().text = story.Thumbnails[0].Description;

            foreach (Choice choice in story.Thumbnails[0].Choices)
            {
                var choiceTemplate = Instantiate(ChoicePrefab, ChoiceParent);
            }
            
        }
        else
        {
            foreach (Thumbnail thumb in story.Thumbnails)
            {
                if (thumb.Id == SceneID)
                {
                    Title.GetComponent<TextMeshProUGUI>().text = thumb.Description;
                    
                    foreach (Choice choice in thumb.Choices)
                    {
                        var choiceTemplate = Instantiate(ChoicePrefab, ChoiceParent);
                    }
                }
            }
        }
        
    }
}
