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
        Debug.Log(label);
        Debug.Log(Label.text);
        Label.text = label;

        if (!string.IsNullOrEmpty(imageName))
        {
            string imagePath = Path.Combine(storyFolderPath, imageName);
            LoadImageFromPath(imagePath);
        }
    }

    private void LoadImageFromPath(string imagePath)
    {
        if (File.Exists(imagePath))
        {
            byte[] imageData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            
            Sprite sprite = Sprite.Create(texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f));
                
            ThumbnailImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Story image not found at path: {imagePath}");
        }
    }
}