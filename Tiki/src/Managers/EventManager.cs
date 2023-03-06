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
  /// <summary>DiscordSocketClient from DI</summary>
  private static DiscordSocketClient Client = ServiceManager.GetService<DiscordSocketClient>();

  /// <summary>CommandService from DI</summary>
  private static CommandService CommandService = ServiceManager.GetService<CommandService>();

  /// <summary>Discord configuration</summary>
  private static TikiConfig Config = ServiceManager.GetService<TikiConfig>();


  /// <summary>
  /// Registers all events to client and provides command prefix for MessageReceived event
  /// </summary>
  /// <param name="nCommandPrefix">Command prefix</param>
  public static async Task RegisterEvents()
  { 
    Client.Ready += OnClientReady;
    Client.MessageReceived += OnClientMessageReceived;
  }


  /// <summary>
  /// Method to execute when client sends Ready event
  /// </summary>
  private static async Task OnClientReady()
  {
    Console.WriteLine($"{Client.CurrentUser.Username} has logged in!");

    await Client.SetStatusAsync(UserStatus.Idle);
    await Client.SetGameAsync($"Prefix: {Config.DiscordCommandPrefix} - Under Development");
  }


  /// <summary>
  /// Handles messages being received
  /// </summary>
  /// <param name="nMessage">Message from discord</param>
  private static async Task OnClientMessageReceived(SocketMessage nMessage)
  { 
    var message = (SocketUserMessage)nMessage;
    var context = new SocketCommandContext(Client, message);

    if(message.Author.IsBot || message.Channel is IDMChannel)
      return;

    int argPos = 0;
    if(!message.HasStringPrefix(Config.DiscordCommandPrefix, ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos))
      return; 

    var result = await CommandService.ExecuteAsync(context, argPos, ServiceManager.ServiceProvider);

    if(!result.IsSuccess)
      Console.WriteLine($"Error with command: '{result.ErrorReason}'");
  }
}
