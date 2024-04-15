using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using DiscordBot.methode_auxiliaire;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Threading;
using Microsoft.SqlServer.Server;
using System.Linq;

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
