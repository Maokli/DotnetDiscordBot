using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace DotnetDiscordBot
{
  public class DiscordConnection
  {
    private readonly DiscordSocketClient _client;

    public DiscordConnection(DiscordSocketClient client)
    {
      _client = client;
    }

    public async Task ConnectToDiscordAsync()
    {

      //hook client log to the created log handler
      _client.Log += Log;

      var token = GetDiscordToken();

      await _client.LoginAsync(TokenType.Bot, token);
      await _client.StartAsync();

      // Block this task until the program is closed.
      await Task.Delay(-1);
    }

    private string GetDiscordToken()
    {
      var builder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

      IConfigurationRoot configuration = builder.Build();
      return configuration.GetSection("DiscordToken").Value;

    }

    private Task Log(LogMessage msg)
    {
      Console.WriteLine(msg.ToString());

      return Task.CompletedTask;
    }

  }
}