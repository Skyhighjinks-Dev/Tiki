using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiki.src.Managers;

namespace Tiki.src;
internal class Bot
{
  private string DiscordToken { get; init; }

  private DiscordSocketClient DiscordClient;
  private CommandService CommandService;

  public Bot(string nToken)
  {
    this.DiscordToken = nToken;
  }


  public async Task MainAsync(string nDiscordPrefix)
  {
    this.DiscordClient = new DiscordSocketClient(new DiscordSocketConfig()
    {
      LogLevel = LogSeverity.Debug
    });

    this.CommandService = new CommandService(new CommandServiceConfig()
    {
      LogLevel = LogSeverity.Debug,
      CaseSensitiveCommands = false,
      DefaultRunMode = RunMode.Async,
      IgnoreExtraArgs = true
    });

    var collection = new ServiceCollection();
    collection.AddSingleton(DiscordClient)
              .AddSingleton(CommandService);

    ServiceManager.SetProvider(collection);

    if (string.IsNullOrEmpty(DiscordToken))
      throw new Exception("Discord token must be provided!");


    await CommandManager.LoadCommandsAsync();
    await EventManager.RegisterEvents(nDiscordPrefix);
    await DiscordClient.LoginAsync(TokenType.Bot, this.DiscordToken);
    await DiscordClient.StartAsync();

    await Task.Delay(-1);
  }
}
