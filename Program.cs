using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace DotnetDiscordBot
{
    class Program
    {
        private static DiscordSocketClient _client;
        static void Main(string[] args)
        {
            _client = new DiscordSocketClient();
            var discordConnection = new DiscordConnection(_client);

            discordConnection.ConnectToDiscordAsync().GetAwaiter().GetResult();

        }

    }
}
