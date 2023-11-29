using System;
using Cysharp.Threading.Tasks;
using Google.Protobuf;

namespace Portfolio.Core.Net
{
    public interface IClient
    {
        void Connect();

        void PollEvents();

        void Disconnect();

        UniTask<TMessage> WaitFor<TMessage>() where TMessage : class;

        void RegisterHandler<TMessage>(Action<TMessage> handler) where TMessage : class, IMessage, new();

        void Send<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}
