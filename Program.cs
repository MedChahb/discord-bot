using System.Drawing;
using System.Threading.Tasks;

namespace DiscordBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Bot.Bot DiscordBot = new Bot.Bot(); // instance du Bot
            await Task.Delay(-1); // infinite delay -> bot stays connected
        }


    }
}