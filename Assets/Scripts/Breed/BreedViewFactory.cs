using Zenject;
using UnityEngine;

public interface IBreedViewFactory
{
    BreedView Create(int index, DefaultNamespace.BreedData breed);
}

public class BreedViewFactory : IBreedViewFactory
{
    private readonly DiContainer _container;
    private readonly GameObject _breedItemPrefab;
    private readonly Transform _contentPanel;

    [Inject]
    public BreedViewFactory(DiContainer container,
                             [Inject(Id = "BreedItemPrefab")] GameObject breedItemPrefab,
                             [Inject(Id = "ContentPanel")] Transform contentPanel)
    {
        _container = container;
        _breedItemPrefab = breedItemPrefab;
        _contentPanel = contentPanel;
    }

    public BreedView Create(int index, DefaultNamespace.BreedData breed)
    {
        var breedItem = _container.InstantiatePrefab(_breedItemPrefab, _contentPanel);
        var breedView = breedItem.GetComponent<BreedView>();
        breedView.Initialize(index, breed);
        return breedView;
    }
}