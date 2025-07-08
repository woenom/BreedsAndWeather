using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreedDetailsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _breedNameText;
    [SerializeField] private TextMeshProUGUI _breedDescriptionText;
    [SerializeField] private Button _closeButton;

    public void Initialize(string name, string description)
    {
        _breedNameText.text = name;
        _breedDescriptionText.text = description;

        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(() => Destroy(gameObject));
    }
}