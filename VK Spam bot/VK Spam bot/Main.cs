using System;
using VkNet;
using VkNet.Model;
using SpamBot.Senders;

namespace SpamBot
{
    [Obsolete]
    public class MainClass
    {
        public static void Main(string[] args)
        {
            string token = "TOKEN OF COMMUNITY";
            ulong id = ID OF COMMUNITY;

            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = token });

            DefaultMessageSender sender = new DefaultMessageSender(api);

            Bot bot = new Bot(api, id, sender);
            bot.StartListening();
        }
    }
}
