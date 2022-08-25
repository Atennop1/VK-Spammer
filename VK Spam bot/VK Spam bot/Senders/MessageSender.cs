using VkNet.Exception;
using VkNet.Model;
using SpamBot.Extensions;

namespace SpamBot.Senders
{
    public abstract class MessageSender : IMessageSender
    {
        public void SendMessage(string message, long? peerId, int count)
        {
            Message newMessage = new Message { Text = message };
            SendMessage(newMessage, peerId, count);
        }

        public void SendMessage(Message message, long? peerId, int count)
        {
            int sendedCount = 0;
            while (sendedCount != count)
            {
                try 
                {
                    string[] commands = message.Text.Split(' ');
                    string finalMessage = commands[0] == ".spam" && int.TryParse(commands[1], out int _) ? message.Text.Format(2) : message.Text;

                    TemplateSendMessage(message, finalMessage, peerId);
                    sendedCount++;
                }
                catch (VkApiException) { continue; }
            }
        }

        protected abstract void TemplateSendMessage(string message, long? peerId);
        protected abstract void TemplateSendMessage(Message message, string messageText, long? peerId);
    }
}
