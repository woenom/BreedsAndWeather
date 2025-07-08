using System;
using System.Threading.Tasks;

public interface IRequest
{
    Guid Id { get; }
    Task Execute();
    void Cancel();
    Action<string> OnComplete { get; set; }
}