using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weatherText;
    [SerializeField] private Image _weatherIcon;
    [SerializeField] private Sprite _loadIcon;
    [SerializeField] private Sprite _sunnyIcon;
    [SerializeField] private Sprite _cloudyIcon;
    [SerializeField] private Sprite _rainIcon;
    [SerializeField] private Sprite _snowIcon;
    [SerializeField] private Sprite _thunderStormIcon;

    public void SetLoading()
    {
        _weatherIcon.sprite = _loadIcon;
        _weatherText.text = "Загрузка...";
    }

    public void SetWeather(string shortForecast, string temperature)
    {
        _weatherIcon.sprite = GetWeatherIcon(shortForecast);
        _weatherText.text = "Сегодня: " + temperature;
    }
    public Sprite GetWeatherIcon(string weatherDescription)
    {
        switch (weatherDescription)
        {
            case "Sunny":
            case "Partly Sunny":
            case "Mostly Sunny":
            case "Mostly Clear":
            case "Clear":
            case "Fair":
            case "Very Hot":
            case "Hot":
            case "Very Cold":
            case "Variable Clouds":
                return _sunnyIcon;
            case "Cloudy":
            case "Partly Cloudy":
            case "Mostly Cloudy":
            case "Overcast":
            case "Fog":
            case "Fog late":
            case "Fog a.m.":
                return _cloudyIcon;
            case "Rain":
            case "Mix":
            case "Chance rain":
            case "Scattered showers":
            case "Isolated showers":
            case "Chance showers":
            case "Chance Rain Showers":
            case "Showers likely":
            case "Rain likely":
            case "Rain or Snow":
            case "Rain and Snow":
            case "Chance Snow/Rain":
            case "Freezing Rain":
            case "Rain Sleet":
                return _rainIcon;
            case "Snow":
            case "Sleet":
            case "Snow showers":
                return _snowIcon;
            case "ThunderStorm":
            case "Showers storms":
            case "Severe storm":
                return _thunderStormIcon;
            default:
                Debug.LogWarning("Неизвестный прогноз погоды: " + weatherDescription);
                return null; // Или иконка по умолчанию
        }
    }
}
