using Spammer.Extensions;
using VkNet.Exception;
using VkNet.Model;

namespace Spammer.Senders
{
    public abstract class MessageSender : IMessageSender
    {
        public void SendMessage(string message, long? peerId, int count)
        {
            var newMessage = new Message { Text = message };
            SendMessage(newMessage, peerId, count);
        }

        public void SendMessage(Message message, long? peerId, int count)
        {
            var sentCount = 0;
            while (sentCount != count)
            {
                try 
                {
                    var commands = message.Text.Split(' ');
                    var finalMessage = commands[0] == ".spam" && int.TryParse(commands[1], out int _) ? message.Text.Format(2) : message.Text;

                    TemplateSendMessage(message, finalMessage, peerId);
                    sentCount++;
                }
                catch (VkApiException) { }
            }
        }

        protected abstract void TemplateSendMessage(string message, long? peerId);
        protected abstract void TemplateSendMessage(Message message, string messageText, long? peerId);
    }
}
