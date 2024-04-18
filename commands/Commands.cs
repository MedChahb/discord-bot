using DiscordBot.games;
using DiscordBot.methode_auxiliaire;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Threading.Tasks;


namespace DiscordBot.commands
{
    public class Commands : BaseCommandModule
    {
        public static List<string> MembersOfRole = new List<string>();

        public TicTacToe XOGame = new TicTacToe();

        //to test for tomorrow
        [Command("TodaysZaml")]
        [Description("Gives you the Zaml of the day.")]
        public async Task TodaysZaml(CommandContext ctx)
        {
            var embedMSG = new DiscordEmbedBuilder
            {
                Title = "ZAML",
                Description = $"Today's Zamel : {Methodes.GetRandomElementPerDay(MembersOfRole)}  💅❤️",
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
                ImageUrl = "https://images-ext-2.discordapp.net/external/am6NQ7MxzdfF-sRwl_pRYJ7hAfkX_RGlcdqfJzOvSFc/https/cdn.boob.bot/boobs/80008C77.gif",
                Color = DiscordColor.Red
            };

            // Send the embed
            await ctx.RespondAsync(embed: embed);
        }

        // stop taging urself
        [Command("XO")]
        [Description("Start TicTacToe game.")]
        public async Task XO(CommandContext ctx, DiscordMember member)
        {
            if(ctx.User.Id == member.Id) await ctx.RespondAsync("You cant play againt yourself.");
            else if (XOGame.isNotStarted)
            {
                XOGame.SetStarter((DiscordMember)ctx.User);
                XOGame.SetFirstTurn((DiscordMember)ctx.User);
                XOGame.SetVersus(member);

                var embedMSG = new DiscordEmbedBuilder
                {
                    Title = $"__{((DiscordMember)(ctx.User)).DisplayName}__ VS __{member.DisplayName}__",
                    Description = "**" + XOGame.DisplayXOGrid() + "**",
                    Color = DiscordColor.Red,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "Use !XO <col> <row> to play.",
                    }
                };
                XOGame.HasStarted();
                await ctx.RespondAsync(embedMSG);
                await ctx.Channel.SendMessageAsync($"{XOGame.turn.Mention} to play.");

            }
            else
            {
                await ctx.RespondAsync("Wait for the game to end.");
            }
        }

        [Command("XO")]
        [Description("Start TicTacToe game.")]
        public async Task XO(CommandContext ctx, int col, int row)
        {

            if (XOGame.isNotStarted)
            {
                await ctx.RespondAsync($"no running game, try !XO <Mention>.");
            }
            else
            {
                // 1-3 -> 0-2
                col -= 1; row -= 1;
                
                // if User is the one to play -> OK!
                if (ctx.User.Id == XOGame.turn.Id)
                {
                    if (!(col >= 0 && col <= 2 && row >= 0 && row <= 2))
                    {
                        await ctx.Channel.SendMessageAsync($"Invalid position, out of board range");
                        return;
                    }
                    // check which char to place based on the User (starter -> X, versus -> O)
                    if (XOGame.isCellEmpty(col, row))
                    {
                        if (ctx.User.Id == XOGame.starter.Id) XOGame.PlaceX(col, row);
                        else if (ctx.User.Id == XOGame.versus.Id) XOGame.PlaceO(col, row);
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync($"Invalid position, taken.");
                        return;
                    }

                    var embedMSG = new DiscordEmbedBuilder
                    {
                        Title = $"__{XOGame.starter.DisplayName}__ VS __{XOGame.versus.DisplayName}__",
                        Description = "**"+XOGame.DisplayXOGrid()+"**",
                        Color = DiscordColor.Red,
                        Footer = new DiscordEmbedBuilder.EmbedFooter
                        {
                            Text = "Use !XO <col> <row> to play.",
                        }
                    };
                    XOGame.ChangeTurn();
                    await ctx.RespondAsync(embedMSG);

                    if (XOGame.Draw())      { await ctx.Channel.SendMessageAsync($"**Draw**"); this.XOGame = new TicTacToe(); }
                    else if (XOGame.Win())  { await ctx.Channel.SendMessageAsync($"**Winner**: {XOGame.winner.Mention}"); this.XOGame = new TicTacToe(); }
                    else                      await ctx.Channel.SendMessageAsync($"{XOGame.turn.Mention} to play.");
                }
                else
                {
                    await ctx.Channel.SendMessageAsync($"Not your turn. Waiting for {XOGame.turn.Mention}");
                }

                
            }
            
        }

        [Command("XO")]
        [Description("Start TicTacToe game.")]
        public async Task XO(CommandContext ctx, string s)
        {
            if(string.Equals("end", s, StringComparison.OrdinalIgnoreCase))
            {
                if (ctx.User.Id == XOGame.starter.Id || ctx.User.Id == XOGame.versus.Id)
                {
                    this.XOGame = new TicTacToe();
                    await ctx.RespondAsync("The game has ended.");
                }
            }
            else
            {
                await ctx.RespondAsync("Do you mean : !XO end ?");
            }
        }
    }
}
