using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DotnetDiscordBot.Modules
{
    public class GreetingModule : ModuleBase<SocketCommandContext>
    {
        [Command("greet")]
        [Alias("user", "whois")]
        public async Task GreetAsync(SocketUser user = null)
        {
            var userToGreet = user ?? Context.User;

            await ReplyAsync($"Hello {userToGreet.Mention}");
        }
    }
}