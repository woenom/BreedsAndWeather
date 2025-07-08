using Zenject;
using UnityEngine;
using Unity.VisualScripting;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private GameObject _weatherPanelPrefab;
    [SerializeField] private GameObject _breedPanelPrefab;
    [SerializeField] private GameObject _breedItemPrefab;
    [SerializeField] private Transform _contentPanel;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private GameObject _breedCardPrefab;
    [SerializeField] private GameObject _breedLoadingIndicator;
    [SerializeField] private WeatherView _weatherViewPrefab;

    public override void InstallBindings()
    {
        Container.Bind<RequestQueue>().AsSingle();
        Container.Bind<IWeatherApi>().To<WeatherController>().AsSingle().NonLazy();
        Container.Bind<IDogApi>().To<DogApiSimple>().AsSingle().NonLazy();
        Container.Bind<GameObject>().WithId("WeatherPanel").FromInstance(_weatherPanelPrefab);
        Container.Bind<GameObject>().WithId("BreedPanel").FromInstance(_breedPanelPrefab);
        Container.Bind<GameObject>().WithId("BreedLoadingIndicator").FromInstance(_breedLoadingIndicator);

        Container.Bind<IBreedViewFactory>()
            .To<BreedViewFactory>()
            .AsSingle();

        Container.Bind<IBreedDetailsViewFactory>()
            .To<BreedDetailsViewFactory>()
            .AsSingle();

        Container.Bind<GameObject>().WithId("BreedItemPrefab").FromInstance(_breedItemPrefab).AsCached();
        Container.Bind<Transform>().WithId("ContentPanel").FromInstance(_contentPanel).AsSingle();
        Container.Bind<Canvas>().WithId("MainCanvas").FromInstance(_mainCanvas).AsSingle();
        Container.Bind<GameObject>().WithId("BreedCardPrefab").FromInstance(_breedCardPrefab).AsCached();
        Container.Bind<WeatherView>().FromInstance(_weatherViewPrefab).AsCached();
    }
}