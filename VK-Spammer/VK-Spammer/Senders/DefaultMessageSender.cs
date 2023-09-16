using System;
using System.Collections.Generic;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Spammer.Senders
{
    public class DefaultMessageSender : MessageSender
    {
        private readonly VkApi _api;

        public DefaultMessageSender(VkApi api) 
            => _api = api;

        protected override void TemplateSendMessage(string message, long? peerId)
        {
            var random = new Random();

            _api.Messages.Send(new MessagesSendParams
            {
                RandomId = random.Next(),
                PeerId = peerId,
                Message = message
            });
        }

        protected override void TemplateSendMessage(Message message, string messageText, long? peerId)
        {
            var random = new Random();
            var attachments = new List<MediaAttachment>();

            if (message.Attachments != null)
                foreach (var attachment in message.Attachments)
                    attachments.Add(attachment.Instance);

            var parameters = new MessagesSendParams
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
