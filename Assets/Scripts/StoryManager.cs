using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private Image  _image;
    [SerializeField] private Transform _buttonContent;
    [SerializeField] private Button _button;
    

    private void Awake()
    {
        GameObject instantiate = Instantiate(_buttonPrefab, _buttonContent);
        instantiate.GetComponent<Button>();
        instantiate.GetComponentInChildren<TMP_Text>().text = "New Story";

        Instantiate(instantiate, _buttonPanel.transform);


    }

}
