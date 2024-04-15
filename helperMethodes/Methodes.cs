using DiscordBot.commands;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.methode_auxiliaire
{
    public class Methodes
    {
        private static Dictionary<List<string>, Tuple<DateTime, string>> lastCallDates = new Dictionary<List<string>, Tuple<DateTime, string>>();

        public static string GetRandomElementPerDay(List<string> stringList)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Check if the file exists
            if (!File.Exists("last_call_date.txt"))
            {
                // If the file doesn't exist, create it and write today's date
                File.WriteAllText("last_call_date.txt", today.ToString());
                int randomIndexx = new Random().Next(0, stringList.Count);
                return stringList[randomIndexx];
            }

            // Read the last call date from the file
            string lastCallDateText = File.ReadAllText("last_call_date.txt");

            // Parse the last call date
            if (DateTime.TryParse(lastCallDateText, out DateTime lastCallDate))
            {
                // Check if the last call was today
                if (today == lastCallDate.Date)
                {
                    // Return the same element as last time
                    return stringList.FirstOrDefault();
                }
            }

            // Generate a random index for the list
            int randomIndex = new Random().Next(0, stringList.Count);
            string randomElement = stringList[randomIndex];

            // Write today's date to the file
            File.WriteAllText("last_call_date.txt", today.ToString());

            // Return the randomly selected element
            return randomElement;
        }

        public static async Task SetMembersOfRoleAtReady(DiscordClient sender, ReadyEventArgs args, ulong GuildId, string roleName)
        {
            // Find the guild by its ID
            var guild = sender.Guilds.Values.FirstOrDefault(g => g.Id == GuildId);

            // Check if the guild exists
            if (guild != null)
            {
                // Get all members of the guild
                var members = await guild.GetAllMembersAsync();

                // Get the role by name
                var role = guild.Roles.Values.FirstOrDefault(r => r.Name == roleName);
                if (role == null)
                {
                    Console.WriteLine($"Role '{roleName}' not found in guild '{guild.Name}'.");
                    return;
                }

                // Filter members by role
                var membersOfRole = members.Where(m => m.Roles.Contains(role))
                                           .Select(m => m.Username)
                                           .ToList();

                // Set the static property of Commands
                Commands.MembersOfRole = membersOfRole;
            }
            else
            {
                Console.WriteLine("Guild not found.");
            }
        }
    }
}
