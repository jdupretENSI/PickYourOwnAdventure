using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ThumbnailUI : MonoBehaviour {

    public GameObject ButtonChoicePrefab;
    
    [Header("References")]
    public Image Image;
    public TMP_Text Description;
    public Transform Inventory;
    public Transform ChoiceContent;

    private Story _story;

    public void Setup(Story story, string storyPath = null) {
        _story = story;

        // Determine the story folder path
        if (string.IsNullOrEmpty(storyPath)) return;
        
        Thumbnail firstThumbnail = story.Thumbnails.Find(t => t.Id == story.StartingThumbnailId);
        LoadThumbnail(firstThumbnail);
    }

    public void LoadThumbnail(Thumbnail thumbnail) {
        //Any time a new thumbnail is loaded a save file should be updated.
        if (thumbnail != null) GameManager.Instance.SaveGame(thumbnail);
        
        
        // Load image from persistent data path
        string imagePath = Path.Combine(Application.persistentDataPath, "TheLostTemple", thumbnail.ImageName + ".png");
        LoadImageFromPath(imagePath);
        
        Description.text = thumbnail.Description;
        ClearChoices();
        foreach (Choice choice in thumbnail.Choices) 
        {
            GameObject instantiate = Instantiate(ButtonChoicePrefab, ChoiceContent);
            var button = instantiate.GetComponent<Button>();
            var text = instantiate.GetComponentInChildren<TMP_Text>();
    
            text.text = choice.Description;
    
            // CHECK INVENTORY REQUIREMENTS
            bool hasRequiredItems = GameManager.Instance.HasRequiredItems(choice.NeededItemsId);
            button.interactable = hasRequiredItems;
    
            if (hasRequiredItems)
            {
                button.onClick.AddListener(() => {
                    GameManager.Instance.ProcessChoiceEffects(choice); // APPLY ITEM EFFECTS
                    Thumbnail linkedThumbnail = _story.Thumbnails.Find(t => t.Id == choice.ThumbnailLinkId);
                    LoadThumbnail(linkedThumbnail);
                });
            }
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
                
            Image.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Image not found at path: {imagePath}");
        }
    }

    private void ClearChoices() {
        foreach (Transform child in ChoiceContent) {
            Destroy(child.gameObject);
        }
    }
}