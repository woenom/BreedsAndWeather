using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public abstract class BaseRequest : IRequest
{
    public Guid Id { get; } = Guid.NewGuid();
    public Action<string> OnComplete { get; set; }
    public Action OnCancelled { get; set; }
    protected bool _isCancelled;
    protected UnityWebRequest _webRequest;

    public abstract Task Execute();

    public virtual void Cancel()
    {
        _webRequest.Abort();
        _webRequest.Dispose();
        _isCancelled = true;
        OnCancelled?.Invoke();
    }
}