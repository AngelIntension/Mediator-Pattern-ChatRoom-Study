namespace Mediator
{
    public interface IMessageWriter<TMessage>
    {
        void Write(Message message);
    }
}
