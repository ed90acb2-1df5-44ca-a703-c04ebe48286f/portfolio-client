using System;
using System.Threading.Tasks;

namespace Portfolio.Core.Net
{
    public interface IClient
    {
        void Connect();

        void PollEvents();

        void Disconnect();

        Task<TMessage> WaitFor<TMessage>() where TMessage : class;

        void RegisterHandler<TMessage>(Action<TMessage> handler) where TMessage : class, new();

        void Command<TCommand>(TCommand command) where TCommand : class;
    }
}
