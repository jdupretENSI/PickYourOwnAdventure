using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class ThumbnailUI : MonoBehaviour {

    public GameObject ButtonChoicePrefab;
    
    [Header("References")]
    public Image Image;
    public TMP_Text Description;
    public Transform Inventory;
    public Transform ChoiceContent;
    
    [Header("Audio References")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    
    private string _StoryFolder;
    private string _AudioPath;
    private Story _story;

    public void Setup(Story story, string storyPath) {
        _story = story;
        
        _StoryFolder = storyPath;
        Thumbnail firstThumbnail = story.Thumbnails.Find(t => t.Id == story.StartingThumbnailId);
        LoadThumbnail(firstThumbnail);
    }

    public void LoadThumbnail(Thumbnail thumbnail) {
        //Any time a new thumbnail is loaded a save file should be updated.
        if (thumbnail != null) GameManager.Instance.SaveGame(thumbnail);
        
        
        // Load image from persistent data path
        string imagePath = Path.Combine(Application.persistentDataPath, "TheLostTemple", thumbnail.ImageName + ".png");
        LoadImageFromPath(imagePath);
        
        //SFX and Music.
        if(AudioHandler.GetAudioTypeString(Path.Combine(_StoryFolder, thumbnail.SfxName)) != null)
        {
            //Normally the check under should stop things, but I think the thumbnails were given names even if there was no file associated.
            //SFX plays ONCE only when you get onto this thumbnail.
            if (!string.IsNullOrEmpty(thumbnail.SfxName))
            {
                string AudioExtention = AudioHandler.GetAudioTypeString(Path.Combine(_StoryFolder, thumbnail.SfxName));
                _AudioPath = Path.Combine(_StoryFolder, thumbnail.SfxName + AudioExtention);
                StartCoroutine(LoadAudioCoroutine(_AudioPath, sfxAudioSource, false));
            }
        }
        else
        {
            sfxAudioSource.Stop();
        }

        if (AudioHandler.GetAudioTypeString(Path.Combine(_StoryFolder, thumbnail.MusicName)) != null)
        {
            //Same problem here
            //Music plays on loop whilst on the thumbnail.
            if (!string.IsNullOrEmpty(thumbnail.MusicName))
            {
                string AudioExtention = AudioHandler.GetAudioTypeString(Path.Combine(_StoryFolder, thumbnail.MusicName));
                _AudioPath = Path.Combine(_StoryFolder, thumbnail.SfxName + AudioExtention);
                StartCoroutine(LoadAudioCoroutine(_AudioPath, musicAudioSource, true));
            }
            
        }
        else
        {
            musicAudioSource.Stop();
        }
        
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

    private IEnumerator LoadAudioCoroutine(string filePath, AudioSource targetSource, bool loop)
    {
        //Checks if we are on windows and assigns the correct path.
        string audioPath = (Application.platform == RuntimePlatform.WindowsPlayer || 
                            Application.platform == RuntimePlatform.WindowsEditor)
            ? "file:///" + filePath.Replace("\\", "/")
            : "file://" + filePath;
        
        
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioPath, AudioHandler.GetAudioType(filePath)))
        {
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                targetSource.clip = clip;
                targetSource.loop = loop;
                targetSource.Play();
            }
            else
            {
                Debug.LogWarning($"Audio file not found: {filePath}");
            }
        }
    }
    

    private void ClearChoices() {
        foreach (Transform child in ChoiceContent) {
            Destroy(child.gameObject);
        }
    }
}