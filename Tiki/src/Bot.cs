using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Tiki.src.Managers;
using static Tiki.Config;

namespace Tiki.src;
internal class Bot
{
  /// <summary>Discord Configuration from arguments</summary>
  private Config.DiscordConfig DiscordConfig { get; set; }

  /// <summary>Discord client</summary>
  private DiscordSocketClient DiscordClient;

  /// <summary>Command Service</summary>
  private CommandService CommandService;


  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="nToken">Discord config</param>
  public Bot(Config.DiscordConfig nConfig)
  {
    this.DiscordConfig = nConfig;
  }


  /// <summary>
  /// Main executing async
  /// Will run forever 
  /// </summary>
  public async Task MainAsync()
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
              .AddSingleton(CommandService)
              .AddSingleton(DiscordConfig);

    ServiceManager.SetProvider(collection);

    if (string.IsNullOrEmpty(DiscordConfig.DiscordToken))
      throw new Exception("Discord token must be provided!");


    await CommandManager.LoadCommandsAsync();
    await EventManager.RegisterEvents();
    await DiscordClient.LoginAsync(TokenType.Bot, this.DiscordConfig.DiscordToken);
    await DiscordClient.StartAsync();

    await Task.Delay(-1);
  }
}
