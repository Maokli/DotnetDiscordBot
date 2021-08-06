using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DotnetDiscordBot.Handlers;
using DotnetDiscordBot.Modules;
using Microsoft.Extensions.Configuration;

namespace DotnetDiscordBot
{
    class Program
    {
        private static DiscordSocketClient _client;
        static void Main(string[] args)
        {
            _client = new DiscordSocketClient();

            //Command Handling Services
            var commandService = new CommandService();
            var commandHandler = new CommandHandler(_client, commandService);
            commandHandler
                .InstallCommandsAsync()
                .GetAwaiter()
                .GetResult();

            //connects to the bot
            var discordConnection = new DiscordConnection(_client);
            discordConnection.ConnectToDiscordAsync()
                .GetAwaiter()
                .GetResult();
            
        }

    }
}
