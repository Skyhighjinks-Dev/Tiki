using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiki.src.Managers;
internal static class EventManager
{
  private static DiscordSocketClient Client = ServiceManager.GetService<DiscordSocketClient>();
  private static CommandService CommandService = ServiceManager.GetService<CommandService>();
  private static string CommandPrefix { get; set; }

  public static async Task RegisterEvents(string nCommandPrefix)
  { 
    CommandPrefix = nCommandPrefix;

    Client.Ready += OnClientReady;
    Client.MessageReceived += OnClientMessageRecieved;
  }


  private static async Task OnClientReady()
  {
    Console.WriteLine($"{Client.CurrentUser.Username} has logged in!");

    await Client.SetStatusAsync(UserStatus.Idle);
    await Client.SetGameAsync($"Prefix: {CommandPrefix} - Under Development");
  }

  private static async Task OnClientMessageRecieved(SocketMessage nMessage)
  { 
    var message = (SocketUserMessage)nMessage;
    var context = new SocketCommandContext(Client, message);

    if(message.Author.IsBot || message.Channel is IDMChannel)
      return;

    int argPos = 0;
    if(!message.HasStringPrefix(CommandPrefix, ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos))
      return; 

    var result = await CommandService.ExecuteAsync(context, argPos, ServiceManager.ServiceProvider);

    if(!result.IsSuccess)
      Console.WriteLine($"Error with command: '{result.ErrorReason}'");
  }
}
