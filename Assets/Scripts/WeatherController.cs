using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class WeatherController : IWeatherApi
{
    private readonly RequestQueue _requestQueue;
    private readonly WeatherView _weatherView;
    private CancellationTokenSource _cts;

    [Inject]
    public WeatherController(RequestQueue requestQueue, WeatherView weatherView)
    {
        _requestQueue = requestQueue;
        _weatherView = weatherView;
    }

    public async void GetWeather()
    {
        _cts = new CancellationTokenSource();
        _weatherView.SetLoading();

        while (true)
        {
            var request = new Request("https://api.weather.gov/gridpoints/TOP/32,81/forecast");
            request.OnComplete = HandleWeatherResponse;
            _requestQueue.Enqueue(request);
            try
            {
                await UniTask.Delay(5000, cancellationToken: _cts.Token);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }

    }
    public void StopWeatherUpdates()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
    private void HandleWeatherResponse(string response)
    {
        try
        {
            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            if (weatherResponse?.properties?.periods != null && weatherResponse.properties.periods.Count > 0)
            {
                Period currentPeriod = weatherResponse.properties.periods[0];
                _weatherView.SetWeather(currentPeriod.shortForecast, currentPeriod.temperature + currentPeriod.temperatureUnit);
            }
            else
            {
                Debug.LogError("Не удалось получить данные о погоде");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка обработки ответа: " + e.Message);
        }
    }
}