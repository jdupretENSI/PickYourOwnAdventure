using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StoryButtonUI : MonoBehaviour
{
    public TMP_Text Label;
    public Image ThumbnailImage;
    
    public void Setup(string label, string storyFolderPath, string imageName)
    {
        Label.text = label;

        if (!string.IsNullOrEmpty(imageName))
        {
            string imagePath = Path.Combine(storyFolderPath, imageName);
            ImageHandler.LoadImageFromPath(ThumbnailImage ,imagePath);
        }
    }
    
}