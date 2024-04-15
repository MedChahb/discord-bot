using DiscordBot.methode_auxiliaire;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.commands
{
    public class Commands : BaseCommandModule
    {
        public static List<string> MembersOfRole = new List<string>();

        //to test for tomorrow
        [Command("TodaysZaml")]
        public async Task TodaysZaml(CommandContext ctx)
        {
            // doesnt work when bot is disconected
            await ctx.Channel.SendMessageAsync($"{Methodes.GetRandomElementPerDay(MembersOfRole)}");
        }

    }
}
