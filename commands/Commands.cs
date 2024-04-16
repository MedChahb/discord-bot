using DiscordBot.games;
using DiscordBot.methode_auxiliaire;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace DiscordBot.commands
{
    public class Commands : BaseCommandModule
    {
        public static List<string> MembersOfRole = new List<string>();

        public bool InTTTGame = false;
        public TicTacToe XOGame = new TicTacToe();

        //to test for tomorrow
        [Command("TodaysZaml")]
        [Description("Gives you the Zaml of the day.")]
        public async Task TodaysZaml(CommandContext ctx)
        {
            var embedMSG = new DiscordEmbedBuilder
            {
                Title = "ZAML",
                Description = $"Today's Zamel : {Methodes.GetRandomElementPerDay(MembersOfRole)} 💅❤️",
            };
            await ctx.RespondAsync(embedMSG);
        }

        [Command("PPsize")]
        [Description("Shows them you're ZB size")]
        public async Task PPsize(CommandContext ctx)
        {
            int randomIndex = new Random().Next(0, 14);
            string zb = "";
            for (int i = 0; i < randomIndex; i++)
                zb += "=";

            var embedMSG = new DiscordEmbedBuilder
            {
                Title = "ZEB",
                Description = $" zb dyalk : 8{zb}D",
            };
            await ctx.RespondAsync(embedMSG);
        }
        [Command("PPsize")]
        [Description("Show them his ZB size.")]
        public async Task PPsize(CommandContext ctx, DiscordMember member)
        {
            int randomIndex = new Random().Next(0, 14);
            string zb = "";
            for (int i = 0; i < randomIndex; i++)
                zb += "=";

            var embedMSG = new DiscordEmbedBuilder
            {
                Title = "ZEB",
                Description = $"zb d {member.DisplayName} : 8{zb}D",
            };
            await ctx.RespondAsync(embedMSG);
        }

        [Command("bzzl")]
        [Description("Central jawda.")]
        public async Task SendGif(CommandContext ctx)
        {

            // Create an embed with the GIF URL
            var embed = new DiscordEmbedBuilder
            {
                Title = "BZZL",
                ImageUrl = "https://images-ext-2.discordapp.net/external/am6NQ7MxzdfF-sRwl_pRYJ7hAfkX_RGlcdqfJzOvSFc/https/cdn.boob.bot/boobs/80008C77.gif"
            };

            // Send the embed
            await ctx.RespondAsync(embed: embed);
        }

        //generation of image is wokring, but NOT SENDING THE IMAGE
        [Command("XO")]
        [Description("Start TicTacToe game.")]
        public async Task XO(CommandContext ctx, int row, int col)
        {
            XOGame.PlaceSymbol(row, col, 'X');
            XOGame.SaveGameState();

            // Create an embed with the image URL
            var embed = new DiscordEmbedBuilder
            {
                Title = "XO",
                ImageUrl = "attachment://XO.jpg", // NOT WORKING
                Color = DiscordColor.Red,
                
            };

            // Send the embed with the image as an attachment
            await ctx.RespondAsync(embed: embed);

            //start all over if ended
            if (false) // if ended
                this.XOGame = new TicTacToe();

        }
    
    }
}
