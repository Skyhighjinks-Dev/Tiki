using Tiki.src;

namespace Tiki;

internal class Program
{
  /// <summary>
  /// App entry
  /// </summary>
  /// <param name="args">Command arguments - contains sensitive data</param>
  static async Task Main(string[] args)
  {
    TikiConfig config = ExtractDiscordConfig(args);

    Bot bot = new Bot(config);
    await bot.MainAsync();
  }


  /// <summary>
  /// Extracts discord configuration from args
  /// </summary>
  /// <param name="nArgs">Args passed into main method</param>
  /// <returns>Discord Config</returns>
  private static TikiConfig ExtractDiscordConfig(string[] nArgs)
  {
    TikiConfig toReturn = new TikiConfig();

    for(int x = 0; x < nArgs.Length; x++)
    { 
      if(nArgs[x].StartsWith("/dt:"))
      { 
        toReturn.DiscordToken = nArgs[x].Substring(4);
        continue;
      }

      if(nArgs[x].StartsWith("/dcp:"))
      { 
        toReturn.DiscordCommandPrefix = nArgs[x].Substring(5);
        continue;
      }
    }

    return toReturn;
  }
}