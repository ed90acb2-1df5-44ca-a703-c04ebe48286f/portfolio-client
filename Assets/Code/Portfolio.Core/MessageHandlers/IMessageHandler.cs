namespace Portfolio.Core.MessageHandlers
{
    public interface IMessageHandler<in TMessage>
    {
        void Handle(TMessage message);
    }
}
