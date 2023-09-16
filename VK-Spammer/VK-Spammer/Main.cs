using System;
using Spammer.Senders;
using VkNet;
using VkNet.Model;

namespace Spammer
{
    [Obsolete]
    public sealed class EntryPoint
    {
        public static void Main(string[] args)
        {
            var token = "TOKEN OF COMMUNITY";
            ulong id = ID OF COMMUNITY;

            var api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = token });

            var sender = new DefaultMessageSender(api);
            var bot = new Bot(api, id, sender);
            bot.StartListening();
        }
    }
}
