using Tiki.src;
using static Tiki.Config;

namespace Tiki;

internal class Program
{
  static async Task Main(string[] args)
  { 
    DiscordConfig config = ExtractDiscordConfig(args);

    Bot bot = new Bot(config.DiscordToken);
    await bot.MainAsync(config.DiscordCommandPrefix);
  }


  private static DiscordConfig ExtractDiscordConfig(string[] nArgs)
  { 
    DiscordConfig toReturn = new DiscordConfig();

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