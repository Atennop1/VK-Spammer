using VkNet;
using System;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using System.Collections.Generic;
using VkNet.Model.Keyboard;
using VkNet.Enums.SafetyEnums;

namespace SpamBot.Senders
{
    public class DefaultMessageSender : MessageSender
    {
        private readonly VkApi _api;

        public DefaultMessageSender(VkApi api)
        {
            _api = api;
        }

        public object MessageKeyboardButtonActionType { get; private set; }

        protected override void TemplateSendMessage(string message, long? peerId)
        {
            Random random = new Random();
            _api.Messages.Send(
                new MessagesSendParams()
                {
                    RandomId = random.Next(),
                    PeerId = peerId,
                    Message = message
                });
        }

        protected override void TemplateSendMessage(Message message, string messageText, long? peerId)
        {
            Random random = new Random();
            List<MediaAttachment> attachments = new List<MediaAttachment>();

            if (message.Attachments != null)
                foreach (Attachment attachment in message.Attachments)
                    attachments.Add(attachment.Instance);

            MessagesSendParams parameters = new MessagesSendParams()
            {
                RandomId = random.Next(),
                PeerId = peerId,
                Message = messageText,
                Attachments = attachments
            };

            if (message.Geo != null)
            {
                parameters.Lat = message.Geo.Coordinates.Latitude;
                parameters.Longitude = message.Geo.Coordinates.Longitude;
            }

            _api.Messages.Send(parameters);
        }
    }
}
