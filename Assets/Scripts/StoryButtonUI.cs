using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryButtonUI : MonoBehaviour
{
    public TMP_Text Label;
    public Image ThumbnailImage;
    
    public void Setup(string label, string path)
    {
        Debug.Log(label);
        Debug.Log(Label.text);
        Label.text = label;

        if (Resources.Load<Sprite>(path))
        {
            ThumbnailImage.sprite = Resources.Load<Sprite>(path);
        }
        
    }

}
