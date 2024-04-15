using DSharpPlus;
using System.Threading.Tasks;
using DiscordBot.config;
using DSharpPlus.CommandsNext;

namespace DiscordBot.Bot
{
    // configs the bot and make it online
    public class Bot
    {
        private DiscordClient Client { get; set; }
        private CommandsNextConfiguration Commands { get; set; }
        public Bot()
        {
            _ = this.BotConfig();
        }

        private async Task BotConfig()
        {
            JsonReader jr = new JsonReader();
            await jr.ReadJson();

            //configs of the bot -> (set token and intents + reconnect if crashed)
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jr.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };
            // creating an instance of the client whit the configs we want
            this.Client = new DiscordClient(discordConfig);

            // what bot does when connected successfully
            this.Client.Ready += Client_Ready;

            await this.Client.ConnectAsync();
        }
        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
