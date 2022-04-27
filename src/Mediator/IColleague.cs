namespace Mediator
{
    public interface IColleague
    {
        public string Name { get; }
        void ReceiveMessage(Message message);
    }
}
