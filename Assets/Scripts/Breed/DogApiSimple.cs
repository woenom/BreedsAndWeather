using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using DefaultNamespace;
using System.Threading;
using Zenject;

public class DogApiSimple : IDogApi
{
    private readonly RequestQueue _requestQueue;
    private readonly IBreedViewFactory _breedViewFactory;
    private readonly IBreedDetailsViewFactory _breedDetailsViewFactory;
    private readonly Transform _contentPanel;
    private readonly GameObject _loadingIndicator;

    private Guid _currentBreedListRequestId;
    private Guid _currentBreedInfoRequestId;

    private List<DefaultNamespace.BreedData> _breeds = new List<DefaultNamespace.BreedData>();
    private CancellationTokenSource _cts;

    [Inject]
    public DogApiSimple(RequestQueue requestQueue,
                       IBreedViewFactory breedViewFactory,
                       IBreedDetailsViewFactory breedDetailsViewFactory,
                       [Inject(Id = "ContentPanel")] Transform contentPanel, 
                       [Inject(Id = "BreedLoadingIndicator")] GameObject loadingIndicator)
    {
        _requestQueue = requestQueue;
        _breedViewFactory = breedViewFactory;
        _breedDetailsViewFactory = breedDetailsViewFactory;
        _contentPanel = contentPanel;
        _loadingIndicator = loadingIndicator;
        _cts = new CancellationTokenSource();
    }

    public void GetDogBreeds()
    {
        _loadingIndicator.SetActive(true);
        var request = new Request("https://dogapi.dog/api/v2/breeds");
        _currentBreedListRequestId = request.Id;
        request.OnComplete = HandleBreedListResponse;
        _requestQueue.Enqueue(request);
    }

    private void HandleBreedListResponse(string response)
    {
        try
        {
            _loadingIndicator.SetActive(false);
            BreedsResponse breedsResponse = JsonConvert.DeserializeObject<BreedsResponse>(response);

            if (breedsResponse?.Data != null)
            {
                foreach (Transform child in _contentPanel)
                {
                    GameObject.Destroy(child.gameObject);
                }
                _breeds = breedsResponse.Data.GetRange(0, Mathf.Min(10, breedsResponse.Data.Count));

                for (int i = 0; i < _breeds.Count; i++)
                {
                    var breed = _breeds[i];
                    var breedView = _breedViewFactory.Create(i + 1, breed);
                    breedView.OnBreedSelected += (breedId, loadingIndicator) =>
                    {
                        GetBreedInfo(breedId, loadingIndicator);
                    };
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка обработки ответа: " + e.Message);
        }
    }

    public void GetBreedInfo(string breedId, LoadingIndicatorView loadingIndicator)
    {
        if (_currentBreedInfoRequestId != Guid.Empty)
        {
            StopBreedRequest();
        }
        var request = new Request($"https://dogapi.dog/api/v2/breeds/{breedId}");
        _currentBreedInfoRequestId = request.Id;
        request.OnCancelled = () =>
        {
            loadingIndicator.Hide();
        };
        request.OnComplete = (response) => HandleBreedInfoResponse(response, loadingIndicator);
        _requestQueue.Enqueue(request);
    }

    private void HandleBreedInfoResponse(string response, LoadingIndicatorView loadingIndicator)
    {
        try
        {
            BreedInfoResponse breedInfoResponse = JsonConvert.DeserializeObject<BreedInfoResponse>(response);

            if (breedInfoResponse?.data?.attributes != null)
            {
                _breedDetailsViewFactory.Create(breedInfoResponse.data.attributes.name, breedInfoResponse.data.attributes.description);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка обработки ответа: " + e.Message);
        }
        finally
        {
            loadingIndicator.Hide();
        }
    }
    public void StopBreedRequest()
    {
        _requestQueue.Cancel(_currentBreedInfoRequestId);
    }
}