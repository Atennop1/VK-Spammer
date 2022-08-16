using System;
using VkNet;
using VkNet.Model;
using System.Text;
using VkNet.Exception;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;

public class Program
{
    [Obsolete]
    public static void Main(string[] args)
    {
        Bot bot = new Bot("TOKEN OF GROUP");
        bot.Activate(ID OD GROUP);
    }
}

public class Bot
{
    private readonly VkApi _api;

    public Bot(string token)
    {
        _api = new VkApi();
        _api.Authorize(new ApiAuthParams() { AccessToken = token });
    }

    [Obsolete]
    public void Activate(ulong id)
    {
        while (true)
        {
            LongPollServerResponse server = _api.Groups.GetLongPollServer(id);
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
                    {
                        if (int.TryParse(commands[1], out int messagesCount)) { }
                        else continue;

                        SendMessage("Start spam attack!", update.Message.PeerId);

                        StringBuilder stringBuilder = new StringBuilder();
                        for (int i = 0; i < commands.Length - 2; i++)
                            stringBuilder.Append(commands[i + 2] + " ");
                        string message = stringBuilder.ToString();

                        int sendedCount = 0;
                        while (sendedCount != messagesCount)
                        {
                            try
                            {
                                SendMessage(message, update.Message.PeerId);
                                sendedCount++;
                            }
                            catch (VkApiException) { continue; }
                        }
                    }
                }
            }
        }
    }

    private void SendMessage(string message, long? peerId)
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
}
