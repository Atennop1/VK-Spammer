using System;
using Spammer.Senders;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace Spammer
{
    [Obsolete]
    public class Bot
    {
        private readonly VkApi _api;
        private readonly ulong _id;
        private readonly IMessageSender _messageSender;

        public Bot(VkApi api, ulong id, IMessageSender messageSender)
        {
            _id = id;
            _api = api;
            _messageSender = messageSender;
        }

        public void StartListening()
        {
            while (true)
            {
                var server = _api.Groups.GetLongPollServer(_id);
                var poll = _api.Groups.GetBotsLongPollHistory(new BotsLongPollHistoryParams
                {
                    Server = server.Server,
                    Ts = server.Ts,
                    Key = server.Key,
                    Wait = 25
                });

                if (poll?.Updates == null) continue;

                foreach (var update in poll.Updates)
                {
                    if (update.Type != GroupUpdateType.MessageNew) 
                        continue;
                    
                    var commands = update.Message.Text.Split(' ');
                    if (commands[0] == ".spam")
                        Spam(update, commands);
                }
            }
        }

        private void Spam(GroupUpdate update, string[] commands)
        {
            if (commands.Length < 2) 
                return;

            if (!int.TryParse(commands[1], out var messagesCount))
                return;

            _messageSender.SendMessage("Start spam attack!", update.Message.PeerId, 1);
            _messageSender.SendMessage(update.Message, update.Message.PeerId, messagesCount);
        }
    }
}