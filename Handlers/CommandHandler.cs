using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DotnetDiscordBot.Handlers
{
  public class CommandHandler
  {
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;

    public CommandHandler(DiscordSocketClient client, CommandService commands)
    {
      _commands = commands;
      _client = client;
    }

     public async Task InstallCommandsAsync()
    {
        // Hook the MessageReceived event into the command handler
        _client.MessageReceived += HandleCommandAsync;

        // Here we discover all of the command modules in the entry 
        // assembly and load them.
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), 
                                        services: null);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        //If it's not a user message, don't process
        var message = messageParam as SocketUserMessage;
        if(message == null) return;

        //position of the prefix
        int argPosition = 0;

        //If the message isn't prefixed or is a bot trigger command
        //Don't process
        if(!(message.HasCharPrefix('!', ref argPosition)) || 
            message.HasMentionPrefix(_client.CurrentUser, ref argPosition) || 
            message.Author.IsBot)
            return;
        
        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await _commands.ExecuteAsync(
            context: context, 
            argPos: argPosition,
            services: null);
    }

  }
}