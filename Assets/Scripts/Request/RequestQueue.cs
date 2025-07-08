using System;
using System.Collections.Generic;

public class RequestQueue
{
    private readonly Queue<IRequest> _requestQueue = new Queue<IRequest>();
    private bool _isProcessing;
    private IRequest _currentRequest;

    public void Enqueue(IRequest request)
    {
        _requestQueue.Enqueue(request);
        ProcessQueue();
    }
    private async void ProcessQueue()
    {
        if (_isProcessing) return;
        _isProcessing = true;

        while (_requestQueue.Count > 0)
        {
            _currentRequest = _requestQueue.Dequeue();
            await _currentRequest.Execute();
            _currentRequest = null;
        }

        _isProcessing = false;
    }

    public void Cancel(Guid requestId)
    {
        if (_currentRequest != null && _currentRequest.Id == requestId)
        {
            _currentRequest.Cancel();
            _currentRequest = null;
            _isProcessing = false;
            ProcessQueue();
        }
        else
        {
            IRequest requestToRemove = null;
            foreach (var request in _requestQueue)
            {
                if (request.Id == requestId)
                {
                    requestToRemove = request;
                    break;
                }
            }

            if (requestToRemove != null)
            {
                _requestQueue.Clear();
            }
        }
    }
}