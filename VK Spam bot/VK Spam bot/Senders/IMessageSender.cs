using VkNet.Model;

namespace SpamBot.Senders
{
    public interface IMessageSender
    {
        void SendMessage(string message, long? peerId, int count);
        void SendMessage(Message message, long? peerId, int count);
    }
}
