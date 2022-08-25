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
            string token = "vk1.a.UoHRbzoZYnMsMCriuEvX7fLT1EXyGyZ0QYIfHBhPt6yZqV6596mwQfnbuqEUibG24eQ6Qux6c2KXs1GkI2lx0qaNggQXs8DTmzyCJO9nzZ6_y37aLA6qeNqVje_kAm51CMdNDsM9qGpwocLfx0GndiQAESWFR3F9uXw55b_PPG8MfX0ZnxrQnFYW-z1rjCSy";
            ulong id = 198111421;

            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = token });

            DefaultMessageSender sender = new DefaultMessageSender(api);

            Bot bot = new Bot(api, id, sender);
            bot.StartListening();
        }
    }
}