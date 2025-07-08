using UnityEngine;
using Zenject;

public class AppController : MonoBehaviour
{
    private IWeatherApi _weatherController;
    private IDogApi _dogApiSimple;
    private GameObject _weatherPanel;
    private GameObject _breedPanel;

    [Inject]
    public void Initialize(IWeatherApi weatherController, IDogApi dogApiSimple,
                      [Inject(Id = "WeatherPanel")] GameObject weatherPanel,
                      [Inject(Id = "BreedPanel")] GameObject breedPanel)
    {
        _weatherController = weatherController;
        _dogApiSimple = dogApiSimple;
        _weatherPanel = weatherPanel;
        _breedPanel = breedPanel;
    }

    public void ActivateWeatherTab()
    {
        _dogApiSimple.StopBreedRequest();
        _weatherPanel.SetActive(true);
        _breedPanel.SetActive(false);
        _weatherController.GetWeather();
    }

    public void ActivateBreedTab()
    {
        _weatherController.StopWeatherUpdates();
        _weatherPanel.SetActive(false);
        _breedPanel.SetActive(true);
        _dogApiSimple.GetDogBreeds();
    }
}