using VkNet;
using System;
using VkNet.Model;
using SpamBot.Senders;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

namespace SpamBot
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
                LongPollServerResponse server = _api.Groups.GetLongPollServer(_id);
                BotsLongPollHistoryResponse poll = _api.Groups.GetBotsLongPollHistory(
                    new BotsLongPollHistoryParams()
                    {
                        Server = server.Server,
                        Ts = server.Ts,
                        Key = server.Key,
                        Wait = 25
                    });

                if (poll?.Updates == null) continue;

                foreach (GroupUpdate update in poll.Updates)
                {
                    if (update.Type == GroupUpdateType.MessageNew)
                    {
                        string[] commands = update.Message.Text.Split(' ');
                        if (commands[0] == ".spam")
                            Spam(update, commands);
                    }
                }
            }
        }

        private void Spam(GroupUpdate update, string[] commands)
        {
            if (commands.Length < 2) return;

            if (int.TryParse(commands[1], out int messagesCount)) { }
            else return;

            _messageSender.SendMessage("Start spam attack!", update.Message.PeerId, 1);
            _messageSender.SendMessage(update.Message, update.Message.PeerId, messagesCount);
        }
    }
}