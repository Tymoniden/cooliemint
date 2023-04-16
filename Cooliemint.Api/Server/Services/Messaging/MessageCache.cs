namespace Cooliemint.Api.Server.Messaging
{
    public interface IMessageCache
    {
        void Add(MessageDto message, bool handled);
    }

    public class MessageCache : IMessageCache
    {
        public void Add(MessageDto message, bool handled)
        {

        }
    }
}
