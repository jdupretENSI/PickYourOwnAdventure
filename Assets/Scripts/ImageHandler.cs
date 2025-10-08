using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler
{
    public static void LoadImageFromPath(Image image, string imagePath)
    {
        if (File.Exists(imagePath))
        {
            byte[] imageData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
                
            Sprite sprite = Sprite.Create(texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f));
                    
            image.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Image not found at path: {imagePath}");
        }
    }
        
}
