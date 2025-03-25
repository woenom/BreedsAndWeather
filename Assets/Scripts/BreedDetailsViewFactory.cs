using Zenject;
using UnityEngine;

public interface IBreedDetailsViewFactory
{
    void Create(string name, string description);
}

public class BreedDetailsViewFactory : IBreedDetailsViewFactory
{
    private readonly DiContainer _container;
    private readonly GameObject _breedCardPrefab;
    private readonly Canvas _mainCanvas;

    [Inject]
    public BreedDetailsViewFactory(DiContainer container,
                                   [Inject(Id = "BreedCardPrefab")] GameObject breedCardPrefab,
                                   [Inject(Id = "MainCanvas")] Canvas mainCanvas)
    {
        _container = container;
        _breedCardPrefab = breedCardPrefab;
        _mainCanvas = mainCanvas;
    }

    public void Create(string name, string description)
    {
        var breedCard = _container.InstantiatePrefab(_breedCardPrefab, _mainCanvas.transform);
        var breedDetailsView = breedCard.GetComponent<BreedDetailsView>();
        breedDetailsView.Initialize(name, description);
    }
}