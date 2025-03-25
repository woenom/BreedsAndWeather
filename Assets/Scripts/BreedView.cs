using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BreedView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _num;
    [SerializeField] private TextMeshProUGUI _breedNameText;
    [SerializeField] private Button _breedButton;
    [SerializeField] private GameObject _loadingIndicator;

    private string _breedId;
    public event Action<string, LoadingIndicatorView> OnBreedSelected;

    public void Initialize(int index, DefaultNamespace.BreedData breed)
    {
        _num.text = index.ToString() + ". ";
        _breedNameText.text = breed.Attributes.Name;
        _breedId = breed.Id;

        _loadingIndicator.SetActive(false);
        _breedButton.onClick.AddListener(() =>
        {
            OnBreedSelected?.Invoke(_breedId, _loadingIndicator.GetComponent<LoadingIndicatorView>());
            _loadingIndicator.SetActive(true);
        });
    }
}