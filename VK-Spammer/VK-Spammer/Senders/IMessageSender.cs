using VkNet.Model;

namespace Spammer.Senders
{
    public interface IMessageSender
    {
        void SendMessage(string message, long? peerId, int count);
        void SendMessage(Message message, long? peerId, int count);
    }
}
