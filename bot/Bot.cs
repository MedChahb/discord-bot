using DiscordBot.commands;
using DiscordBot.config;
using DiscordBot.methode_auxiliaire;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Bot
{
    // configs the bot and make it online
    public class Bot
    {
        private DiscordClient Client { get; set; }
        private CommandsNextExtension Commands { get; set; }

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

            //config des commandes
            var CommandsConfid = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] {jr.prefix},
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = true, // to turn false when help page is done
                CaseSensitive = false,
            };
            this.Commands = Client.UseCommandsNext(CommandsConfid);

                //registration of commands
            this.Commands.RegisterCommands<Commands>();

            await this.Client.ConnectAsync();
        }
        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            ulong zb_trmaID = 931298615463383090;
            string role = "lolers";

            Console.WriteLine("connected!");

            _ = Methodes.SetMembersOfRoleAtReady(sender, args, zb_trmaID,role);

            return Task.CompletedTask;
        }
    }
}
